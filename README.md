# Unity System Tray
A simple script that adds an icon to the system tray

> **Reference:** https://www.programmersought.com/article/23467662017/

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
