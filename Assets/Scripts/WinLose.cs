using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    private GameConfig Game;
    private GameClass GameController;

    private void Start()
    {
        GameObject Board = GameObject.FindGameObjectWithTag("BoardManager");
        Game = (GameConfig)Board.GetComponent(typeof(GameConfig));
        GameController = (GameClass)Board.GetComponent(typeof(GameClass));
        Game.LockGame = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Game.LockGame = false;
            if (Game.GameStatus == "WIN")
                GameController.UpdateLevel(false);
            else
                GameController.UpdateLevel(true);
            Destroy(GameObject.FindGameObjectWithTag("Win or Lose"));
        }
    }
}
