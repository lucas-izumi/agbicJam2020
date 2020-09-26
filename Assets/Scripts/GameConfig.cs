using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public string CurrentColor;
    public int BlocksDestroyed;
    private string CurrentLevel;
    private int CurrentPoints;

    void Start()
    {
        CurrentColor = "none";
        BlocksDestroyed = 0;
        CurrentPoints = 0;
        CurrentLevel = "Level 1";
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
