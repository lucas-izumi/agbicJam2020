using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text Speak;
    public GameConfig gameConfig;
    private int clickCount;
    private SpriteRenderer render;
    private GameObject arrows;
    public GameObject objArrow;
    public GameObject objArrowDown;

    private void Start()
    {
        clickCount = 0;
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickCount < 16)
        {
            if (clickCount == 0)
                Speak.text = "It seems it's your first time here, hiehiehie.";
            else if (clickCount == 1)
                Speak.text = "I'm Dr. Bon Bon, it's a pleasure to meet you!";
            else if (clickCount == 2)
                Speak.text = "I'm here to challenge your brain!";
            else if (clickCount == 3)
                Speak.text = "Your goal is to clear the board...";
            else if (clickCount == 4)
                Speak.text = "... but this ain't no regular Match-3 game!";
            else if (clickCount == 5)
                Speak.text = "You can paint ANY block on the board with any of the colors.";
            else if (clickCount == 6)
                Speak.text = "I mean, if the buttons on the panel allow it, hiehiehie.";
            else if (clickCount == 7)
            {
                arrows = Instantiate(objArrow, new Vector3(0.49F, 1.91F, 0), transform.rotation);
                Speak.text = "See that red button with a number on it?";
            }
            else if (clickCount == 8)
                Speak.text = "That's how many times you can paint a block with that color.";
            else if (clickCount == 9)
                Speak.text = "Once you click a button you are set!";
            else if (clickCount == 10)
            {
                Destroy(arrows);
                arrows = Instantiate(objArrowDown, new Vector3(-5.15F, 0F, 0), transform.rotation);
                Speak.text = "Now you just have to click one of the blocks to paint it!";
            }
            else if (clickCount == 11)
                Speak.text = "You can paint as many blocks as the buttons lets you!";
            else if (clickCount == 12)
            {
                Destroy(arrows);
                arrows = Instantiate(objArrowDown, new Vector3(6.62F, -0.79F, 0), transform.rotation);
                Speak.text = "Once you are done just click the Match Button!";
            }
            else if (clickCount == 13)
                Speak.text = "Now, enough talking, you are on your own!";
            else if (clickCount == 14)
                Speak.text = "I'll keep watching from here, hiehiehie.";
            else if (clickCount == 15)
            {
                Destroy(arrows);
                gameConfig.TutorialMode = false;
                Speak.text = "EXPERIMENT START!!!";
                //render.sprite = null;
            }

            clickCount++;
        }
    }
}
