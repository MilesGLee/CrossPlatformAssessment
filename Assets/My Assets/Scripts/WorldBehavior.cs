using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldBehavior : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject point;
    [SerializeField] private Text scoreText;
    private float score;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5.0f, 5.0f);
        InvokeRepeating("SpawnPoint", 5.0f, 5.0f);
    }

    void Update()
    {
        if(player)
            score += 0.01f;
        scoreText.text = "" + Mathf.Round(score);
    }

    void SpawnEnemy() 
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        if (list.Length >= 3) return;

        Vector3 randPos;
        randPos.x = Random.Range(-50.0f, 50.0f);
        randPos.y = Random.Range(-50.0f, 50.0f);
        randPos.z = 0;

        EnemyBehavior enmy = Instantiate(enemy, randPos, Quaternion.identity).GetComponent<EnemyBehavior>();
        enmy.Target = player;
        enmy.WB = this;
    }

    void SpawnPoint()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Point");
        if (list.Length >= 10) return;

        Vector3 randPos;
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
        if(score > PlayerPrefs.GetFloat("Score"))
            PlayerPrefs.SetFloat("Score", Mathf.Round(score));
        PlayerPrefs.Save();
        Application.LoadLevel("main_menu");
    }
}
