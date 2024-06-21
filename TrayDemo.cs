using System;
using UnityEngine;

public class TrayDemo : MonoBehaviour
{
    [SerializeField]
    private Texture2D icon;

    private Tray tray;

    void Awake()
    {
        tray = new Tray();
        tray.InitTray("Demo", icon, new Tray.MenuItem[]
        {
          new Tray.MenuItem("Show Window", (object sender, EventArgs e) => tray.ShowWindow() ),
          new Tray.MenuItem("Hide Window", (object sender, EventArgs e) => tray.HideWindow() ),
          new Tray.MenuItem("Windowed", (object sender, EventArgs e) => Screen.SetResolution(1280, 720, false) ),
          new Tray.MenuItem("Quit", (object sender, EventArgs e) =>
            {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #else
                UnityEngine.Application.Quit();
            #endif
            }),
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            tray.TriggerBalloonTip("KeyDown", "Space was Pressed!");
    }

    void OnApplicationQuit()
    {
        tray?.Dispose();
        tray = null;
    }
}
