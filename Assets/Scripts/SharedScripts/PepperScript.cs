using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PepperScript : MonoBehaviour
{
    float speed = 1.5f;
    Rigidbody2D _rbody;
    float _baseline;

    // Start is called before the first frame update
    void Start()
    {
        _baseline = gameObject.transform.position.y;
        _rbody = GetComponent<Rigidbody2D>();
        _rbody.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
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
