using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHelp : MonoBehaviour
{
    public GameConfig Game;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Board = GameObject.FindGameObjectWithTag("BoardManager");
        Game = (GameConfig)Board.GetComponent(typeof(GameConfig));
        Game.LockGame = true;
        Debug.Log("ENTREI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Game.LockGame = false;
            Destroy(GameObject.FindGameObjectWithTag("Help Screen"));
        }
    }
}
