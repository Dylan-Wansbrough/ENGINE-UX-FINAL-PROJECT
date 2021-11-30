using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{

    public void menu()
    {
        SceneManager.LoadScene("main menu");
    }

    public void play()
    {
        playerController.trapCurrency = 100;
        gameController.isPaused = false;
        gameController.totalKilled = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
