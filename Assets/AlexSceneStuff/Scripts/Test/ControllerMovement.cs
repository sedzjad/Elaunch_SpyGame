using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    private GameObject backPlace;
    private GameObject stickPlace;
    private GameObject Player;
    private bool touchingStick = false;
    private Vector2 stickPos;
    private Vector3 backPos;
    private Vector3 mousePos;
    private float maxDinstance;
    public Vector2 moveAngle;
    public float speed = 10f;
    private Rigidbody2D rb2D;
    Touch touch;
    private Vector3 Testvec;
    public float something;


    void Start()
    {
        backPlace = GameObject.Find("Achterkant");
        stickPlace = GameObject.Find("Voorkant");
        Player = GameObject.Find("Bob");
        maxDinstance = backPlace.transform.localScale.x * GameObject.Find("Canvas").GetComponent<RectTransform>().transform.localScale.x / 3 * 2;

        stickPos = stickPlace.transform.position;
        backPos = backPlace.transform.position;


    }


    void Update()
    {
        stickPos = stickPlace.transform.position;
        backPos = backPlace.transform.position;
        rb2D = Player.GetComponent<Rigidbody2D>();

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
        {
            Testvec = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(Testvec);
            mousePos = new Vector3(mousePos.x, mousePos.y, backPos.z);



                if (backPlace.GetComponent<CircleCollider2D>().bounds.Contains(mousePos))
                {
                    touchingStick = true;
                }

                if (touchingStick == true)
                {

                    float posDistance = Vector2.Distance(mousePos, backPos);
                    if (posDistance >= maxDinstance)
                    {
                        posDistance = maxDinstance;
                    }

                    stickPos = backPos + new Vector3(mousePos.x - backPos.x, mousePos.y - backPos.y).normalized * posDistance;

                    stickPlace.transform.position = new Vector3(stickPos.x, stickPos.y, -1);

                    something = Mathf.Atan2(stickPos.y - backPos.y, stickPos.x - backPos.x) * (180 / Mathf.PI);

                    moveAngle.x = Mathf.Cos(Vector2.Angle(stickPos, backPos)) / speed;
                    moveAngle.y = Mathf.Sin(Vector2.Angle(stickPos, backPos)) / speed;

                    Player.GetComponent<Rigidbody2D>().velocity = moveAngle;
                    rb2D.AddForce(Quaternion.Euler(0, 0, something) * new Vector3(speed, 0, 0) * Time.deltaTime);

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (touchingStick == true)
                {
                    touchingStick = false;
                    stickPlace.transform.position = new Vector3(backPos.x, backPos.y, -1);
                }
            }

            if (touchingStick == false)
            {
                rb2D.velocity = Vector3.zero;
            }
        }



        if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                mousePos = new Vector3(mousePos.x, mousePos.y, backPos.z);

                if (touch.phase == TouchPhase.Began)
                {
                    if (backPlace.GetComponent<CircleCollider2D>().bounds.Contains(mousePos))
                    {
                        touchingStick = true;
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (touchingStick == true)
                    {
                        stickPos = mousePos;

                        float posDistance = Vector2.Distance(mousePos, backPos);

                        if (posDistance >= maxDinstance)
                        {
                            posDistance = maxDinstance;
                        }
                        stickPos = backPos + new Vector3(mousePos.x - backPos.x, mousePos.y - backPos.y).normalized * posDistance;


                        stickPlace.transform.position = new Vector3(stickPos.x, stickPos.y, -1);

                        something = Mathf.Atan2(stickPos.y - backPos.y, stickPos.x - backPos.x) * (180 / Mathf.PI);

                        moveAngle.x = Mathf.Cos(Vector2.Angle(stickPos, backPos)) / speed;
                        moveAngle.y = Mathf.Sin(Vector2.Angle(stickPos, backPos)) / speed;

                        Player.GetComponent<Rigidbody2D>().velocity = moveAngle;
                        rb2D.AddForce(Quaternion.Euler(0, 0, something) * new Vector3(speed, 0, 0) * Time.deltaTime);
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    touchingStick = false;
                    stickPlace.transform.position = new Vector3(backPos.x, backPos.y, -1);
                }


            }
        }



    }
}
