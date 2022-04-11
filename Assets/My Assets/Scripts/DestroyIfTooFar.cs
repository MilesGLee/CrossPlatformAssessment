using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfTooFar : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance;

    public Transform Target 
    {
        set { target = value; }
    }
    public float Distance
    {
        set { distance = value; }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > distance)
            Destroy(gameObject);
    }
}
