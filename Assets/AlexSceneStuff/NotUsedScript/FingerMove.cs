using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMove : MonoBehaviour
{
    private GameObject blueSquare; 
    private GameObject blueCircle; 
    private Vector3 startPos = new Vector3(0.95f, -7.66f, -1.67f);
    private Vector3 endpos = new Vector3(11.2f, -7.66f, -1.67f);
    private bool iTouchedIt = false;
    private Vector2 mousePos;
    Touch touch;

    private void Start()
    {
        // Get objects
        blueSquare = GameObject.Find("Square");
        blueCircle = GameObject.Find("Circle");   
    }


    void FixedUpdate()
    {
        //Get position from circle
        Vector3 CirclePos = blueCircle.transform.position;

        // If there is a touch on the screen
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        // If there is a touch on the screen
        if (Input.GetMouseButton(0))
        {
            Vector2 Testvec = touch.position;
            mousePos = Camera.main.ScreenToWorldPoint(Testvec);
        }

        // If there is a touch on the screen
        if (Input.touchCount > 0)
        {
            // When touched
            if (touch.phase == TouchPhase.Began)
            {
  
                if (blueCircle.GetComponent<CircleCollider2D>().bounds.Contains(mousePos))
                {
                    GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "ON CIRCLE";
                    iTouchedIt = true;
                    float NewX = mousePos.x;
                    CirclePos = new Vector3(NewX, CirclePos.y, CirclePos.z);
                }
            }

            
            // If still touching
            if (touch.phase == TouchPhase.Moved)
            {
                if (iTouchedIt == true)
                {
                    float NewX = mousePos.x;
                    CirclePos = new Vector3(NewX, CirclePos.y, CirclePos.z);
                    GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "MOVING OBJECT";
                }

            }

            // If touch ended
            if (touch.phase == TouchPhase.Ended)
            {
                GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "OF IT";
                iTouchedIt = false;
            }
            
        }

        
        // If circle is standing on Endpos, make cube flip
        if (CirclePos == endpos)
        {
            blueSquare.transform.Rotate(0, 0, 1);
        }

        // Smaller than endPos
        if (CirclePos.x < startPos.x)
        {
            CirclePos = startPos;
        }

        // Bigger than endPos
        if (CirclePos.x > endpos.x)
        {
            CirclePos = endpos;
        }
        

        //Give back the position to circle
        blueCircle.transform.position = CirclePos;
        

    }
}
