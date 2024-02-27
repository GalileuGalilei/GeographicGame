using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField]
    private PlayerScoreUI playerScorePrefab;

    [SerializeField] 
    private ScoreboardDB scoreboardDB;
    private List<ScoreboardDB.PlayerScore> playerScores;

    private void Awake()
    {
        StartCoroutine(scoreboardDB.GetScoreboardAsync((playerScores) =>
        {
            this.playerScores = playerScores;
            UpdateUI();
        }));
    }

    private void UpdateUI()
    {
        foreach (var playerScore in playerScores)
        {
            var playerScoreUI = Instantiate(playerScorePrefab, transform);
            playerScoreUI.SetText(playerScore.PlayerName, playerScore.Score.ToString(), playerScore.GameModeName);
        }
    }
}
