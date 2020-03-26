using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour
{

    private GameObject thisSquare;
    private Vector3 mousePos;

    void Start()
    {
        thisSquare = this.gameObject;
    }

  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (thisSquare.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
            {
                if (thisSquare.GetComponent<SpriteRenderer>().enabled == false)
                {
                    thisSquare.GetComponent<SpriteRenderer>().enabled = true;
                } else
                {
                    thisSquare.GetComponent<SpriteRenderer>().enabled = false;
                }
            } 
        }
    }
}
