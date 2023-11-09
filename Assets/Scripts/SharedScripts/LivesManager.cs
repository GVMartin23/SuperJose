using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public Text Lives;

    // Start is called before the first frame update
    private void Start()
    {
        Lives.text = "Lives: " + PlayerPrefs.GetInt("Lives");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}