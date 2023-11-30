using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ParticleSystem))]
public class SnowBallScript : MonoBehaviour
{
    ParticleSystem _ps;
    Rigidbody2D _rbody;
    // Start is called before the first frame update
    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _rbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("SnowMan"))
        {
            Destroy(gameObject);
        }
    }
}