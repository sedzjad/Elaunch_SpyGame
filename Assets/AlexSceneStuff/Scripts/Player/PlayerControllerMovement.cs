using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMovement : MonoBehaviour
{
    
    // Objects //
    private GameObject biggerCircle;
    private GameObject smallerCircle;
    private float maxStickDistance;
    public Vector3 biggerPos;
    public Vector3 smallerPos;
    private Vector3 testPos;

    // Touching List // 
    private Touch[] touchList = new Touch[10];
    private bool[] missedTouch = new bool[] { false, false, false, false, false, false, false, false, false, false };
    private bool[] isTouching = new bool[] { false, false, false, false, false, false, false, false, false, false };

    public Vector3 mousePos;
    private bool didAlreadyTouch = false;
    private int isNowId;
    public bool onetime = false;

    // Player Movement // 
    public bool canMove = true;
    private GameObject Player;
    public float playerSpeed = 200;
    private Vector2 movingAngle;
    public float playerDegrees;
    private Rigidbody2D playerRigid;
    public bool Ismoving = false;


    void Start()
    {
        // Declare objects // 
        biggerCircle = GameObject.Find("Control_Circle");
        smallerCircle = GameObject.Find("Control_Stick");
        Player = GameObject.Find("Player");
        playerRigid = Player.GetComponent<Rigidbody2D>();

        // Calculate max distance stick may travel // 
        maxStickDistance = biggerCircle.transform.localScale.x * GameObject.Find("Canvas").GetComponent<RectTransform>().transform.localScale.x * 3 / 2;
    }


    void Update()
    {
        smallerPos = smallerCircle.transform.position;
        biggerPos = biggerCircle.transform.position;

        // Set everything back when nothing touches the screen (reset) // 
        if (Input.touchCount == 0)
        {
            Ismoving = false;
            for (int i = 0; i < 5; i++)
            {
                missedTouch[i] = false;
                isTouching[i] = false;
            }
            didAlreadyTouch = false;
            smallerCircle.transform.position = new Vector3(biggerPos.x, biggerPos.y, -1);
            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        if (canMove == true)
        {
            // Checks touch inputs // 
            if (Input.touchCount > 0)
            {


                // Check for inputs // 
                foreach (Touch touch in Input.touches)
                {
                    int i = touch.fingerId;
                    touchList[i] = touch;

                    // Did i already touched controller? // 
                    if (didAlreadyTouch == false)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(touchList[i].position);
                        mousePos = new Vector3(mousePos.x, mousePos.y, biggerPos.z);

                        if (touchList[i].phase == TouchPhase.Began)
                        {
                            if (biggerCircle.GetComponent<CircleCollider2D>().bounds.Contains(mousePos))
                            {
                                missedTouch[i] = false;
                                isTouching[i] = true;
                                isNowId = touchList[i].fingerId;
                                Ismoving = true;

                                MovePlayer();

                                didAlreadyTouch = true;
                            }
                        }
                    }

                    // Checks for missed touches // 
                    if (biggerCircle.GetComponent<CircleCollider2D>().bounds.Contains(testPos) == false && i != isNowId)
                    {
                        missedTouch[i] = true;
                    }

                    if (biggerCircle.GetComponent<CircleCollider2D>().bounds.Contains(testPos) && didAlreadyTouch == true && i != isNowId)
                    {
                        missedTouch[i] = true;
                    }

                    // If touched controller is moving // 
                    if (didAlreadyTouch == true && isNowId == touchList[i].fingerId)
                    {
                        if (touchList[isNowId].phase == TouchPhase.Moved)
                        {
                            if (missedTouch[isNowId] == false)
                            {
                                mousePos = Camera.main.ScreenToWorldPoint(touchList[isNowId].position);
                                mousePos = new Vector3(mousePos.x, mousePos.y, biggerCircle.transform.position.z);

                                MovePlayer();
                            }
                        }
                    }

                    // When touched has ended check if its the controller touch or other touch // 
                    if (touchList[i].phase == TouchPhase.Ended)
                    {
                        if (missedTouch[i] == false)
                        {
                            didAlreadyTouch = false;
                            smallerCircle.transform.position = new Vector3(biggerPos.x, biggerPos.y, -1);
                            isTouching[i] = false;
                            isNowId = 10;
                            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                            Ismoving = false;
                        }
                        missedTouch[i] = false;
                    }
                }
            }
        } else
        {
            Ismoving = false;
            for (int i = 0; i < 5; i++)
            {
                missedTouch[i] = false;
                isTouching[i] = false;
            }
            didAlreadyTouch = false;
            smallerCircle.transform.position = new Vector3(biggerPos.x, biggerPos.y, -1);
            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }


    private void MovePlayer()
    {
        

        float posDistance = Vector2.Distance(mousePos, biggerPos);
        if (posDistance >= maxStickDistance)
        {
            posDistance = maxStickDistance;
        }

        smallerPos = biggerPos + new Vector3(mousePos.x - biggerPos.x, mousePos.y - biggerPos.y).normalized * posDistance;

        smallerCircle.transform.position = new Vector3(smallerPos.x, smallerPos.y, -1);

        var Scircle = (smallerPos - biggerPos).normalized;
        var Bcircle = biggerPos - biggerPos;

        /// Player Movement//
        playerDegrees = Mathf.Atan2(Scircle.y - Bcircle.y, Scircle.x - Bcircle.x) * (180 / Mathf.PI);
        if (Scircle.y < Bcircle.y)
        {
            playerDegrees += 360;
        }

        // Get moving angles //
        movingAngle.x = Mathf.Cos(Vector2.Angle(Scircle, Bcircle)) / playerSpeed;
        movingAngle.y = Mathf.Sin(Vector2.Angle(Scircle, Bcircle)) / playerSpeed;

        // Set velocity angle // 
        Player.GetComponent<Rigidbody2D>().velocity = movingAngle;

        playerRigid.AddForce(Quaternion.Euler(0, 0, playerDegrees) * new Vector3(playerSpeed, 0, 0));
    }
}