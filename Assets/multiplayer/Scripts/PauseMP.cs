using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class PauseMP : NetworkBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject bow;

    public AudioSource BackgroundMusic;
    private RatingMP scoringSystem;

    private ConnectionMenu menu;

    private void Start()
    {
        scoringSystem = FindObjectOfType<RatingMP>();
        menu = FindObjectOfType<ConnectionMenu>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused )
            {
                Paus();
            }
        }
    }

    public void Resume()
    {
        BackgroundMusic.Play(); 
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        bow.SetActive(true);
        multiplayerPlayer.sensitivity = 2f;
        GameIsPaused = false;
    }

    void Paus()
    {
        BackgroundMusic.Pause();
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        bow.SetActive(false);
        multiplayerPlayer.sensitivity = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        TimerMP.GameIsStart = true;
        scoringSystem.player1Score = 0;
        scoringSystem.player2Score = 0;
        GameIsPaused = false;
        multiplayerPlayer.sensitivity = 2f;
        if (isServer)
        {
            menu.manager.StopHost();
        }
        else
        {
            menu.manager.StopClient();
        }
        SceneManager.LoadScene("MainMenu");

    }

    public void LoadMenuInResult()
    {
        TimerMP.GameIsStart = true;
        scoringSystem.player1Score = 0;
        scoringSystem.player2Score = 0;
        multiplayerPlayer.sensitivity = 2f;
        if (menu.manager.mode == NetworkManagerMode.Host)
        {
            menu.manager.StopHost();
        }
        if (menu.manager.mode == NetworkManagerMode.ClientOnly)
        {
            menu.manager.StopClient();
        }
        SceneManager.LoadScene("MainMenu");
    }


    public void Restart()
    {
        TimerMP.GameIsStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
        bow.SetActive(true);
        Time.timeScale = 1.0f;
        scoringSystem.player1Score = 0;
        scoringSystem.player2Score = 0;
    }
}