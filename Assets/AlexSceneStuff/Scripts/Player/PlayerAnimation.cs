using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAni;
    private GameObject Player;

    private PlayerControllerMovement playerControll;
    private SpriteRenderer sprite;
    private PlayerDashButton playerD;
    private float degrees;
    private bool isPlayerMoving = false;
    private bool canPlayerMove = false;
    private bool isHeDashing = false;
    

    void Start()
    {
        Player = GameObject.Find("Player");
        playerAni = Player.GetComponent<Animator>();
        playerControll = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        sprite = Player.GetComponent<SpriteRenderer>();
        playerD = GameObject.Find("Canvas").GetComponent<PlayerDashButton>();

        playerAni.SetInteger("PlayerAnimation", 1);
    }

    void Update()
    {
        isPlayerMoving = playerControll.Ismoving;
        degrees = playerControll.playerDegrees;
        canPlayerMove = playerControll.canMove;
        isHeDashing = playerD.dashTiming;
        

        if (isPlayerMoving == true)
        {
            if (degrees >0  && degrees < 180)
            {
                if (isHeDashing == false)
                {
                    playerAni.SetInteger("PlayerAnimation", 4);
                } else
                {
                    playerAni.SetInteger("PlayerAnimation", 6);
                }

                if(degrees > 0 && degrees < 90)
                {
                    sprite.flipX = false;
                }

                if (degrees > 90 && degrees < 180)
                {
                    sprite.flipX = true;
                }
            }

            if (degrees < 360 && degrees > 180)
            {
                if (isHeDashing == false)
                {
                    playerAni.SetInteger("PlayerAnimation", 3);
                } else
                {
                    playerAni.SetInteger("PlayerAnimation", 7);
                }
                if (degrees < 360 && degrees > 270)
                {
                    sprite.flipX = false;
                }

                if (degrees < 270 && degrees > 180)
                {
                    sprite.flipX = true;
                }
            }
        
        } 

        if (isPlayerMoving == false && canPlayerMove == true)
        {

            if (degrees > 0 && degrees < 180)
            {
                playerAni.SetInteger("PlayerAnimation", 2);
                if (degrees > 0 && degrees < 90)
                {
                    sprite.flipX = false;
                }

                if (degrees > 90 && degrees < 180)
                {
                    sprite.flipX = true;
                }
            }

            if (degrees < 360 && degrees > 180)
            {
                playerAni.SetInteger("PlayerAnimation", 1);
                if (degrees < 260 && degrees > 270)
                {
                    sprite.flipX = false;
                }

                if (degrees < 270 && degrees > 180)
                {
                    sprite.flipX = true;
                }
            }
        }




    }
}
