﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{

    public void menu()
    {
        SceneManager.LoadScene("main menu");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
