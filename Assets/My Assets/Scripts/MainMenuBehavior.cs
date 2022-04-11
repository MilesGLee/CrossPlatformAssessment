using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    void Start()
    {
        
    }

    public void PlayGame() 
    {
        Application.LoadLevel("play_scene");
    }

}
