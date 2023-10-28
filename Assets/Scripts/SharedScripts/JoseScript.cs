using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JoseScript : MonoBehaviour
{
    public LayerMask GroundLayer;
    public float Speed;
    public float JumpForce;
    bool _startedJump = false;
    bool _hasJumped = false;

    Rigidbody2D _rbody;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            _startedJump = true;
            _hasJumped = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && _hasJumped)
        {
            _rbody.velocity = _rbody.velocity / 2f;
            _hasJumped = false;
        }

    }

    private void FixedUpdate()
    {
        float xdir = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(xdir * Speed, _rbody.velocity.y);



        if (_startedJump)
        {
            //_rbody.AddForce(Vector2.up * JumpForce);
            _rbody.velocity = new Vector2(0, JumpForce);
            _startedJump = false;

        }
    }

    //Handles all movement for Jose
    //Currently can only move left and right, jumping later
    private void HandleMovement()
    {
        float dx = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(dx * Speed, _rbody.velocity.y);//Keep y velocity same instead of at 0, might be good when we have jumping
        

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

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left, Vector2.down, 0.65f, GroundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right, Vector2.down, 0.65f, GroundLayer);
        return hit.collider != null || hit2.collider != null;
    }

}
