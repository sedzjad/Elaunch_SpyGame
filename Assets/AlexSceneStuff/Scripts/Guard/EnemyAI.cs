using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Guard Enums // 
    public Guards chosenGuard;
    public StartPosition chosenTurn;
    public enum Guards { Flashlight_Walk, Flashligt_Stand, NoFlashlight_Walk, NoFlashlight_Stand, Sit_at_desk, Sleep }
    public enum StartPosition { Up, Down, Right, Left }
    private float[] degreeList = new float[] { 0, 90, 180, 270, 360 };

    // Guard Object // 
    private Animator guardAni;
    private GameObject Guard;
    private float startAniPosition;
    private float lookedAtPos;
    public float guardTurnDegrees;
    public bool turned = true;
    private bool canWalk = false;

    // Guard && Player // 
    private float guardToPlayerDegrees;
    private float oldGuardToPlayerDegrees;
    private int randomTurnInt;

    // Player // 
    private GameObject Player;
    private TeleportMenu teleP;
    private PlayerControllerMovement playerC;
    private Vector3 heardPlayerPos;

    // Message // 
    public bool heardPlayer = false;
    private bool flashlighttouched = false;
    private bool donelooking = true;

    // Scripts // 
    private GuardAnimation guardAniScript;
    private SpriteRenderer Gsprite;



    void Start()
    {
        Player = GameObject.Find("Player");
        teleP = Player.GetComponent<TeleportMenu>();
        playerC = Player.GetComponent<PlayerControllerMovement>();
        Guard = this.gameObject;
        var circleColl = Guard.GetComponent<CircleCollider2D>();
        guardAni = Guard.GetComponent<Animator>();
        guardAniScript = Guard.GetComponent<GuardAnimation>();
        Gsprite = Guard.GetComponent<SpriteRenderer>();


        // Check player startposition // 
        if (chosenTurn == StartPosition.Up) { guardTurnDegrees = 90; guardAni.SetInteger("GuardAnim", 2); }
        if (chosenTurn == StartPosition.Down) { guardTurnDegrees = 270; guardAni.SetInteger("GuardAnim", 1); }
        if (chosenTurn == StartPosition.Right) { guardTurnDegrees = 0; guardAni.SetInteger("GuardAnim", 3); }
        if (chosenTurn == StartPosition.Left) { guardTurnDegrees = 180; guardAni.SetInteger("GuardAnim", 3); Gsprite.flipX = true; }

        // Set animation position // 

        // Set circle collider radius // 
        if (chosenGuard == Guards.Flashlight_Walk || chosenGuard == Guards.Flashligt_Stand ||
            chosenGuard == Guards.NoFlashlight_Stand || chosenGuard == Guards.NoFlashlight_Walk) { circleColl.radius = 5; }
        if (chosenGuard == Guards.Sit_at_desk || chosenGuard == Guards.Sleep) { circleColl.radius = 3; }
    }


    void Update()
    {
        // For every guard that stands / walks // 
        if (chosenGuard == Guards.Flashlight_Walk || chosenGuard == Guards.Flashligt_Stand || chosenGuard == Guards.NoFlashlight_Stand || chosenGuard == Guards.NoFlashlight_Walk)
        {
            // If the guard hasnt heard the player and is just doing his thing // s
            if (heardPlayer == false)
            {
                DoingHisThing();
            }
            if (flashlighttouched == false)
            {
                if (heardPlayer == true)
                {
                    if (turned == true && guardToPlayerDegrees != guardTurnDegrees) {

                        TurnToWay(guardToPlayerDegrees);
                        turned = false;
                    }

                    if (guardToPlayerDegrees == guardTurnDegrees)
                    {
                        turned = true;
                        heardPlayer = false;
                    }
                }
            }

            if (Vector3.Distance(Player.transform.position, this.gameObject.transform.position) < 3)
            {
                // Player has been found because he went to close to the enemy // 
                // Guard turns && Player movement is stuck // 
                teleP.teleportPlayer();
            }

            if (flashlighttouched == true)
            {
                // If player has been touched by the flashlight //
                teleP.teleportPlayer();
                heardPlayer = false;
                flashlighttouched = false;
            }
        }

        // For every guard that walks // 
        if (chosenGuard == Guards.Flashlight_Walk || chosenGuard == Guards.NoFlashlight_Walk)
        {
            if (heardPlayer == true)
            {
                // stop moving for a sec bcs you heard the player and you are turning // 
            }
        }

        // For every guard that sleeps / sits // 
        if (chosenGuard == Guards.Sleep || chosenGuard == Guards.Sit_at_desk)
        {
            if (heardPlayer == true)
            {
                // Player has been found because he went to close to the enemy that was sleeping / standing // 
                // Guard turns // 
                teleP.teleportPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player has been found by the guard // 
        if (collision.gameObject.tag == "Player")
        {
            // If player walked in while guard was not looking for him yet // 
            if (heardPlayer == false)
            {
                heardPlayerPos = Player.transform.position;

                // Get the turn position the enemy has && check the player position //
                guardToPlayerDegrees = Mathf.Atan2(heardPlayerPos.y - Guard.transform.position.y, heardPlayerPos.x - Guard.transform.position.x) * (180 / Mathf.PI);
                if (heardPlayerPos.y < Guard.transform.position.y)
                {
                    guardToPlayerDegrees += 360;
                }
                oldGuardToPlayerDegrees = guardToPlayerDegrees;

                // Check what is closest degrees // 
                float shortestTest = 9999;
                for (int i = 0; i < degreeList.Length; i++)
                {
                    float shortestWay = degreeList[i] - guardToPlayerDegrees;
                    if (shortestWay < shortestTest && shortestWay > -45)
                    {
                        shortestTest = shortestWay;
                        guardToPlayerDegrees = degreeList[i];
                    }
                }
                if (guardToPlayerDegrees == 360)
                {
                    guardToPlayerDegrees = 0;
                }

                randomTurnInt = Random.Range(0, 2);
                heardPlayer = true;
            }
        }
    }

    private void TurnToWay(float turnToThis)
    {

        // If looking right // 
        if (guardTurnDegrees == 0)
        {
            if (turnToThis == 90) { StartCoroutine(guardAniScript.TurntoPosition(0, 90)); }
            if (turnToThis == 180)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(0, 90)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(0, 270)); }
            }
            if (turnToThis == 270) { StartCoroutine(guardAniScript.TurntoPosition(0, 270)); }
        }

        // If looking Up // 
        if (guardTurnDegrees == 90)
        {
            if (turnToThis == 0) { StartCoroutine(guardAniScript.TurntoPosition(90, 0)); }
            if (turnToThis == 180) { StartCoroutine(guardAniScript.TurntoPosition(90, 180)); }
            if (turnToThis == 270)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(90, 180)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(90, 0)); }
            }
        }

        // If looking Left // 
        if (guardTurnDegrees == 180)
        {
            if (turnToThis == 0)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(180, 90)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(180, 270)); }
            }
            if (turnToThis == 90) { StartCoroutine(guardAniScript.TurntoPosition(180, 90)); }
            if (turnToThis == 270) { StartCoroutine(guardAniScript.TurntoPosition(180, 270)); }
        }

        // If looking Down // 
        if (guardTurnDegrees == 270)
        {
            if (turnToThis == 0) { StartCoroutine(guardAniScript.TurntoPosition(270, 0)); }
            if (turnToThis == 90)
            {
                if (randomTurnInt == 0) { StartCoroutine(guardAniScript.TurntoPosition(270, 180)); }
                if (randomTurnInt == 1) { StartCoroutine(guardAniScript.TurntoPosition(270, 0)); }
            }
            if (turnToThis == 180) { StartCoroutine(guardAniScript.TurntoPosition(270, 180)); }
        }
    }


    private void DoingHisThing()
    {
        if (chosenGuard == Guards.Flashlight_Walk || chosenGuard == Guards.Flashligt_Stand || chosenGuard == Guards.NoFlashlight_Stand || chosenGuard == Guards.NoFlashlight_Walk)
        {

        }

        if (canWalk == true)
        {

        }

        // If enemy wants to walk an way and the enemy isnt facing that way // 
        //TurnToWay();
    }

    

}
