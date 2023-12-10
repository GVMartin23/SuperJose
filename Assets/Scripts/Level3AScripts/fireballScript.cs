using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        _audiosource = GetComponent<AudioSource>(); 
        _audiosource.Play();

        _rbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (_rbody.transform.position.y < -3) 
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
