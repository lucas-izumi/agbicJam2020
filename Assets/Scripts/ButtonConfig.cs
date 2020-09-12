using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConfig : MonoBehaviour
{
    public GameConfig gameConfig;
    public string color;
    public string buttontag;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.CompareTag(buttontag))
            {
                gameConfig.SetCurrentColor(color);
                //Debug.Log("Color set: " + color);
            }
        }
    }
}
