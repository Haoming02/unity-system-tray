using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UnityEngine;

public class Tray : IDisposable
{
    private IntPtr hwnd;

    private NotifyIcon notifyIcon;
    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem menuItem_ShowWindow;
    private ToolStripMenuItem menuItem_HideWindow;
    private ToolStripMenuItem menuItem_Windowed;
    private ToolStripMenuItem menuItem_Quit;

    public void InitTray(string iconText, Texture2D iconTexture)
    {
        this.hwnd = Win32API.GetForegroundWindow();

        // ===== ContextMenu (Right Click) =====
        this.contextMenu = new ContextMenuStrip();
        this.menuItem_ShowWindow = new ToolStripMenuItem();
        this.menuItem_HideWindow = new ToolStripMenuItem();
        this.menuItem_Windowed = new ToolStripMenuItem();
        this.menuItem_Quit = new ToolStripMenuItem();

        // ===== NotifyIcon =====
        this.notifyIcon = new NotifyIcon();
        this.notifyIcon.Text = iconText;
        this.notifyIcon.Icon = CustomTrayIconFromTexture2D(iconTexture);
        this.notifyIcon.ContextMenuStrip = this.contextMenu;
        this.notifyIcon.MouseDoubleClick += OnMouseClick;
        this.notifyIcon.Visible = true;

        // ===== ContextMenu Items =====
        this.contextMenu.SuspendLayout();

        var menuItems = new ToolStripMenuItem[]
            {
                this.menuItem_ShowWindow,
                this.menuItem_HideWindow,
                this.menuItem_Windowed,
                this.menuItem_Quit
            };

        const int menuPadding = 1;
        const int menuWidth = 180;
        const int menuHeight = 22;

        this.contextMenu.Items.AddRange(menuItems);
        this.contextMenu.Size = new Size(menuWidth + menuPadding,
            (menuItems.Length + menuPadding) * menuHeight);

        this.menuItem_ShowWindow.Size = new Size(menuWidth, menuHeight);
        this.menuItem_ShowWindow.Text = "Show Window";
        this.menuItem_ShowWindow.Click += (sender, e) => Win32API.Show(this.hwnd);

        this.menuItem_HideWindow.Size = new Size(menuWidth, menuHeight);
        this.menuItem_HideWindow.Text = "Hide Window";
        this.menuItem_HideWindow.Click += (sender, e) => Win32API.Hide(this.hwnd);

        this.menuItem_Windowed.Size = new Size(menuWidth, menuHeight);
        this.menuItem_Windowed.Text = "Windowed";
        this.menuItem_Windowed.Click += (sender, e) => UnityEngine.Screen.SetResolution(1280, 720, false);

        this.menuItem_Quit.Size = new Size(menuWidth, menuHeight);
        this.menuItem_Quit.Text = "Quit";
        this.menuItem_Quit.Click += (sender, e) =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            UnityEngine.Application.Quit();
#endif
        };

        this.contextMenu.ResumeLayout(false);
    }

    public void TriggerBalloonTip(string title, string content, int timeout = 1000)
    {
        this.notifyIcon.ShowBalloonTip(timeout, title, content, ToolTipIcon.Info);
    }

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            Win32API.Show(this.hwnd);
    }

    public void ShowTray() => this.notifyIcon.Visible = true;
    public void HideTray() => this.notifyIcon.Visible = false;

    public void Dispose()
    {
        this.notifyIcon.MouseDoubleClick -= OnMouseClick;
        this.notifyIcon?.Dispose();

        this.contextMenu?.Dispose();
        this.menuItem_ShowWindow?.Dispose();
        this.menuItem_HideWindow?.Dispose();
        this.menuItem_Windowed?.Dispose();
        this.menuItem_Quit?.Dispose();

        this.hwnd = IntPtr.Zero;
    }

    private static Icon CustomTrayIconFromTexture2D(Texture2D texture, int width = 64, int height = 64)
    {
        var byteArray = texture.EncodeToPNG();
        using (MemoryStream ms = new MemoryStream(byteArray))
        {
            Image returnImage = Image.FromStream(ms);
            ms.Flush();

            Bitmap bt = new Bitmap(returnImage);
            Bitmap fitSizeBt = new Bitmap(bt, width, height);
            return Icon.FromHandle(fitSizeBt.GetHicon());
        }
    }
}
