using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveBoard : MonoBehaviour
{
    public string buttontag;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.CompareTag(buttontag))
            {
                foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("gameblock"))
                {
                    if (fooObj.name == "Block(Clone)")
                    {
                        BlockClass solver = (BlockClass)fooObj.GetComponent(typeof(BlockClass));
                        solver.ClearAllMatches();
                    }
                }
                
            }
        }
    }
}
