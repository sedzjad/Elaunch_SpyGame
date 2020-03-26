using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInPlant : MonoBehaviour
{

    // Plant // 
    private bool foundPlant = false;
    private bool inPlant = false;
    private GameObject selectedPlant;
    private Vector3 foundPlantPos;
    public bool goingInPlant = false;
    private bool goingOutPlant = false;
    private float playerPlantdistance;

    // Player // 
    private GameObject Player;
    private PlayerControllerMovement canIMove;
    private Vector3 OldPlayerPos;
    private bool playerInvis = false;
    private Animator playerAnim;

    // Plant // 
    private Animator plantAnim;

    // Touch // 
    private GameObject plantHitCollider;
    private Vector3 mousePos;

    // Timer //
    private float timer = 2;
    private float jumpInOuttime = 6;
    private bool waittoClick = false;
    private float testtimer = 0;

    

 

    void Start()
    {
        canIMove = GameObject.Find("Canvas").GetComponent<PlayerControllerMovement>();
        Player = GameObject.Find("Player");
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }


    void Update()
    {
        // Has found a plant to go in // 
        if (foundPlant == true) 
        {

            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                        mousePos = new Vector3(mousePos.x, mousePos.y, plantHitCollider.transform.position.z);
                        

                        if (plantHitCollider.GetComponent<PolygonCollider2D>().bounds.Contains(mousePos))
                        {
                            // If player isnt in plant and wants to go in //
                            if (inPlant == false && goingInPlant == false && waittoClick == false)
                            {
                                canIMove.canMove = false;
                                OldPlayerPos = Player.transform.position;
                                playerPlantdistance = Vector3.Distance(foundPlantPos, OldPlayerPos) / 50;
                                goingInPlant = true;
                                //plantAnim = selectedPlant.transform.parent.GetComponent<Animator>();
                                //plantAnim.SetInteger("PlantAnimation", 1);
                                playerAnim.SetInteger("PlayerAnimation", 5);
                            }

                            // If player is in plant and wants to get out //
                            if (inPlant == true && goingOutPlant == false && waittoClick == false)
                            {
                                playerAnim.SetInteger("PlayerAnimation", 6);
                                goingOutPlant = true;
                            }
                        }
                    }
                }
            }
        }

        if (goingInPlant == true)
        {
            testtimer += Time.deltaTime;
            if (testtimer > 0.3)
            {

                Player.GetComponent<EdgeCollider2D>().isTrigger = true;
                Player.transform.position = Vector3.Lerp(Player.transform.position, foundPlantPos, jumpInOuttime * Time.deltaTime);

                if (Vector2.Distance(Player.transform.position, foundPlantPos) < playerPlantdistance)
                {
                    inPlant = true;
                    playerInvis = true;
                    goingInPlant = false;
                    waittoClick = true;
                    testtimer = 0;
                }
                
            }
        }

        if (goingOutPlant == true)
        {
            testtimer += Time.deltaTime;
            if (testtimer > 0.4)
            {
                playerInvis = false;
                Player.transform.position = Vector3.Lerp(Player.transform.position, OldPlayerPos, jumpInOuttime * Time.deltaTime);

                if (Vector2.Distance(Player.transform.position, OldPlayerPos) < playerPlantdistance)
                {
                    playerAnim.SetInteger("PlayerAnimation", 1);
                    inPlant = false;
                    Player.GetComponent<EdgeCollider2D>().isTrigger = false;
                    canIMove.canMove = true;
                    goingOutPlant = false;
                    testtimer = 0;
                    
                }
            }
            
        }

        if (waittoClick == true)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                waittoClick = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks the plant it's in area with and get objects & position //
        if (collision.gameObject.tag == "Plant")
        {
            selectedPlant = collision.gameObject;
            foundPlantPos = selectedPlant.transform.position;
            foundPlantPos = new Vector3(foundPlantPos.x, foundPlantPos.y - (selectedPlant.GetComponent<CircleCollider2D>().offset.y * 0.2f), foundPlantPos.z);
            plantHitCollider = selectedPlant.transform.GetChild(0).gameObject;
            foundPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If player isn't in the plant and gets out // 
        if (collision.gameObject.tag == "Plant" && inPlant == false)
        {
            foundPlant = false;
        }
    }
}
