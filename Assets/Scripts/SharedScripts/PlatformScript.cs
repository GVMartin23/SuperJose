using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class moving_platform_script : MonoBehaviour
{
    public int speed;
    Rigidbody2D _rbody;
    Vector2 down = (new Vector2(0, -3)).normalized;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _rbody.velocity = (-down) * speed;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = _rbody.transform.position;
        float posY = position.y;

        if (posY > -1.5)
        {
            _rbody.velocity = (down) * speed;
        }
        else if (posY < -4)
        {
            _rbody.velocity = (-down) * speed;
        }
    }
}