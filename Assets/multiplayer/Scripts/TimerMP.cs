using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;

public class TimerMP : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI player1ScoreText;
    [SerializeField] TextMeshProUGUI player2ScoreText;
    [SerializeField] TextMeshProUGUI player1ScoreResultText;
    [SerializeField] TextMeshProUGUI player2ScoreResultText;
    [SerializeField] TextMeshProUGUI StartTimerText;
    [SerializeField] TextMeshProUGUI waitingPlayers;
    [SerializeField] TextMeshProUGUI winOrLose;

    public AudioSource LetsGo;

    public static bool GameIsStart = true;
    public static bool GameIsEnd = false;

    public GameObject resultMenuUI;
    public GameObject bow;

    public float timeLeft = 30;
    public float timeStart = 3;

    private RatingMP scoringSystem;

    private void Start()
    {
        scoringSystem = FindObjectOfType<RatingMP>();
    }
    void Update()
    {
        scoringSystem.FindPlayersByTag();
        if (scoringSystem.playersArray.Length < 2)
        {
            multiplayerPlayer.sensitivity = 0f;
            return;
        }
        if (GameIsStart)
        {
            waitingPlayers.GameObject().SetActive(false);
            StartTimerText.GameObject().SetActive(true);
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
            if (Mathf.Round(timeLeft) <= 0)
            {
                if (isServer)
                {
                    TryResultServer();
                }
                else
                {
                    TryResultClient();
                }
            }
        }
    }

    void TryResultServer()
    {
        Cursor.lockState = CursorLockMode.None;
        TimerText.GameObject().SetActive(false);
        player1ScoreText.GameObject().SetActive(false);
        player2ScoreText.GameObject().SetActive(false);
        resultMenuUI.SetActive(true);
        GameIsEnd = true;
        bow.SetActive(false);
        multiplayerPlayer.sensitivity = 0f;

        winOrLose.GameObject().SetActive(true);

        if (Convert.ToInt32(player1ScoreResultText.text) < Convert.ToInt32(player2ScoreResultText.text))
        {
            winOrLose.text = "Вы проиграли :(";
        }
        else if (Convert.ToInt32(player1ScoreResultText.text) > Convert.ToInt32(player2ScoreResultText.text))
        {
            winOrLose.text = "Вы выиграли :)";
        }
        else
        {
            winOrLose.text = "Ничья :o";
        }
    }
    void TryResultClient()
    {
        Cursor.lockState = CursorLockMode.None;
        TimerText.GameObject().SetActive(false);
        player1ScoreText.GameObject().SetActive(false);
        player2ScoreText.GameObject().SetActive(false);
        resultMenuUI.SetActive(true);
        GameIsEnd = true;
        bow.SetActive(false);
        multiplayerPlayer.sensitivity = 0f;

        winOrLose.GameObject().SetActive(true);

        if (Convert.ToInt32(player1ScoreResultText.text) < Convert.ToInt32(player2ScoreResultText.text))
        {
            winOrLose.text = "Вы выиграли :)";
        }
        else if (Convert.ToInt32(player1ScoreResultText.text) > Convert.ToInt32(player2ScoreResultText.text))
        {
            winOrLose.text = "Вы проиграли :(";
        }
        else
        {
            winOrLose.text = "Ничья :o";
        }
    }
    void StartGame()
    {
        timeLeft = 30;
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
