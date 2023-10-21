using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D _rbody;
    public float playerSpeed;
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerSpeed = 3;
            float x = playerSpeed * Input.GetAxis("Horizontal");
            _rbody.velocity = new Vector2(-playerSpeed,0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerSpeed = 3;
            float x = playerSpeed * Input.GetAxis("Horizontal");
            _rbody.velocity = new Vector2(playerSpeed,0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


    }
}
