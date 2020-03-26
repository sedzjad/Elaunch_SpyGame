using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    private Touch touch;

    private GameObject thisSquare;
    private Vector3 mousePos;
    private bool oneClick = false;

    void Start()
    {
        // Gets gameobjects
        thisSquare = this.gameObject;
    }

 
    void Update()
    {
        // Checks touch inputs
        if (Input.touchCount > 0)
        {
            // Sets input to touch
            touch = Input.GetTouch(0);

            // When touched screen
            if (touch.phase == TouchPhase.Began)
            {
                // Check only 1 click
                if (oneClick == false)
                {
                    // Get touch pos
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    mousePos.z = 0;

                    // Makes objects change invis and back
                    if (thisSquare.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                    {
                        if (thisSquare.GetComponent<SpriteRenderer>().enabled == false)
                        {
                            thisSquare.GetComponent<SpriteRenderer>().enabled = true;
                        }
                        else
                        {
                            thisSquare.GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                    oneClick = true;
                }
            }

            // When touch is up from screen
            if (touch.phase == TouchPhase.Ended)
            {
                oneClick = false;
            }
        }
    
    }
}
