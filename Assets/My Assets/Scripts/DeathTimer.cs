using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] private float timer;

    private void Awake()
    {
        StartCoroutine(DeathTime());
    }

    IEnumerator DeathTime() 
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
