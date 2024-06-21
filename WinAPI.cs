using System;
using System.Runtime.InteropServices;

public static class Win32API
{
    /// <summary> hide window </summary>
    private const int SW_HIDE = 0;
    /// <summary> show window </summary>
    private const int SW_SHOW = 5;

    /// <summary> put on the top </summary>
    private static readonly IntPtr HWND_TOP = new IntPtr(0);

    /// <summary> Keep the current size (ignore the cx and cy parameters) </summary>
    private const uint SWP_NOSIZE = 0x0001;
    /// <summary> Keep the current Z order (ignore the hWndInsertAfter parameter) </summary>
    private const uint SWP_NOZORDER = 0x0004;

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    /// <summary><a href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow">document</a></summary>
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    /// <summary><a href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow">document</a></summary>
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    /// <summary><a href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos">document</a></summary>
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    public static void Show(IntPtr hwnd) => ShowWindow(hwnd, SW_SHOW);
    public static void Hide(IntPtr hwnd) => ShowWindow(hwnd, SW_HIDE);

    public static void SetWindowPosOnDisplay2(IntPtr hWnd, int left, int top)
    {
        SetWindowPos(hWnd, HWND_TOP, left, top, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
    }
}
