using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour
{
    public string CurrentColor;
    public int BlocksDestroyed;
    public Text CurrentLevel;
    public Text CurrentPoints;
    public int Level;
    public int Points;

    void Start()
    {
        CurrentColor = "none";
        BlocksDestroyed = 0;
        Level = 1;
        CurrentLevel.text = "Level " + Level.ToString();
        CurrentPoints.text = Points.ToString();
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
