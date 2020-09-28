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
    private bool disabled;
    private Delay delay;
    public int pressCount;
    public Text pressCountTxt;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        disabled = false;
        delay = new Delay(0.5f);
        pressCount = 3; //MOCK, quem deve gerar esse numero é o gerador de levels
        pressCountTxt.text = pressCount.ToString();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && disabled == false && gameConfig.GetCurrentColor() == "none")
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
                //Debug.Log("Color set: " + color);
            }

            if (pressCount == 0)
            {
                render.sprite = btnNoButton;
                pressCountTxt.text = "";
            }
        }

        if (delay.IsReady && disabled == true && render.sprite != btnNoButton && gameConfig.GetCurrentColor() == "none")
        {
            disabled = false;
            render.sprite = btnColor;
            pressCountTxt.text = pressCount.ToString();
        }
    }
}
