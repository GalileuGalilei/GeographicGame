using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Unity.Properties;
using System.Threading.Tasks;
using System;

public class scoreboard : MonoBehaviour
{
    [SerializeField]
    private List<PlayerScore> playerScores = new List<PlayerScore>();
    // Start is called before the first frame update
    async void Start()
    {
        HttpClient client = new();
        playerScores = await GetScoreboardAsync(client);
    }

    [Serializable]
    public class PlayerScore
    {
        public string name;
        public int score;
    }

    private static async Task<List<PlayerScore>> GetScoreboardAsync(HttpClient client)
    {
        string responseBody = await client.GetStringAsync("http://168.138.147.86:80/scoreboard");
        List<PlayerScore> playerScores = JsonConvert.DeserializeObject<List<PlayerScore>>(responseBody);
        
        Debug.Log("Scoreboard initialized");
        return playerScores;
    }

}
