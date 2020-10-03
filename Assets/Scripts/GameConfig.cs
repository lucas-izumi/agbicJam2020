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
    public bool TutorialMode;

    void Start()
    {
        CurrentColor = "none";
        BlocksDestroyed = 0;
        Level = 0;
        TutorialMode = true; //This must be checked on save file. If its already past tutorial, this should be false
        //CurrentLevel.text = "LEVEL " + Level.ToString();
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
