using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
using Unity.VisualScripting;

public class RatingMP : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI player1ScoreText;
    [SerializeField] TextMeshProUGUI player2ScoreText;
    [SerializeField] TextMeshProUGUI ResultScore1Text;
    [SerializeField] TextMeshProUGUI ResultScore2Text;

    [SyncVar(hook = nameof(OnPlayer1ScoreChanged))] public int player1Score = 0;
    [SyncVar(hook = nameof(OnPlayer2ScoreChanged))] public int player2Score = 0;

    [SyncVar] NetworkIdentity player1;

    private int player1ConnectionId = 0;
    private int player2ConnectionId = 1;

    public GameObject[] playersArray;

    public void FindPlayersByTag()
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");
    }
    public void IncrementPlayerScore(int points, int connectionId)
    {
        if (connectionId == player1ConnectionId)
        {
            player1Score += points;
        }
        else if (connectionId == player2ConnectionId)
        {
            player2Score += points;
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdIncrementPlayerScore(int points, int connectionId)
    {
        IncrementPlayerScore(points, connectionId);
    }
    private void UpdateScoreboard()
    {
        player1ScoreText.text = "Игрок 1: " + player1Score.ToString();
        player2ScoreText.text = "Игрок 2: " + player2Score.ToString();
        ResultScore1Text.text = player1Score.ToString();
        ResultScore2Text.text = player2Score.ToString();

    }
    private void OnPlayer1ScoreChanged(int oldValue, int newValue)
    {
        UpdateScoreboard();
    }

    private void OnPlayer2ScoreChanged(int oldValue, int newValue)
    {
        UpdateScoreboard();
    }
}
