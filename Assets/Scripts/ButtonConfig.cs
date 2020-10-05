using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConfig : MonoBehaviour
{
    public GameConfig gameConfig;
    public string color;
    public string buttontag;
    private SpriteRenderer render;
    public Sprite btnPressedColors;
    public Sprite btnNoButton;
    public Sprite btnColor;
    public bool disabled;
    private Delay delay;
    public int pressCount;
    public Text pressCountTxt;
    public AudioClip bPress;
    public AudioClip bReleased;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        disabled = true;
        delay = new Delay(0.5f);
        pressCount = 0;
        pressCountTxt.text = pressCount.ToString();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && disabled == false && gameConfig.GetCurrentColor() == "none" && gameConfig.LockGame == false)
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));

            if (hitCollider != null && hitCollider.CompareTag(buttontag))
            {
                delay.Reset();
                disabled = true;
                pressCount--;
                pressCountTxt.text = "";
                gameConfig.SetCurrentColor(color);
                render.sprite = btnPressedColors;
                gameObject.GetComponent<AudioSource>().PlayOneShot(bPress, 1.0F);
                Debug.Log("Color set: " + color);
            }
        }

        if (pressCount == 0)
        {
            render.sprite = btnNoButton;
            pressCountTxt.text = "";
        }

        if (delay.IsReady && disabled == true && render.sprite != btnNoButton && gameConfig.GetCurrentColor() == "none")
        {
            disabled = false;
            render.sprite = btnColor;
            pressCountTxt.text = pressCount.ToString();
            gameObject.GetComponent<AudioSource>().PlayOneShot(bReleased, 1.0F);
        }
    }
}
