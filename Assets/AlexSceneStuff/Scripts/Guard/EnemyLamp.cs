using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLamp : MonoBehaviour
{
    // Lamp moet kunnen werken wanneer guard loopt. Gaurd kan er ook voor kiezen
    // om zonder een lamp te lopen. Wanneer de guard loopt en op het moment stilstaat
    // en effe rust neemt kan de lamp aan/uit blijven. Als de guard eenmaal moet draaien
    // of hij moet effe een turn nemen "gaat de lamp uit (omlaag)". Wanneer de speler
    // in een bepaalde radius komt van de gaurd krijgt de guard een !!!!!. Hij draaid
    // dan naar de laatste speler positie hij doorkreeg en gaat dan kijken of de speler
    // daar is door met zijn lamp erop te schijnen. Als de speler er niet is draaid hij
    // terug en gaat hij verder met zijn laatste loopje waarmee hij bezig was, of hij
    // neem een kleine pauze. Als het eenmaal gebeurd dat de speler gezien word is de speler
    // gevangen genomen. de speler gaat dan terug naar de laatste veilige positie dat hij
    // is geweest.


    // Walk with lamp
    // Walk without lamp
    // stand still idle
    // Catch player, player goes back to start
    // Player is caught
    // Enemy turn position = lamp uit

    // !!! De lamp moet de animatie van de speler volgen & daarbij ook meedraaien wanneer hij dat doet !!! //


    private GameObject gameMenu;
    private TimeMenu timeMenu;

    private GameObject thisLight;

    public bool isLightOn = false;


    void Start()
    {
        gameMenu = GameObject.Find("GameMenu");
        timeMenu = gameMenu.GetComponent<TimeMenu>();
        thisLight = this.gameObject;
    }

    
    void Update()
    {
        if (isLightOn == true)
        {
            thisLight.GetComponent<PolygonCollider2D>().enabled = true;
            thisLight.GetComponent<SpriteRenderer>().enabled = true;
        }   else
        {
            thisLight.GetComponent<PolygonCollider2D>().enabled = false;
            thisLight.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Caught");
            // Make guard stop walking
        }
    }


}
