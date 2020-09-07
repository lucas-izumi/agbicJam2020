using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public string CurrentColor;
    // Start is called before the first frame update
    void Start()
    {
        CurrentColor = "none";
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
