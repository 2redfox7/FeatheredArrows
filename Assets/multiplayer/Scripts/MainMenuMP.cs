using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;
using Mirror.Examples.Basic;
using TMPro;

[System.Serializable]
public class Match : NetworkBehaviour
{
    public string ID;
    public readonly List<GameObject> players = new List<GameObject>();

    public Match(string ID, GameObject player)
    {
        this.ID = ID;
        players.Add(player);
        Debug.Log("WWW");
    }
}
public class MainMenuMP : NetworkBehaviour
{
    public static MainMenuMP instance;
    public readonly SyncList<Match> matches = new SyncList<Match>();
    public readonly SyncList<string> matchIDs = new SyncList<string>();
    [SerializeField] TMP_InputField JoinInput;
    public Button HostButton;
    public Button JoinButton;
    public Canvas LobbyCanvas;

    public Transform UIPLayerParent;
    public GameObject UIPlayerPrefab;
    public TextMeshProUGUI IDText;
    public Button BeginGameButton;
    public GameObject TurnManager;
    public bool inGame;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (!inGame)
        {
            multiplayerPlayer[] players = FindObjectsOfType<multiplayerPlayer>();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].gameObject.transform.localScale = Vector3.zero;
            }
        }
    }

    public void Host()
    {
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;

        multiplayerPlayer.localPlayer.HostGame();
    }

    public void HostSuccess(bool success, string matchID)
    {
        if (success)
        {
            LobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(multiplayerPlayer.localPlayer);
            IDText.text = matchID;
            BeginGameButton.interactable = true;
        }
        else
        {
            JoinInput.interactable = true;
            HostButton.interactable = true;
            JoinButton.interactable = true;
        }
    }

    public void Join()
    {
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;

        multiplayerPlayer.localPlayer.JoinGame(JoinInput.text.ToUpper());
    }

    public void JoinSuccess(bool success, string matchID)
    {
        if (success)
        {
            LobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(multiplayerPlayer.localPlayer);
            IDText.text = matchID;
            BeginGameButton.interactable = false;
        }
        else
        {
            JoinInput.interactable = true;
            HostButton.interactable = true;
            JoinButton.interactable = true;
        }
    }

    public bool HostGame(string matchID, GameObject player)
    {
        if (!matchIDs.Contains(matchID))
        {
            matchIDs.Add(matchID);
            matches.Add(new Match(matchID, player));
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JoinGame(string matchID, GameObject player)
    {
        if (matchIDs.Contains(matchID))
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].ID == matchID)
                {
                    matches[i].players.Add(player);
                    break;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetRandomID()
    {
        string ID = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, 36);
            if (rand < 26)
            {
                ID += (char)(rand + 65);
            }
            else
            {
                ID += (rand - 26).ToString();
            }
        }
        return ID;
    }

    public void SpawnPlayerUIPrefab(multiplayerPlayer player)
    {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPLayerParent);
        newUIPlayer.GetComponent<PlayerUI>().SetPlayer(player);
    }

    public void StartGame()
    {
        multiplayerPlayer.localPlayer.BeginGame();
    }

    public void BeginGame(string matchID)
    {
        GameObject newTurnManager = Instantiate(TurnManager);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = matchID.ToGuid();
        TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();

        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].ID == matchID)
            {
                foreach (var player in matches[i].players)
                {
                    multiplayerPlayer player1 = player.GetComponent<multiplayerPlayer>();
                    turnManager.AddPlayer(player1);
                    player1.StartGame();
                }
                break;
            }
        }
    }
}

public static class MatchExtension
{
    public static Guid ToGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hasBytes = provider.ComputeHash(inputBytes);

        return new Guid(hasBytes);
    }
}