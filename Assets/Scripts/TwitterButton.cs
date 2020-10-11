using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TwitterButton : MonoBehaviour
{
    const string Address = "http://twitter.com/intent/tweet";
    public GameConfig Game;

    public static void Share(string text, string url,
                             string related, string lang = "en")
    {
        Application.OpenURL(Address +
                            "?text=" + UnityWebRequest.EscapeURL(text) +
                            "&amp;url=" + UnityWebRequest.EscapeURL(url) +
                            "&amp;related=" + UnityWebRequest.EscapeURL(related) +
                            "&amp;lang=" + UnityWebRequest.EscapeURL(lang));
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && Game.LockGame == false)
        {
            Vector3 pos = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (hitCollider != null && hitCollider.CompareTag("Twitter Button"))
            {

                Share("I'm playing Dr. Bon Bon Puzzle and am at Level " + Game.Level + " with " + Game.Points + " points! Play the game here: https://randoman.itch.io/dr-bon-bon-puzzle #agbic #drbonbon", "", "", "en");
                Debug.Log("Twitter Button Pressed...");
            }
        }
    }
}
