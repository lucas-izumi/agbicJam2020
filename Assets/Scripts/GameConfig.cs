using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour
{
    public string CurrentColor;
    public int BlocksDestroyed;
    public int BlockCount;
    public Text CurrentLevel;
    public Text CurrentPoints;
    public bool LockGame;
    public string GameStatus;
    public int retries;
    public int calculatedPoints;
    public int MaxLevel;

    public int Level;
    public int Points;

    void Start()
    {
        SaveSystem save = new SaveSystem();
        SavedData sData;
        CurrentColor = "none";
        BlocksDestroyed = 0;
        BlockCount = 0;
        retries = 0;
        calculatedPoints = 0;
        sData = save.LoadGame();
        Level = sData.saved_level;
        Points = sData.saved_points;
        CurrentPoints.text = Points.ToString();

        MaxLevel = 10;
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

    public void CalculatePoints()
    {
        int newpoints = calculatedPoints - (retries);
        if (newpoints > 0)
            Points += newpoints;
        CurrentPoints.text = Points.ToString();
    }
}
