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
    public bool LockGame;

    void Start()
    {
        CurrentColor = "none";
        BlocksDestroyed = 0;
        Level = 1;
        //CurrentLevel.text = "LEVEL " + Level.ToString();
        CurrentPoints.text = Points.ToString();
    }

    public int GetCurrentLevel()
    {
        return Level;
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
