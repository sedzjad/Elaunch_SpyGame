using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    // Vector list// 
    private List<Vector3> walkPositions = new List<Vector3>();
    private GameObject objectList;

    // Guard //
    private GameObject Guard;
    public bool canWalk;
    
    void Start()
    {
        if (transform.childCount > 0)
        {
            canWalk = true;
            Guard = transform.parent.gameObject;

            walkPositions.Add(Guard.transform.position);
            foreach (Transform child in this.transform)
            {
                if (child.name == "WalkingPositions")
                {
                    objectList = child.gameObject;
                    walkPositions.Add(objectList.transform.position);
                }
            }

            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        } else
        {
            canWalk = false;
            Destroy(this.gameObject);
        }
    }

    
    void Update()
    {
        if (canWalk == true)
        {

        }
    }
}
