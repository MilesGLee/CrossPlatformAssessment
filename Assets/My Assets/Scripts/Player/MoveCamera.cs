using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        if(target)
            transform.position = target.position;
    }
}
