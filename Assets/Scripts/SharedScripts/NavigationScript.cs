using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Generic Script for letting scenes escape to menu
public class NavigationScript : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject Panel;
    public GameObject PauseButton;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPaused = !GameIsPaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (GameIsPaused)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
            PauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
            PauseButton.SetActive(true);
        }
    }

    public void OnPauseButtonClicked()
    {
        GameIsPaused = true;
        PauseGame();
    }

    public void OnResumeButtonClicked()
    {
        GameIsPaused = false;
        PauseGame();
    }

    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }

    public void OnQuitButtonClicked()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}