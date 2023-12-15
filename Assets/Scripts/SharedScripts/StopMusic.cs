using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    private JoseScript _joseScript;

    // Start is called before the first frame update
    private void Start()
    {
        _joseScript = FindObjectOfType<JoseScript>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_joseScript.stopMusic == true)
        {
            _audioSource.Stop();
            _joseScript.stopMusic = false;
        }
    }

    public void Stop()
    {
        _audioSource?.Stop();
    }
}