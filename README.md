# Unity System Tray
A simple script that adds an icon to the system tray

> **Reference:** https://www.programmersought.com/article/23467662017/

## Prerequisite
- Create a `csc.rsp` file and add the following:
    ```
    -r:System.Drawing.dll
    -r:System.Windows.Forms.dll
    ```
- Go to **Edit** -> **Project Settings** -> **Player** -> **Other Settings** -> **API Compatibility Level**, set it to `.NET 4.x` *(or `.NET Framework` in other Unity versions)*

## How to Use
> A demo script is included

- Create a `Tray` object
- Call `InitTray()` with the title of the icon and the `Texture2D` for the icon, as well as an array of buttons to be shown when right-clicking the icon
- You may can call `TriggerBalloonTip()` to trigger a Windows notification
- Remember to call `Dispose()` on the `Tray` object when quitting

## Known Issues
- The editor sometimes crashes when recompiling the scripts
- When entering **Play Mode** after the first time, there will be an error popup...
