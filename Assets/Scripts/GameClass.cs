﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClass : MonoBehaviour
{
    public List<Sprite> characters = new List<Sprite>();
    public GameObject tile;
    public int xSize, ySize;
    public GameConfig Game;
    public SolveBoard MatchButton;

    public Sprite yellowSprite;
    public Sprite greenSprite;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite graySprite;
    public Sprite yellowBtnSprite;
    public Sprite greenBtnSprite;
    public Sprite redBtnSprite;
    public Sprite blueBtnSprite;
    public Sprite grayBtnSprite;
    public Sprite btnMatch;

    public Text Speak;

    private GameObject[,] tiles;
    private float firstX = -8F;
    private float firstY = -3F;
    private float xOffset;
    private float yOffset;

    void Start()
    {
        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        xOffset = offset.x;
        yOffset = offset.y;
        CreateLevel();
    }

    private void CreateBoard() //Creates a regular board
    {
        tiles = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;

        Sprite[] previousLeft = new Sprite[ySize];
        Sprite previousBelow = null;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                tiles[x, y] = newTile;
                newTile.transform.parent = transform;

                List<Sprite> possibleCharacters = new List<Sprite>();
                possibleCharacters.AddRange(characters);

                possibleCharacters.Remove(previousLeft[y]);
                possibleCharacters.Remove(previousBelow);

                Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
                previousLeft[y] = newSprite;
                previousBelow = newSprite;
            }
        }
    }

    public void UpdateLevel(bool isSameLevel)
    {
        if (!isSameLevel)
        {
            if ((Game.Level + 1) <= Game.MaxLevel)
                Game.Level++;
        }
        CreateLevel();
    }

    private void CreateLevel()
    {
        int level = Game.GetCurrentLevel();
        ResetButtons();
        ClearBoard();
        MatchButton.ResetMatchingInterval();
        if (level == 0) { Tutorial(); }
        else if (level == 1) { Level1(); }
        else if (level == 2) { Level2(); }
        else if (level == 3) { Level3(); }
        else if (level == 4) { Level4(); }
        else if (level == 5) { Level5(); }
        else if (level == 6) { Level6(); }
    }

    void SetButton(string buttonTag, int buttonPressCount, Sprite btnSprite)
    {
        GameObject Button = GameObject.FindGameObjectWithTag(buttonTag);
        ButtonConfig BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = buttonPressCount;
        BtnCfg.pressCountTxt.text = BtnCfg.pressCount.ToString();
        BtnCfg.disabled = false;
        Button.GetComponent<SpriteRenderer>().sprite = btnSprite;
    }

    void ResetButtons()
    {
        GameObject Button = GameObject.FindGameObjectWithTag("solver");
        if (Button.GetComponent<SpriteRenderer>().sprite != btnMatch)
        {
            Button.GetComponent<SpriteRenderer>().sprite = btnMatch;
        }

        Game.SetCurrentColor("none");

        Button = GameObject.FindGameObjectWithTag("Red Button");
        ButtonConfig BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = 0;
        BtnCfg.pressCountTxt.text = "";
        BtnCfg.disabled = true;

        Button = GameObject.FindGameObjectWithTag("Blue Button");
        BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = 0;
        BtnCfg.pressCountTxt.text = "";
        BtnCfg.disabled = true;

        Button = GameObject.FindGameObjectWithTag("Green Button");
        BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = 0;
        BtnCfg.pressCountTxt.text = "";
        BtnCfg.disabled = true;

        Button = GameObject.FindGameObjectWithTag("Yellow Button");
        BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = 0;
        BtnCfg.pressCountTxt.text = "";
        BtnCfg.disabled = true;

        Button = GameObject.FindGameObjectWithTag("Gray Button");
        BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = 0;
        BtnCfg.pressCountTxt.text = "";
        BtnCfg.disabled = true;
    }

    void CreateBlock(float x, float y, Sprite sprColor)
    {
        GameObject newTile;
        newTile = Instantiate(tile, new Vector3(x, y, 0), transform.rotation);
        newTile.GetComponent<SpriteRenderer>().sprite = sprColor;
        Game.BlockCount++;
    }

    void CreateRow(string colors, int rowNumber, int startingCol)
    {
        int i = startingCol;
        foreach (char c in colors)
        {
            if (c == 'G') CreateBlock(firstX + (xOffset * i), firstY + (yOffset * rowNumber), greenSprite);
            else if (c == 'R') CreateBlock(firstX + (xOffset * i), firstY + (yOffset * rowNumber), redSprite);
            else if (c == 'Y') CreateBlock(firstX + (xOffset * i), firstY + (yOffset * rowNumber), yellowSprite);
            else if (c == 'B') CreateBlock(firstX + (xOffset * i), firstY + (yOffset * rowNumber), blueSprite);
            else if (c == 'X') CreateBlock(firstX + (xOffset * i), firstY + (yOffset * rowNumber), graySprite);
            i++;
        }
    }

    void ClearBoard()
    {
        GameObject[] blocks;

        blocks = GameObject.FindGameObjectsWithTag("gameblock");

        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }
        Game.BlockCount = 0;
        Game.BlocksDestroyed = 0;
    }

    void Tutorial()
    {
        Game.CurrentLevel.text = "TUTORIAL";

        if (Game.GameStatus != "LOSE")
            Game.LockGame = true;

        SetButton("Red Button", 1, redBtnSprite);

        CreateRow("BGGRGGY", 0, 0);
        CreateRow("BRBRY", 1, 1);
        CreateRow("BRY", 2, 2);
        CreateRow("G", 3, 3);
        Debug.Log("Tutorial block count: " + Game.BlockCount);
    }

    void Level1()
    {
        Game.CurrentLevel.text = "LEVEL 1";
        Speak.text = "Are you up to the challenge?";

        SetButton("Green Button", 1, greenBtnSprite);
        SetButton("Red Button", 1, redBtnSprite);

        CreateRow("BRRGGY", 0, 0);
        CreateRow("BBYRYY", 1, 0);
        CreateRow("RGRYY", 2, 0);
        CreateRow("B-BY", 3, 0);
        CreateRow("B--B", 4, 0);
        Debug.Log("Level 1 block count: " + Game.BlockCount);
    }

    void Level2() //TODO
    {
        Game.CurrentLevel.text = "LEVEL 2";
        Speak.text = "Don't panic! Take it easy.";

        SetButton("Green Button", 2, greenBtnSprite);
        SetButton("Red Button", 2, redBtnSprite);
        SetButton("Yellow Button", 2, yellowBtnSprite);

        CreateRow("GRGBGRG", 0, 0);
        CreateRow("RYR-RYR", 1, 0);
        CreateRow("-RY-YR", 2, 0);
        CreateRow("-G---G", 3, 0);

        Debug.Log("Level 2 block count: " + Game.BlockCount);
    }

    void Level3()
    {
        Game.CurrentLevel.text = "LEVEL 3";
        Speak.text = "I know you can do it! Meet me at Level 4, ok?";

        SetButton("Yellow Button", 2, yellowBtnSprite);
        SetButton("Red Button", 2, redBtnSprite);
        SetButton("Blue Button", 2, blueBtnSprite);

        CreateRow("YBYBBYBY", 0, 0);
        CreateRow("-YRYYRY", 1, 0);
        CreateRow("-YY--YY", 2, 0);
        CreateRow("-R----R", 3, 0);
        Debug.Log("Level 3 block count: " + Game.BlockCount);
    }

    void Level4()
    {
        Game.CurrentLevel.text = "LEVEL 4";
        Speak.text = "You made it! I'm so proud!";

        SetButton("Yellow Button", 3, yellowBtnSprite);
        SetButton("Red Button", 2, redBtnSprite);
        SetButton("Green Button", 1, greenBtnSprite);

        CreateRow("YRBRY", 0, 0);
        CreateRow("GBRYG", 1, 0);
        CreateRow("BRGG", 2, 0);
        CreateRow("RY", 3, 0);
        CreateRow("YG", 4, 0);
        CreateRow("G", 5, 0);
        Debug.Log("Level 4 block count: " + Game.BlockCount);
    }

    void Level5() //TODO, sem button gray, apenas blocos
    {
        Game.CurrentLevel.text = "LEVEL 5";
        Speak.text = "Gray blocks vanish when you push the Match Button. Stay aware!";

        
        Debug.Log("Level 5 block count: " + Game.BlockCount);
    }

    void Level6()
    {
        Game.CurrentLevel.text = "LEVEL 6";
        Speak.text = "You know you don't have to press all the buttons, right?";

        SetButton("Green Button", 1, greenBtnSprite);
        SetButton("Red Button", 2, redBtnSprite);
        SetButton("Blue Button", 1, blueBtnSprite);

        CreateRow("YYBGGBRR", 0, 0);
        CreateRow("BBYRBRRG", 1, 0);
        CreateRow("-RBRRBBG", 2, 0);
        CreateRow("--R-BRBY", 3, 0);
        CreateRow("----BYYG", 4, 0);
        CreateRow("-----R--", 5, 0);
        CreateRow("-----R--", 6, 0);
        CreateRow("-----B--", 7, 0);
        Debug.Log("Level 6 block count: " + Game.BlockCount);
    }
}
