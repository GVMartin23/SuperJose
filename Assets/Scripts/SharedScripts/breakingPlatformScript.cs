using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakingPlatformScript : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        //Get audiosorce
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Break platform 1 second after player touches
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("BreakPlatform", 1);
            Invoke("BreakSound", 0.5f);
        }
    }

    private void BreakPlatform()
    {
        //disable platform
        gameObject.SetActive(false);
        Invoke("BuildPlatform", 5);
    }

    private void BuildPlatform()
    {
        //reset platform
        gameObject.SetActive(true);
    }

    private void BreakSound()
    {
        _audioSource.Play();
    }
}