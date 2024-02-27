using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class ScoreboardDB : MonoBehaviour
{
    [Serializable]
    public class PlayerScore
    {
        public string PlayerName;
        public string GameModeName;
        public int Score;
    }

    static private string serverURL = "https://168-138-147-86.nip.io:443";

    public string PlayerName;
    public string PlayerMode;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PlayerMode = "Clas";
    }

    public void SetPlayerNickname(TextMeshProUGUI input)
    {
        PlayerName = input.text;
    }

    public void SetPlayerMode(string mode)
    {
        PlayerMode = mode;
    }

    public IEnumerator GetScoreboardAsync(Action<List<PlayerScore>> callback)
    {
        var response = UnityWebRequest.Get(serverURL + "/scoreboard");
        yield return response.SendWebRequest();

        if(response.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Scoreboard loaded successfully");
        }
        else
        {
            Debug.Log($"Failed to load scoreboard {response.result}");
            callback(new List<PlayerScore>());
            yield break;
        }

        string responseBody = response.downloadHandler.text;
        List<PlayerScore>  playerScores = JsonConvert.DeserializeObject<List<PlayerScore>>(responseBody);
        callback(playerScores);
    }

    public IEnumerator PostScoreAsync(string name, int score)
    {
        string content = JsonConvert.SerializeObject(new PlayerScore { PlayerName = name, GameModeName = PlayerMode, Score = score });
        byte[] contentRaw = System.Text.Encoding.UTF8.GetBytes(content);

        UnityWebRequest webRequest = new UnityWebRequest(serverURL + "/score/" + name, "POST");

        webRequest.uploadHandler = new UploadHandlerRaw(contentRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score posted successfully");
        }
        else
        {
            Debug.Log($"Failed to post score {webRequest.downloadHandler.text}");
        }
    }

    public IEnumerator PostScoreAsync(int score)
    {
        if (PlayerName == "")
        {
            yield break;
        }

        yield return PostScoreAsync(PlayerName, score);
    }
}
