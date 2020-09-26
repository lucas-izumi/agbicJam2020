using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConfig : MonoBehaviour
{
    public GameConfig gameConfig;
    public string color;
    public string buttontag;
    private SpriteRenderer render;
    public Sprite btnPressedColors;
    public Sprite btnNoButton;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.CompareTag(buttontag))
            {
                gameConfig.SetCurrentColor(color);
                render.sprite = btnPressedColors;
                //Debug.Log("Color set: " + color);
            }
        }
    }
}
