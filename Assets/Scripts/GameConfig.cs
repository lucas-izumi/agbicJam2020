using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public string CurrentColor;
    public int BlocksDestroyed;

    void Start()
    {
        CurrentColor = "none";
        BlocksDestroyed = 0;
    }

    public void SetCurrentColor(string color)
    {
        CurrentColor = color;
    }

    public string GetCurrentColor()
    {
        return CurrentColor;
    }
}
