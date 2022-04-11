using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldBehavior : MonoBehaviour
{
    [SerializeField] private Transform player; //The players transform
    [SerializeField] private GameObject enemy; //The enemy prefab to spawn
    [SerializeField] private GameObject point; //The point prefab to spawn
    [SerializeField] private Text scoreText; 
    private float score; //The games score

    void Start()
    {
        //Spawn enemies and points in a 5 second interval
        InvokeRepeating("SpawnEnemy", 5.0f, 5.0f);
        InvokeRepeating("SpawnPoint", 5.0f, 5.0f);
    }

    void Update()
    {
        if(player)
            score += 0.01f;
        scoreText.text = "" + Mathf.Round(score); //Update hud text to display score
    }

    void SpawnEnemy() 
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        if (list.Length >= 3) return; //List and if statement to make sure theres never more than 3 enemies in the scene.

        Vector3 randPos; //Pick a random position in the map bounds to spawn the enemy
        randPos.x = Random.Range(-50.0f, 50.0f);
        randPos.y = Random.Range(-50.0f, 50.0f);
        randPos.z = 0;

        //Spawn the enemy and target it to the player
        EnemyBehavior enmy = Instantiate(enemy, randPos, Quaternion.identity).GetComponent<EnemyBehavior>();
        enmy.Target = player;
        enmy.WB = this;
    }

    void SpawnPoint()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Point");
        if (list.Length >= 10) return; //No more than 10 points in play

        Vector3 randPos; //Rando position acquired
        randPos.x = Random.Range(-50.0f, 50.0f);
        randPos.y = Random.Range(-50.0f, 50.0f);
        randPos.z = 0;

        PointBehavior pb = Instantiate(point, randPos, Quaternion.identity).GetComponent<PointBehavior>();
        pb.WB = this;
    }

    public void AddScore(float value) 
    {
        score += value;
    }

    public void OnPlayerDeath() 
    {
        //When the player dies, save the score if it is a new highscore and go back to main menu
        if(score > PlayerPrefs.GetFloat("Score"))
            PlayerPrefs.SetFloat("Score", Mathf.Round(score));
        PlayerPrefs.Save();
        Application.LoadLevel("main_menu");
    }
}
