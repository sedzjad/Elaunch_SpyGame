using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditModeGridSnap : MonoBehaviour
{

    private float snapValueX = 1.98f;
    private float snapValueY = 1.46f;
    private float depth = 0;

    
    void Update()
    {
        foreach (Transform test1 in transform)
        {
            foreach (Transform test2 in test1)
            {
                float snapInverseX = 1 / snapValueX;
                float snapInverseY = 1 / snapValueY;

                float x, y, z;

                // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
                // so 1.45 to nearest .5 is 1.5
                x = Mathf.Round(test2.position.x * snapInverseX) / snapInverseX;
                y = Mathf.Round(test2.position.y * snapInverseY) / snapInverseY;
                z = depth;  // depth from camera

                test2.position = new Vector3(x, y, z);
            }
        }
    }
}