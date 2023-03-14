using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RatingMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EasyLevelText;
    [SerializeField] TextMeshProUGUI MediumLevelText;
    [SerializeField] TextMeshProUGUI HardLevelText;
    [SerializeField] TextMeshProUGUI BonusLevelText;
    void Update()
    {
        EasyLevelText.text = "Легкий уровень - " + PlayerPrefs.GetInt("EasyScore").ToString();
        MediumLevelText.text = "Средний уровень - " + PlayerPrefs.GetInt("MediumScore").ToString();
        HardLevelText.text = "Сложный уровень - " + PlayerPrefs.GetInt("HardScore").ToString();
    }
}
