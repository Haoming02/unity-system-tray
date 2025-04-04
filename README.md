# Unity System Tray
A simple script that adds a system tray (notificaion) icon for Unity applications

> [!Important]
> Since the script uses functions from `user32.dll`, it is only usable on **Windows** systems

## How to Use

- Call the `TrayIcon.Init` function:
    - **appName:** Just an internal `string`, will not be visible
    - **tooltip:** Shows up when you hover the icon
    - **iconTexture:** Icon texture :skull:
    - **actions:** A `List` of `string`, `Action` pairs
        - For regular strings, it will be shown as an option in the menu when right-clicking the icon
        - Use `TrayIcon.LEFT_CLICK` for when left-clicking the icon directly
        - Use `TrayIcon.SEPARATOR` to add a divider in the menu

- Call `TrayIcon.ShowBalloonTip` to trigger a notification

> [!Tip]
> Check out the included Demo scene

<hr>

<p align="center"><b><i>Special Thanks: Gemini 2.5 Pro</i></b></p>
