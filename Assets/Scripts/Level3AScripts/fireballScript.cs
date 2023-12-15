using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    private Rigidbody2D _rbody;
    private AudioSource _audiosource;

    // Start is called before the first frame update
    private void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _audiosource.Play();

        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Disable if no collision but moving offscreen
        if (_rbody.transform.position.y < -3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Disable if colliding
        gameObject.SetActive(false);
    }
}