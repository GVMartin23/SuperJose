using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public Text Lives;
    public List<GameObject> Joses;

    // Start is called before the first frame update
    private void Start()
    {
        int lives = PlayerPrefs.GetInt("Lives");
        Lives.text = "Lives: ";

        //Sets lives amount of Jose objects active to show how many lives there are
        for (int i = 0; i < lives; i++)
        {
            Joses[i].SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}