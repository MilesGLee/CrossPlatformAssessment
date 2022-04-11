using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject deathParticle;

    public Transform Target 
    {
        set { target = value; }
    }

    void Update()
    {
        if(target)
            transform.Translate((target.position - transform.position) * Time.deltaTime * 0.5f);
    }
    void EnemyDeath()
    {
        Instantiate(deathParticle, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") 
        {
            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude >= 10)
            {
                EnemyDeath();
            }
            else 
            {
                collision.transform.GetComponent<PlayerMovement>().PlayerDeath();
            }
        }
    }
}
