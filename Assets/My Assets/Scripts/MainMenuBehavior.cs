using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    [SerializeField] private Text highScoreText;

    void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("Score");
    }

    public void PlayGame() 
    {
        Application.LoadLevel("play_scene");
    }

}
