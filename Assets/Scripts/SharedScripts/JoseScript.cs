using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JoseScript : MonoBehaviour
{
    public float Speed;

    Rigidbody2D _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    //Handles all movement for Jose
    //Currently can only move left and right, jumping later
    private void HandleMovement()
    {
        float dx = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(dx * Speed, _rigidBody.velocity.y);//Keep y velocity same instead of at 0, might be good when we have jumping
        

        //flips sprite
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
