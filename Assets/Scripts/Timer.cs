using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI StartTimerText;

    public AudioSource LetsGo;

    public static bool GameIsStart = true;
    public static bool GameIsEnd = false;

    public GameObject resultMenuUI;
    public GameObject bow;

    public static float timeLeft = 30;
    public static float timeStart = 3;
    void Update()
    {
        if (GameIsStart)
        {
            bow.SetActive(false);
            Player.sensitivity = 0f;
            StartTimerText.text = "" + Mathf.Round(timeStart).ToString();
            timeStart -= Time.deltaTime;
            if (Mathf.Round(timeStart) == 0)
            {
                StartGame();
            }
        }
        else
        {
            TimerText.text = "" + Mathf.Round(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            if (Mathf.Round(timeLeft) == 0)
            {
                TryResult();
            }
        }
    }

    void TryResult()
    {
        Cursor.lockState = CursorLockMode.None;
        TimerText.GameObject().SetActive(false);
        ScoreText.GameObject().SetActive(false);
        resultMenuUI.SetActive(true);
        GameIsEnd = true;
        Time.timeScale = 0f;
        bow.SetActive(false);
        Player.sensitivity = 0f;
    }

    void StartGame()
    {
        if (SceneManager.GetActiveScene().name == "hardLevel")
        {
            timeLeft = 60;
        }
        else
        {
            timeLeft = 30;
        }
        StartTimerText.GameObject().SetActive(false);
        ScoreText.GameObject().SetActive(true);
        TimerText.GameObject().SetActive(true);
        Player.sensitivity = 2f;
        bow.SetActive(true);
        LetsGo.Play();
        GameIsStart = false;
    }
}
