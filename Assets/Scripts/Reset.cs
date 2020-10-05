using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameConfig Game;
    public GameClass GameController;
    private SpriteRenderer render;
    public Sprite btnPressedSprite;
    public Sprite btnSprite;
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
            if (hitCollider != null && hitCollider.CompareTag("Reset Button"))
            {
                render.sprite = btnPressedSprite;                
                delay.Reset();
                gameObject.GetComponent<AudioSource>().Play();
                GameController.UpdateLevel(true);
            }
        }

        if (delay.IsReady)
        {
            render.sprite = btnSprite;
        }
    }
}
