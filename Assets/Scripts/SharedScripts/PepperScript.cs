using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PepperScript : MonoBehaviour
{
    private float speed = 1.5f;
    private Rigidbody2D _rbody;
    private float _baseline;

    // Start is called before the first frame update
    private void Start()
    {
        //Get rigidbody, set initial speed and position
        _baseline = gameObject.transform.position.y;
        _rbody = GetComponent<Rigidbody2D>();
        _rbody.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        //Moves pepper up and down within range
        float posY = _rbody.transform.position.y;

        if (posY > _baseline + 0.2)
        {
            _rbody.velocity = Vector2.down * speed;
        }
        if (posY < _baseline - 0.2)
        {
            _rbody.velocity = Vector2.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}