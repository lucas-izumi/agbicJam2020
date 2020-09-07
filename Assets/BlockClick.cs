using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClick : MonoBehaviour
{
    public GameConfig gameConfig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.gameObject.transform == transform)
            {
                if (gameConfig.GetCurrentColor() == "red")
                {
                    Instantiate(GameObject.Find("Red Block"), new Vector2(transform.position.x, transform.position.y), transform.rotation);
                    Destroy(this.gameObject);
                }
                gameConfig.SetCurrentColor("none");
            }
        }
    }
}
