using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<Camera>().aspect);
        Vector3 newPos = transform.position;
        if (GetComponent<Camera>().aspect < 1)
            newPos.z = -35;
        if (GetComponent<Camera>().aspect > 1)
            newPos.z = -20;

        transform.position = newPos;
    }
}
