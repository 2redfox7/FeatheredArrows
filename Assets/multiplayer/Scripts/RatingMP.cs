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
    [SerializeField] TextMeshProUGUI ResultScoreText;

    [SyncVar(hook = nameof(OnPlayer1ScoreChanged))] public int player1Score = 0;
    [SyncVar(hook = nameof(OnPlayer2ScoreChanged))] public int player2Score = 0;

    [SyncVar] NetworkIdentity player1;

    private int player1ConnectionId;
    private int player2ConnectionId;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (player1 == null)
        {
            player1 = GameObject.FindWithTag("Player").GetComponent<NetworkIdentity>();
        }
        else
        {
            return;
        }
        // Find the two players' network connection IDs
        /*NetworkIdentity[] player = FindObjectsOfType<NetworkIdentity>();
        NetworkIdentity[] players = new NetworkIdentity[2];
        foreach (NetworkIdentity playerIdentity in player)
        {
            if (playerIdentity.tag == "Player")
            {
                if (players[0] == null) players[0] = playerIdentity;
                else players[1] = playerIdentity;
            }
        }
        
        player1ConnectionId = Mathf.Min(players[0].connectionToClient.connectionId); //, players[1].connectionToClient.connectionId);
        //player2ConnectionId = Mathf.Max(players[0].connectionToClient.connectionId, players[1].connectionToClient.connectionId);

        Debug.Log("Player 1 connection ID: " + player1ConnectionId);
        //Debug.Log("Player 2 connection ID: " + player2ConnectionId);*/
        player1ConnectionId = player1.connectionToClient.connectionId;

        Debug.Log("Player 1 connection ID: " + player1ConnectionId);

    }
    public void IncrementPlayerScore(int points, int connectionId)
    {
        Debug.Log("УРа");
        // Determine which player scored based on their network connection ID
        if (connectionId == player1ConnectionId)
        {
            Debug.Log("ЖОПА 1");
            player1Score += points;
        }
        else
        {
            Debug.Log("ЖОПА 2");
            player2Score += points;
        }
    }
    [Command]
    public void CmdIncrementPlayerScore(int points, int connectionId)
    {
        IncrementPlayerScore(points, connectionId);
    }
    private void UpdateScoreboard()
    {
        player1ScoreText.text = "Player 1: " + player1Score.ToString();
        player2ScoreText.text = "Player 2: " + player2Score.ToString();
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
