using System.Collections;
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
}
