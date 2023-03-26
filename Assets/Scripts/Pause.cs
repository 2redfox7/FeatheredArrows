using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject bow;

    public AudioSource BackgroundMusic;

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
        Time.timeScale = 1.0f;
        Player.sensitivity = 2f;
        GameIsPaused = false;
    }

    void Paus()
    {
        BackgroundMusic.Pause();
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        bow.SetActive(false);
        Player.sensitivity = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Timer.timeStart = 3;
        Timer.GameIsStart = true;
        Rating.score = 0;
        GameIsPaused = false;
        Player.sensitivity = 2f;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMenuInResult()
    {
        Timer.timeStart = 3;
        Timer.GameIsStart = true;
        Rating.score = 0;
        Player.sensitivity = 2f;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Timer.timeStart = 3;
        Timer.GameIsStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
        bow.SetActive(true);
        Time.timeScale = 1.0f;
        Rating.score = 0;
    }
}
