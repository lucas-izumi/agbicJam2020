using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject BlueBlock;
    public GameObject GreenBlock;
    public GameObject YellowBlock;
    public GameObject RedBlock;
 
    // Start is called before the first frame update
    void Start()
    {
        Level1();
    }

    void Level1()
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
