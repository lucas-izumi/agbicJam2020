using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClass : MonoBehaviour
{
    /*public GameObject BlueBlock;
    public GameObject GreenBlock;
    public GameObject YellowBlock;
    public GameObject RedBlock;*/

    public static GameClass instance;
    public List<Sprite> characters = new List<Sprite>();
    public GameObject tile;
    public int xSize, ySize;

    private GameObject[,] tiles;

    /*void Level1()
    {
        BlueBlock = GameObject.Find("Blue Block");
        GreenBlock = GameObject.Find("Green Block");
        YellowBlock = GameObject.Find("Yellow Block");
        RedBlock = GameObject.Find("Red Block");
        GameObject BlockColor;

        for (int j = 0; j < 8; ++j)
        {
            if (j < 2) BlockColor = BlueBlock;
            else if (j < 4) BlockColor = RedBlock;
            else if (j < 6) BlockColor = YellowBlock;
            else BlockColor = GreenBlock;

            for (int i = 0; i < 8; ++i)
            {
                Instantiate(BlockColor, new Vector3(-7.91F + i, 5 + j, 0), transform.rotation);
            }
        }
    }*/

    void Start()
    {
        instance = GetComponent<GameClass>();

        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }

    private void CreateBoard(float xOffset, float yOffset)
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
