using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashButton : MonoBehaviour
{
    // Als je dashed mag je alle kanten op gaan // 
    // Als je dashed moet je bij de "PlayerControllerMovement" script kijken of je wel of niet mag dashen // 

    // Player // 
    private bool canMove = true;
    private PlayerControllerMovement playerController;

    // Camera //
    private float camDashSpeed;
    private float normalCamSpeed;
    private FollowPlayer camP;

    // Touch //
    private Vector3 touchPos;

    // Dashing //
    private GameObject dashButton;
    private bool canIDash = false;
    private bool isDashing = false;
    private float dashSpeed;
    private float normalspeed;
    public bool dashTiming = false;

    // Timer // 
    private float firstTimer = 0;
    private float secondTimer = 0;
    private float timeToDash = 0.5f;
    private float timeAfterDash = 3f;
    


    void Start()
    {
        dashButton = GameObject.Find("DashButton");
        playerController = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        camP = GameObject.Find("Main Camera").GetComponent<FollowPlayer>();
        camDashSpeed = camP.camSpeed * 2;
        normalCamSpeed = camP.camSpeed;
        dashSpeed = playerController.playerSpeed * 2;
        normalspeed = playerController.playerSpeed;
    }

    void Update()
    {
        canMove = playerController.canMove;

        if (canMove == true)
        {
            // Check for input & if player can dash (needs to move) //
            if (Input.touchCount > 0)
            {
                canIDash = playerController.Ismoving;

                if (canIDash == true) 
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (isDashing == false)
                        {
                            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPos = new Vector3(touchPos.x, touchPos.y, dashButton.transform.position.z);

                            if (touch.phase == TouchPhase.Began)
                            {
                                if (dashButton.GetComponent<BoxCollider2D>().bounds.Contains(touchPos))
                                {
                                    playerController.playerSpeed = dashSpeed;
                                    camP.camSpeed = camDashSpeed;
                                    dashTiming = true;
                                    isDashing = true;
                                }
                            }
                        }
                    }
                }
            }

            if (isDashing == true)
            {
                if (firstTimer < timeToDash)
                {
                    firstTimer += Time.deltaTime;
                }

                if (firstTimer > timeToDash)
                {
                    dashTiming = false;
                    secondTimer += Time.deltaTime;
                    playerController.playerSpeed = normalspeed;
                    camP.camSpeed = normalCamSpeed;

                    if (secondTimer > timeAfterDash)
                    {
                        isDashing = false;
                        firstTimer = 0;
                        secondTimer = 0;

                    }
                }

            } else
            {
                isDashing = false;
                dashTiming = false;
                firstTimer = 0;
                secondTimer = 0;
                playerController.playerSpeed = normalspeed;
                camP.camSpeed = normalCamSpeed;
            }
        } else
        {
            dashTiming = false;
            isDashing = false;
            firstTimer = 0;
            secondTimer = 0;
            camP.camSpeed = normalCamSpeed;
            playerController.playerSpeed = normalspeed;
        }
    }
}
