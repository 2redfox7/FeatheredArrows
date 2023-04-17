using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlayerText;
    private multiplayerPlayer player;

    public void SetPlayer(multiplayerPlayer player)
    {
        this.player = player;
        
        PlayerText.text = "Õ» Õ≈…Ã";
    }
}