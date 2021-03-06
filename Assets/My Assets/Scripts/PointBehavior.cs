using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehavior : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    private float rot;
    private WorldBehavior wb;

    public WorldBehavior WB 
    {
        set { wb = value; }
    }

    void Start()
    {
        rot = 0;
    }

    void Update()
    {
        //Constantly rotate the point to allure the players eyes, almost seductive like...
        rot++;
        transform.rotation = Quaternion.Euler(0, rot, -50);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            //Increase the runs score when the player collidies with the point.
            wb.AddScore(100);
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
