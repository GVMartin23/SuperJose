using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class JoseScript : MonoBehaviour
{
    public LayerMask GroundLayer;
    public float Speed;
    public float JumpForce;
    bool _startedJump = false;
    bool _hasJumped = false;

    public GameObject PlayerJump;
    public GameObject PlayerWalk;
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
            PlayerJump.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space) && _hasJumped)
        { 
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

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            PlayerJump.GetComponent<SpriteRenderer>().flipX = true;
            PlayerWalk.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            PlayerJump.GetComponent<SpriteRenderer>().flipX = false;
            PlayerWalk.GetComponent<SpriteRenderer>().flipX = false;

        }



        if (!IsGrounded())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerJump.SetActive(true);
            PlayerWalk.SetActive(false);
        }
        else if (IsGrounded() && ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow))))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerJump.SetActive(false);
            PlayerWalk.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            PlayerJump.SetActive(false);
            PlayerWalk.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level2ADoor"))
        {
            Invoke("LoadLevel2A", 1);
        }
        if (collision.gameObject.CompareTag("Level2BDoor"))
        {
            Invoke("LoadLevel2B", 1);
        }
        if (collision.gameObject.CompareTag("Level3ADoor"))
        {
            Invoke("LoadLevel3A", 1);
        }
        if (collision.gameObject.CompareTag("Level3BDoor"))
        {
            Invoke("LoadLevel3B", 1);
        }
    }

    void LoadLevel2A()
    {
        SceneManager.LoadScene("Level2AScene");
    }

    void LoadLevel2B()
    {
        SceneManager.LoadScene("Level2BScene");
    }

    void LoadLevel3A()
    {
        SceneManager.LoadScene("Level3AScene");
    }

    void LoadLevel3B()
    {
        SceneManager.LoadScene("Level3BScene");
    }
}
