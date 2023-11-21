using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGameSceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void RestartOnClick()
    {
        //Sets lives to 3
        PlayerPrefs.SetInt("Lives", 5);
        SceneManager.LoadScene("Level1Scene");
    }

    public void MainMenuOnClick()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}