using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI PlayerName;

    [SerializeField]
    TextMeshProUGUI PlayerScoreText;

    [SerializeField]
    TextMeshProUGUI PlayerMode;

    public void SetText(string name, string score, string mode)
    {
        PlayerName.text = name;
        PlayerScoreText.text = score;
        PlayerMode.text = mode;
    }
}
