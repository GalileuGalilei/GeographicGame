using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FlagDisplayer : MonoBehaviour
{
    [SerializeField]
    Image flagImage;

    string flagCode = "BE";
    string flagAPI = "https://cdn.jsdelivr.net/gh/hampusborgos/country-flags@main/png100px/";
    string style = ".png";

    void Start()
    {
        StartCoroutine(UpdateFlagImage());
    }

    public void UpdateFlag(string code)
    {
        flagCode = code;
        StartCoroutine(UpdateFlagImage());
    }

    IEnumerator UpdateFlagImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(flagAPI + flagCode.ToLower() + style);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error + "tryied to download from " + flagAPI + flagCode + style);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            flagImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
