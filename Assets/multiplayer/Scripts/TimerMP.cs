using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class TimerMP : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI player1ScoreText;
    [SerializeField] TextMeshProUGUI player2ScoreText;
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
            multiplayerPlayer.sensitivity = 0f;
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
        player1ScoreText.GameObject().SetActive(false);
        player2ScoreText.GameObject().SetActive(false);
        resultMenuUI.SetActive(true);
        GameIsEnd = true;
        Time.timeScale = 0f;
        bow.SetActive(false);
        multiplayerPlayer.sensitivity = 0f;
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
        player1ScoreText.GameObject().SetActive(true);
        player2ScoreText.GameObject().SetActive(true);
        TimerText.GameObject().SetActive(true);
        multiplayerPlayer.sensitivity = 2f;
        bow.SetActive(true);
        LetsGo.Play();
        GameIsStart = false;
    }
}
