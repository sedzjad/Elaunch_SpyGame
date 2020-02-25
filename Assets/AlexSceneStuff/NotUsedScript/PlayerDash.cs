using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // If player is moving & wants to dash -> player movement stops for ??* sec,
    // Check the player movement speed in ControllerMovement.cs
    // Check what way player is looking at by getting the last angle its looking at in ControllerMovement.cs
    // Based on the player lookAt & Movement speed it will dash to a specific place for a few* seconds.
    
    // Player dash has to remove movement to dash. Dashing can be done while walking or while not walking. Not sure on
    // if we want to dash when not walking but will be looked at later.

    // Candash has to be false on every animation and when getting caught. This means that when movement = false, dashing is also false.
    // Candash is also false when the player has no angle to look at
    
    // *Player dash animation is still not decided. So for now we make a timer for how long the player dashes, this will later change
    // based on the animation.

    private float playerMoveSpeed;
    private float timeDashing = 1f;

    private GameObject Player;
    private GameObject dashButton;
    private Vector3 dashPos;

    private bool canDash = true;
    private bool wantToDash = false;

    private Vector3 clickPos;
    Touch touch;
    private Vector3 mousePos;

    private float playerLookAtAngle;
    private float timePassed = 0;

    private ControllerMovement playerStats;
    private Rigidbody2D rb2D;

    void Start()
    {
        playerStats = GetComponent<ControllerMovement>();

        playerMoveSpeed = playerStats.speed;
        
        dashButton = GameObject.Find("DashButton");
        dashPos = dashButton.transform.position;

        Player = GameObject.Find("Bob");
        rb2D = Player.GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        playerLookAtAngle = playerStats.something;

            if (canDash == true)
            {

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    if (Input.GetMouseButton(0))
                    {
                        clickPos = Input.mousePosition;
                        mousePos = Camera.main.ScreenToWorldPoint(clickPos);
                        mousePos = new Vector3(mousePos.x, mousePos.y, dashPos.z);

                    if (dashButton.GetComponent<CircleCollider2D>().bounds.Contains(mousePos))
                        {
                            if (wantToDash == false)
                            {
                                wantToDash = true;
                        }
                        }

                    }
                }

           
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {

                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    mousePos = new Vector3(mousePos.x, mousePos.y, dashPos.z);

                    if (dashButton.GetComponent<CircleCollider2D>().bounds.Contains(mousePos))
                    {
                        if (wantToDash == false)
                        {
                            wantToDash = true;
                            
                        }
                    }
                }
            }
            
            
        }

        if (wantToDash == true)
        {
            timePassed += Time.deltaTime;

            if (timePassed < 0.2)
            {
                rb2D.AddForce(Quaternion.Euler(0, 0, playerLookAtAngle) * new Vector3(playerMoveSpeed * 3, 0, 0) * Time.deltaTime);
            } else
            {
                timePassed = 0;
                wantToDash = false;
            }
        }
    }
}
