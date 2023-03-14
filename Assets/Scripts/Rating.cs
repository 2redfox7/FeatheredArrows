using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Rating : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI ResultScoreText;
    [SerializeField] TextMeshProUGUI HighscoreText;

    string level;
    public static float score = 0;
    public static int highscore;
    public static bool newRecord = false;

    private void Update()
    {
        highscore = (int)score;
        ScoreText.text = "Очки: " + highscore.ToString();
        ResultScoreText.text = "" + highscore.ToString();
        if (Timer.GameIsEnd)
        {
            if (SceneManager.GetActiveScene().name == "easyLevel")
            {
                level = "EasyScore";
                Highscore(level);
            }
            if (SceneManager.GetActiveScene().name == "mediumLevel")
            {
                level = "MediumScore";
                Highscore(level);
            }
            if (SceneManager.GetActiveScene().name == "hardLevel")
            {
                level = "HardScore";
                Highscore(level);
            }
        }
    }

    void Highscore(string a)
    {
        Timer.GameIsEnd = false;
        if (PlayerPrefs.GetInt(a) <= highscore)
        {
            newRecord = true;
            PlayerPrefs.SetInt(a, highscore);
        }
        if (newRecord)
        {
            HighscoreText.text = "Новый рекорд!";
            newRecord = false;
        }
        else
        {
            HighscoreText.text = "Ваш рекорд: " + PlayerPrefs.GetInt(a).ToString();
        }
    }
}
