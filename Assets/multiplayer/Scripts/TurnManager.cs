using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : MonoBehaviour
{
    private List<multiplayerPlayer> players = new List<multiplayerPlayer>();

    public void AddPlayer(multiplayerPlayer player)
    {
        players.Add(player);
    }
}