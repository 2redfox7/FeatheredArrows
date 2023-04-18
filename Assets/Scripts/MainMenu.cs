using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayEasy()
    {
        SceneManager.LoadScene("easyLevel");
    }
    public void PlayMiddle()
    {
        SceneManager.LoadScene("mediumLevel");
    }
    public void PlayHard()
    {
        SceneManager.LoadScene("hardLevel");
    }
    public void PlayBonus()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void MultiplayerLobby()
    {
        SceneManager.LoadScene("hardMultiplayerLevel");
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
