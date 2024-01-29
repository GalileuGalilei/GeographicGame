using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuHUD;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void BackToMenu()
    {
        if(isPaused)
        {
            Resume();
        }

        MenuHUD.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        MenuHUD.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Resume()
    {
        MenuHUD.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}
