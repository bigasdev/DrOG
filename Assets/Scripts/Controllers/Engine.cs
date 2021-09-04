using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Engine
{
    public static Vector2 MousePos{
        get{
            if(Camera.main == null) return new Vector2(0,0);
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            return worldPos;
        }
    }
}
