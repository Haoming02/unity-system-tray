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
- Call `InitTray()` with the title of the icon and the `Texture2D` for the icon
- You can call `TriggerBalloonTip()` to trigger a notification
- You may also edit the `SystemTray.cs` to add/remove the buttons shown when right-clicking the icon
- Remember to `Dispose()` the `Tray` object

## Known Issues
- The editor sometimes crashes when recompiling the scripts
- When entering **Play Mode** after the first time, there will be an error popup...
