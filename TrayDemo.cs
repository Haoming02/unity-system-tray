using UnityEngine;

public class TrayDemo : MonoBehaviour
{
    [SerializeField]
    private Texture2D icon;

    private Tray tray;

    void Awake()
    {
        tray = new Tray();
        tray.InitTray("Demo", icon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            tray.TriggerBalloonTip("KeyDown", "You pressed Space!");
    }

    void OnApplicationQuit()
    {
        tray?.Dispose();
        tray = null;
    }
}
