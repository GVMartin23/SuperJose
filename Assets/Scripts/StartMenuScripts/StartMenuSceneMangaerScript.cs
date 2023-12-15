using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuSceneMangaerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //Exit app
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartButtonOnClicked()
    {
        //Set Lives to 5
        PlayerPrefs.SetInt("Lives", 5);
        SceneManager.LoadScene("Level1Scene");
    }

    public void QuitButtonOnClicked()
    {
        Application.Quit();
    }
}