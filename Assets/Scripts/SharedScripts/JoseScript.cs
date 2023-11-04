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

    public GameObject PlayerJump;
    Rigidbody2D _rbody;


    public float CoyoteTime;
    private float _lastJumpTime = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        //PlayerJump.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        //
        if (IsGrounded())
        {
            _lastJumpTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space) && WasGrounded())
        {
            _startedJump = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerJump.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space) && _hasJumped)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            PlayerJump.SetActive(false);
            _hasJumped = false;
            float dy = _rbody.velocity.y > 0 ? _rbody.velocity.y / 5f : _rbody.velocity.y;
            _rbody.velocity = new Vector2(_rbody.velocity.x, dy);
        }


    }

    private void FixedUpdate()
    {
        float xdir = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(xdir * Speed, _rbody.velocity.y);



        

        if (_startedJump)
        {
            var yforce = Vector2.up * JumpForce;
            _rbody.velocity += yforce;
            _startedJump = false;
            _hasJumped = true;


        }
    }

    //Handles all movement for Jose
    //Currently can only move left and right, jumping later
    private void HandleMovement()
    {
        float dx = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(dx * Speed, _rbody.velocity.y);//Keep y velocity same instead of at 0, might be good when we have jumping
        

        //flips sprite
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            PlayerJump.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            PlayerJump.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left *.5f, Vector2.down, 0.65f, GroundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right*.5f, Vector2.down, 0.65f, GroundLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool WasGrounded()
    {
        return Time.time - _lastJumpTime < CoyoteTime;
    }

}
