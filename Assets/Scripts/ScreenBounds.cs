using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds: MonoBehaviour
{
    static Vector2 bounds;
    public static float Left { get { return -bounds.x - 1.5f; } }
    public static float Right { get { return bounds.x + 1.5f; } }
    public static float Top { get { return bounds.y; } }
    public static float Bottom { get { return -bounds.y; } }

    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}
