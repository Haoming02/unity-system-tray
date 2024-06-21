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
    private ToolStripMenuItem[] menuItems;

    public struct MenuItem
    {
        public string buttonText;
        public EventHandler callback;

        public MenuItem(string text, EventHandler e)
        {
            this.buttonText = text;
            this.callback = e;
        }
    }

    public void InitTray(string iconText, Texture2D iconTexture, MenuItem[] menuButtons)
    {
        this.hwnd = Win32API.GetForegroundWindow();

        // ===== ContextMenu (Right Click) =====
        this.contextMenu = new ContextMenuStrip();

        // ===== NotifyIcon =====
        this.notifyIcon = new NotifyIcon();
        this.notifyIcon.Text = iconText;
        this.notifyIcon.Icon = CustomTrayIconFromTexture2D(iconTexture);
        this.notifyIcon.ContextMenuStrip = this.contextMenu;
        this.notifyIcon.MouseDoubleClick += OnMouseClick;
        this.notifyIcon.Visible = true;

        // ===== ContextMenu Items =====
        this.contextMenu.SuspendLayout();

        const int menuPadding = 1;
        const int menuWidth = 160;
        const int menuHeight = 22;
        int l = menuButtons.Length;

        this.menuItems = new ToolStripMenuItem[l];

        for (int i = 0; i < l; i++)
        {
            var item = new ToolStripMenuItem();

            item.Size = new Size(menuWidth, menuHeight);
            item.Text = menuButtons[i].buttonText;
            item.Click += menuButtons[i].callback;

            this.menuItems[i] = item;
        }

        this.contextMenu.Items.AddRange(this.menuItems);
        this.contextMenu.Size = new Size(menuWidth + menuPadding,
            (l + menuPadding) * menuHeight);

        this.contextMenu.ResumeLayout(false);
    }

    public void TriggerBalloonTip(string title, string content, int timeout = 1000)
    {
        this.notifyIcon.ShowBalloonTip(timeout, title, content, ToolTipIcon.Info);
    }

    public void ShowWindow() => Win32API.Show(this.hwnd);
    public void HideWindow() => Win32API.Hide(this.hwnd);
    public void ShowTray() => this.notifyIcon.Visible = true;
    public void HideTray() => this.notifyIcon.Visible = false;

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            Win32API.Show(this.hwnd);
    }

    public void Dispose()
    {
        this.notifyIcon.MouseDoubleClick -= OnMouseClick;
        this.notifyIcon?.Dispose();

        this.contextMenu?.Dispose();
        foreach (var item in this.menuItems)
            item?.Dispose();

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
