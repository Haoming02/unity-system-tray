using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TrayDemo : MonoBehaviour
{
    [SerializeField]
    private Texture2D icon;
    [SerializeField]
    private string iconName;
    [SerializeField]
    private Image demoImage;

    private void Greet() { Debug.Log("Icon Left Clicked!"); }
    private void Rotate() { demoImage.gameObject.transform.Rotate(0.0f, 0.0f, 30.0f); }
    private void ChangeImageColor(Color color) { demoImage.color = color; }

    private void Stop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void Awake()
    {
        var context = new List<(string, Action)>() {
            (TrayIcon.LEFT_CLICK, Greet),
            ("Rotate Image", Rotate),
            ("Change Image Color\\Red", () => ChangeImageColor(Color.red)),
            ("Change Image Color\\Green",() => ChangeImageColor(Color.green)),
            ("Change Image Color\\Yellow", () => ChangeImageColor(Color.yellow)),
            ("Change Image Color\\Blue", () => ChangeImageColor(Color.blue)),
            (TrayIcon.SEPARATOR, null),
            ("Quit", Stop)
        };

        TrayIcon.Init("Demo", iconName, icon, context);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TrayIcon.ShowBalloonTip("Pop Up!", "You pressed Space!", TrayIcon.ToolTipIcon.Info);
    }
}
