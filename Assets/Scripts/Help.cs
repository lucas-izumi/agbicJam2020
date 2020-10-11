using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameConfig Game;
    public GameClass GameController;
    private SpriteRenderer render;
    public Sprite btnPressedSprite;
    public Sprite btnSprite;
    public GameObject HelpScreen;
    private Delay delay;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        delay = new Delay(0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && render.sprite != btnPressedSprite && Game.LockGame == false)
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.CompareTag("Help Button"))
            {
                render.sprite = btnPressedSprite;
                delay.Reset();
                Instantiate(HelpScreen, new Vector3(0, 0, 0), transform.rotation);
            }
        }

        if (delay.IsReady)
        {
            render.sprite = btnSprite;
        }
    }
}
