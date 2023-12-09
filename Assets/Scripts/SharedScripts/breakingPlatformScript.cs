using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakingPlatformScript : MonoBehaviour
{
    AudioSource _audioSource;

    public GameObject snowFall;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        snowFall.SetActive(false);
        snowFall.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Invoke("BreakPlatform", 1);
            Invoke("BreakSound", 0.5f);
        }
    }

    void BreakPlatform()
    {
        gameObject.SetActive(false);
        snowFall.SetActive(true);
        Invoke("BuildPlatform", 5);
        

    }

    void BuildPlatform()
    {
        gameObject.SetActive(true);

    }

    void BreakSound()
    {
        _audioSource.Play();
    }
}
