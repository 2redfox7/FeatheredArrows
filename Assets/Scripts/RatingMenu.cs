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
        EasyLevelText.text = "������ ������� - " + PlayerPrefs.GetInt("EasyScore").ToString();
        MediumLevelText.text = "������� ������� - " + PlayerPrefs.GetInt("MediumScore").ToString();
        HardLevelText.text = "������� ������� - " + PlayerPrefs.GetInt("HardScore").ToString();
    }
}
