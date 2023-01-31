using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pManager;
    public void PlayGame()
    {
        SceneManager.LoadScene("Map 1");
    }
    public void QuitGame()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        Application.Quit();
    }

    public void QuitGame2()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main") && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("GameOver"))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pManager.GetComponent<PlayerResources>().textHolderSpeed.enabled = false;
        pManager.GetComponent<PlayerResources>().spedUp = false;
    }
}
