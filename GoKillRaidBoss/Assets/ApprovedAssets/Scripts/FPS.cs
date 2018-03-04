using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{

    float fps;
    int i;
    float timer;
    GUIStyle style = new GUIStyle();
    int fontSize = 20;

    void Start()
    {
        //style.fontSize = 10;
        style.alignment = TextAnchor.MiddleCenter;
    }

    void Update()
    {
        i++;
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            fps = i * (1 / timer);
            timer = 0f;
            i = 0;
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;

        GUI.contentColor = Color.red;
        GUI.Box(new Rect(10, 10, 150, 50), "");
        GUI.Box(new Rect(10, 10, 150, 50), "");
        GUI.contentColor = Color.white;
        GUI.Box(new Rect(10, 10, 150, 50), "FPS = " + (int)fps);
    }
}
