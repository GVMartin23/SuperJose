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
    }

    public void StartButtonOnClicked()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void QuitButtonOnClicked()
    {
        Application.Quit();
    }
}