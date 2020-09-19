using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveBoard : MonoBehaviour
{
    public string buttontag;
    public GameConfig gameConfig;
    private bool startMatching;
    private int blockCount;
    private Delay delay;

    private void Start()
    {
        blockCount = GameObject.FindGameObjectsWithTag("gameblock").Length;
        startMatching = false;
        delay = new Delay(1.0f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.CompareTag(buttontag))
            {
                startMatching = true;
                delay.Reset();
            }
        }

        if (startMatching)
        {
            if (delay.IsReady)
            {
                MatchAllBlocks();
                delay.Reset();
            }
        }
    }

    private void MatchAllBlocks()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("gameblock"))
        {
            if (fooObj.name == "Block(Clone)")
            {
                BlockClass solver = (BlockClass)fooObj.GetComponent(typeof(BlockClass));
                solver.ClearIfGray();
                solver.ClearAllMatches();
            }
        }
    }

    /*private float CheckStillBlocks()
    {
        float stillBlocks = 0F;
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("gameblock"))
        {
            if (fooObj.name == "Block(Clone)")
            {
                stillBlocks += fooObj.GetComponent<Rigidbody2D>().velocity.magnitude;
            }
            
        }
        return stillBlocks;
    }*/
}
