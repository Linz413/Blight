﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        //ADD AUDIO HERE
        SceneManager.LoadScene("StartMenu");
    }
}
