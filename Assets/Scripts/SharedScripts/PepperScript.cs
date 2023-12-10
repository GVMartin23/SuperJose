using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PepperScript : MonoBehaviour
{
    int speed = 1;
    Rigidbody2D _rbody;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 position = _rbody.transform.position;
        float posY = position.y;

        if (posY > gameObject.transform.position.y + 0.5)
        {
            _rbody.velocity = Vector2.down * speed;
        }
        else if (posY < gameObject.transform.position.y - 0.5)
        {
            _rbody.velocity = Vector2.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
