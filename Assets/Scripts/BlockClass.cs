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

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameObject Board = GameObject.FindGameObjectWithTag("BoardManager");
        gameConfig = (GameConfig)Board.GetComponent(typeof(GameConfig));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.gameObject.transform == transform)
            {
                SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                if (gameConfig.GetCurrentColor() == "red")
                {
                    spriteRenderer.sprite = redSprite;
                }
                else if (gameConfig.GetCurrentColor() == "yellow")
                {
                    spriteRenderer.sprite = yellowSprite;
                }
                else if (gameConfig.GetCurrentColor() == "green")
                {
                    spriteRenderer.sprite = greenSprite;
                }
                else if (gameConfig.GetCurrentColor() == "blue")
                {
                    spriteRenderer.sprite = blueSprite;
                }
                else if (gameConfig.GetCurrentColor() == "gray")
                {
                    spriteRenderer.sprite = graySprite;
                }
                gameConfig.SetCurrentColor("none");
            }
        }
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        Debug.DrawRay(transform.position, castDir, Color.green);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
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
                Destroy(matchingTiles[i]);
            }
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
            render.sprite = null;
            matchFound = false;
        }
    }
}
