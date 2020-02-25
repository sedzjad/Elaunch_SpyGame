using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SortingLayer : MonoBehaviour
{

    private GameObject playerObject;

    // Set different gameobject Layers
    private GameObject wallsLayer;
    private GameObject obstaclesLayer;
    private GameObject objectsLayer;

    // Set different layers for objects
    Dictionary<GameObject, float> wallsMenu = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> obstaclesMenu = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> objectsMenu = new Dictionary<GameObject, float>();


    private float playerY;
    private float playerSpriteY;


    void Start()
    {
        playerObject = GameObject.Find("Bob");

        // Get given Layers for different objects
        wallsLayer = GameObject.Find("wallsLayer");
        obstaclesLayer = GameObject.Find("obstaclesLayer");
        objectsLayer = GameObject.Find("objectsLayer");

        // Walls // Object = Laag - Player = Laag
        for (int i = 0; i < wallsLayer.transform.childCount; i++)
        {
            GameObject usedChild = wallsLayer.transform.GetChild(i).gameObject;
            float childY = usedChild.transform.position.y;
            float childSpriteY = usedChild.GetComponent<SpriteRenderer>().bounds.size.y;
            float childPositionY = childY - (childSpriteY / 2);

            wallsMenu.Add(usedChild, childPositionY);
        }

        // Obstacles // Object = Boven - Player = Laag  
        for (int i = 0; i < obstaclesLayer.transform.childCount; i++)
        {
            GameObject usedChild = obstaclesLayer.transform.GetChild(i).gameObject;
            float childY = usedChild.transform.position.y;
            float childSpriteY = usedChild.GetComponent<SpriteRenderer>().bounds.size.y;
            float childPositionY = childY - (childSpriteY / 2) + (usedChild.GetComponent<BoxCollider2D>().bounds.size.y - (usedChild.GetComponent<BoxCollider2D>().bounds.size.y * 1/10));

            obstaclesMenu.Add(usedChild, childPositionY);
        }

        // Objects // Object = Midden - Player = Midden
        for (int i = 0; i < objectsLayer.transform.childCount; i++)
        {
            GameObject usedChild = objectsLayer.transform.GetChild(i).gameObject;
            float childY = usedChild.transform.position.y;
            float childSpriteY = usedChild.GetComponent<SpriteRenderer>().bounds.size.y;
            float childPositionY = childY - (childSpriteY / 2) + (usedChild.GetComponent<BoxCollider2D>().bounds.size.y / 2);

            objectsMenu.Add(usedChild, childPositionY);
        }
    }


    void Update()
    {
        playerY = playerObject.transform.position.y;
        playerSpriteY = playerObject.GetComponent<SpriteRenderer>().bounds.size.y;

        // Walls // Object = Laag - Player = Laag
        WallLayer();
        // Obstacles // Object = Boven - Player = Laag   
        ObstacleLayer();
        // Objects // Object = Midden - Player = Midden
        ObjectLayer();
    }

    void WallLayer()
    {
        // Player feet position (Box Collider)
        float positionY = playerY - (playerSpriteY / 2);

        // Check if player is above or under given places
        for (int j = 0; j < wallsLayer.transform.childCount; j++)
        {
            if (wallsMenu.Values.ElementAt(j) > positionY)
            {
                wallsMenu.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "BackLayer";
            }
            if (wallsMenu.Values.ElementAt(j) < positionY)
            {
                wallsMenu.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "FrontLayer";
            }

        }
    }

    void ObstacleLayer()
    {
        // Player feet position (Box Collider)
        float positionY = playerY - (playerSpriteY / 2);

        // Check if player is above or under given places
        for (int j = 0; j < obstaclesLayer.transform.childCount; j++)
        {
            if (obstaclesMenu.Values.ElementAt(j) > positionY)
            {
                obstaclesMenu.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "BackLayer";
            }
            if (obstaclesMenu.Values.ElementAt(j) < positionY)
            {
                obstaclesMenu.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "FrontLayer";
            }
        }
    }   

    void ObjectLayer()
    {
        // Player Middle Position (Box Collider)
        float positionY = playerY - (playerSpriteY / 2) + (playerObject.GetComponent<BoxCollider2D>().bounds.size.y / 2);

        // Check if player is above or under given places
        for (int j = 0; j < objectsLayer.transform.childCount; j++)
        {
            if (objectsMenu.Values.ElementAt(j) > positionY)
            {
                objectsMenu.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "BackLayer";
            }
            if (objectsMenu.Values.ElementAt(j) < positionY)
            {
                objectsMenu.Keys.ElementAt(j).GetComponent<SpriteRenderer>().sortingLayerName = "FrontLayer";
            }
            
        }
    }

}
