using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClass : MonoBehaviour
{
    public static GameClass instance;
    public List<Sprite> characters = new List<Sprite>();
    public GameObject tile;
    public int xSize, ySize;
    private GameConfig Game;

    public Sprite yellowSprite;
    public Sprite greenSprite;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite graySprite;

    private GameObject[,] tiles;
    private float firstX = -8F;
    private float firstY = -3F;

    void Tutorial(float xOffset, float yOffset)
    {
        Game.CurrentLevel.text = "TUTORIAL";

        SetButton("Red Button", 1);

        CreateBlock(firstX, firstY, blueSprite);
        CreateBlock(firstX + (xOffset * 1), firstY, greenSprite);
        CreateBlock(firstX + (xOffset * 2), firstY, greenSprite);
        CreateBlock(firstX + (xOffset * 3), firstY, redSprite);
        CreateBlock(firstX + (xOffset * 4), firstY, greenSprite);
        CreateBlock(firstX + (xOffset * 5), firstY, greenSprite);
        CreateBlock(firstX + (xOffset * 6), firstY, yellowSprite);

        CreateBlock(firstX + (xOffset * 1), firstY + (yOffset * 1), blueSprite);
        CreateBlock(firstX + (xOffset * 2), firstY + (yOffset * 1), redSprite);
        CreateBlock(firstX + (xOffset * 3), firstY + (yOffset * 1), blueSprite);
        CreateBlock(firstX + (xOffset * 4), firstY + (yOffset * 1), redSprite);
        CreateBlock(firstX + (xOffset * 5), firstY + (yOffset * 1), yellowSprite);

        CreateBlock(firstX + (xOffset * 2), firstY + (yOffset * 2), blueSprite);
        CreateBlock(firstX + (xOffset * 3), firstY + (yOffset * 2), redSprite);
        CreateBlock(firstX + (xOffset * 4), firstY + (yOffset * 2), yellowSprite);

        CreateBlock(firstX + (xOffset * 3), firstY + (yOffset * 3), greenSprite);
    }

    void SetButton(string buttonTag, int buttonPressCount)
    {
        GameObject Button = GameObject.FindGameObjectWithTag(buttonTag);
        ButtonConfig BtnCfg = (ButtonConfig)Button.GetComponent(typeof(ButtonConfig));
        BtnCfg.pressCount = buttonPressCount;
        BtnCfg.pressCountTxt.text = BtnCfg.pressCount.ToString();
        BtnCfg.disabled = false;
    }

    void CreateBlock(float x, float y, Sprite sprColor)
    {
        GameObject newTile;
        newTile = Instantiate(tile, new Vector3(x, y, 0), transform.rotation);
        newTile.GetComponent<SpriteRenderer>().sprite = sprColor;
    }

    void Start()
    {
        instance = GetComponent<GameClass>();
        GameObject Board = GameObject.FindGameObjectWithTag("BoardManager");
        Game = (GameConfig)Board.GetComponent(typeof(GameConfig));

        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        //CreateBoard(offset.x, offset.y);
        Tutorial(offset.x, offset.y);
    }

    private void CreateBoard(float xOffset, float yOffset) //Creates a regular board
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
}
