using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform target; //The enemies target
    [SerializeField] private GameObject deathParticle; //The effect that plays when the enemy dies
    private WorldBehavior wb;

    public Transform Target 
    {
        set { target = value; }
    }
    public WorldBehavior WB
    {
        set { wb = value; }
    }

    void Update()
    {
        if(target) //If a target exists, constantly move towards it.
            transform.Translate((target.position - transform.position) * Time.deltaTime * 0.5f);
    }
    void EnemyDeath()
    {
        Instantiate(deathParticle, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") //If on collision the player has enough speed = kill me, if not = kill player
        {
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude >= 10)
            {
                wb.AddScore(200);
                EnemyDeath();
            }
            else 
            {
                other.transform.GetComponent<PlayerMovement>().PlayerDeath();
            }
        }
    }
}
