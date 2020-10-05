using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClass : MonoBehaviour
{
    public GameConfig gameConfig;
    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite yellowSprite;
    public Sprite blueSprite;
    public Sprite graySprite;

    private SpriteRenderer render;
    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private AudioSource audioData;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameObject Board = GameObject.FindGameObjectWithTag("BoardManager");
        gameConfig = (GameConfig)Board.GetComponent(typeof(GameConfig));
        audioData = (AudioSource)Board.GetComponent(typeof(AudioSource));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameConfig.LockGame == false)
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.gameObject.transform == transform)
            {
                if (gameConfig.GetCurrentColor() == "red" && render.sprite != redSprite)
                {
                    render.sprite = redSprite;
                    gameConfig.SetCurrentColor("none");
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (gameConfig.GetCurrentColor() == "yellow" && render.sprite != yellowSprite)
                {
                    render.sprite = yellowSprite;
                    gameConfig.SetCurrentColor("none");
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (gameConfig.GetCurrentColor() == "green" && render.sprite != greenSprite)
                {
                    render.sprite = greenSprite;
                    gameConfig.SetCurrentColor("none");
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (gameConfig.GetCurrentColor() == "blue" && render.sprite != blueSprite)
                {
                    render.sprite = blueSprite;
                    gameConfig.SetCurrentColor("none");
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else if (gameConfig.GetCurrentColor() == "gray" && render.sprite != graySprite)
                {
                    render.sprite = graySprite;
                    gameConfig.SetCurrentColor("none");
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        //Debug.DrawRay(transform.position, castDir, Color.green);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name + ", " + hit.collider.gameObject.transform.position.x + ", " + hit.collider.gameObject.transform.position.y);
            return hit.collider.gameObject;
        }
        return null;
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        //Debug.Log("=================================BLOCK=================================");
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir, 1.0F);
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir, 1.0F);
        }
        return matchingTiles;
    }

    private void ClearMatch(Vector2[] paths)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++) { matchingTiles.AddRange(FindMatch(paths[i])); }
        if (matchingTiles.Count >= 2)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                gameConfig.BlocksDestroyed++;
                gameConfig.calculatedPoints += 50;
                Destroy(matchingTiles[i]);
            }
            audioData.Play();
            matchFound = true;
        }
    }

    private bool matchFound = false;
    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            matchFound = false;
            gameConfig.BlocksDestroyed++;
            Debug.Log("Blocks destroyed: " + gameConfig.BlocksDestroyed);
            Destroy(this.gameObject);
        }
    }

    public void ClearIfGray()
    {
        if (render.sprite == graySprite)
        {
            Destroy(this.gameObject);
        }
    }
}
