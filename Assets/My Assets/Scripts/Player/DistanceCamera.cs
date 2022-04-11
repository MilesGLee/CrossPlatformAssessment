using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCamera : MonoBehaviour
{
    void Update()
    {
        //Change the cameras distance to the player depending on the aspect ratio
        Vector3 newPos = transform.position;
        if (GetComponent<Camera>().aspect < 1)
            newPos.z = -35;
        if (GetComponent<Camera>().aspect > 1)
            newPos.z = -20;

        transform.position = newPos;
    }
}
