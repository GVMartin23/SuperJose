using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class JoseScript : MonoBehaviour
{
    public LayerMask GroundLayer;
    public LayerMask WallJumpLayer;
    public LayerMask EnemyLayer;
    public string SceneName;
    public float Speed;
    public float JumpForce;
    private bool _startedJump = false;
    private bool _hasJumped = false;
    private bool _isDead = false;
    private bool _goingRight = true;

    public GameObject PlayerJump;
    public GameObject PlayerWalk;
    private Rigidbody2D _rbody;
    private BoxCollider2D _boxCollider;

    public float CoyoteTime;
    private float _lastJumpTime = 0;

    [Header("Wall Jumping")]
    public Transform WallCheck;

    private bool _isWallTouch;
    private bool _isSliding;
    private bool _isWallJumping;
    public float WallSlidingSpeed;
    public Vector2 WallJumpForce;
    public float WallJumpDuration;

    // Start is called before the first frame update
    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();

        //Platforming jump rules
        //Checks if grounded to set last time grounded
        if (IsGrounded())
        {
            _lastJumpTime = Time.time;
        }

        //Can only jump when either grounded or within coyotetime
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startedJump = true;
        }

        //Allows for altering jump height
        if (Input.GetKeyUp(KeyCode.Space) && _hasJumped)
        {
            _hasJumped = false;
            float dy = _rbody.velocity.y > 0 ? _rbody.velocity.y / 5f : _rbody.velocity.y;
            _rbody.velocity = new Vector2(_rbody.velocity.x, dy);
        }
    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        //Movement inputs
        float xdir = Input.GetAxis("Horizontal");

        if (_startedJump && WasGrounded())
        {
            PlayerJump.SetActive(true);
            _rbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            _startedJump = false;
            _hasJumped = true;
        }

        //All Wall Jumping in method below found at https://www.youtube.com/watch?v=sfDnN-Im7rY
        _isWallTouch = Physics2D.OverlapBox(WallCheck.position, new Vector2(.3f, .5f), 0, WallJumpLayer);

        _isSliding = _isWallTouch && !IsGrounded() && xdir != 0;

        if (_isSliding)
        {
            _rbody.velocity = new Vector2(_rbody.velocity.x, Mathf.Clamp(_rbody.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }

        if (_isSliding && _startedJump)
        {
            _isWallJumping = true;
            Invoke(nameof(StopWallJump), WallJumpDuration);
        }

        if (_isWallJumping && !_hasJumped)
        {
            _rbody.velocity = new Vector2(-xdir * WallJumpForce.x, WallJumpForce.y);
            _startedJump = false;
            _hasJumped = true;
        }
        else if (!_isWallJumping)
        {
            _rbody.velocity = new Vector2(xdir * Speed, _rbody.velocity.y);
        }
    }

    private void StopWallJump()
    {
        _isWallJumping = false;
    }

    //Handles all movement animations for Jose
    //Currently can only move left and right, jumping later
    private void HandleMovement()
    {
        //Facing left animations
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow) && !_goingRight))
        {
            _goingRight = true;
            Flip();
        }

        //Facing right animations
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow) && _goingRight))
        {
            _goingRight = false;
            Flip();
        }

        //Set grounded animations
        if (!IsGrounded())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerJump.SetActive(true);
            PlayerWalk.SetActive(false);
        }
        else if (IsGrounded() && (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow))))
        {
            //Walking animations
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerJump.SetActive(false);
            PlayerWalk.SetActive(true);
        }
        else
        {
            //Jumping animations
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            PlayerJump.SetActive(false);
            PlayerWalk.SetActive(false);
        }
    }

    private void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = _goingRight;
        PlayerJump.GetComponent<SpriteRenderer>().flipX = _goingRight;
        PlayerWalk.GetComponent<SpriteRenderer>().flipX = _goingRight;
    }

    //Check if grounded by raycasting on sides of Jose
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.left * .4f), Vector2.down, 0.65f, GroundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (Vector3.right * .4f), Vector2.down, 0.65f, GroundLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool WasGrounded()
    {
        return Time.time - _lastJumpTime < CoyoteTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Chech for which door Jose has collided with, then loads correct level
        if (collision.gameObject.CompareTag("Level2ADoor"))
        {
            Invoke(nameof(LoadLevel2A), 2);
        }
        if (collision.gameObject.CompareTag("Level2BDoor"))
        {
            Invoke(nameof(LoadLevel2B), 1);
        }
        if (collision.gameObject.CompareTag("Level3ADoor"))
        {
            Invoke(nameof(LoadLevel3A), 1);
        }
        if (collision.gameObject.CompareTag("Level3BDoor"))
        {
            Invoke(nameof(LoadLevel3B), 1);
        }

        //Check if Jose has collided with an enemy or hazard
        if (collision.gameObject.CompareTag("FroggyBoi") || collision.gameObject.CompareTag("SnowMan") || collision.gameObject.CompareTag("NonDodgeableEnemy"))
        {
            FrogCollision(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Way to trigger to collide with Jose as enemies
        if (collision.gameObject.CompareTag("FroggyBoi") || collision.gameObject.CompareTag("SnowMan") || collision.gameObject.CompareTag("NonDodgeableEnemy"))
        {
            FrogCollision(collision.gameObject);
        }
    }

    #region Level Loading Methods for Invoking

    private void LoadLevel1()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    private void LoadLevel2A()
    {
        SceneManager.LoadScene("Level2AScene");
    }

    private void LoadLevel2B()
    {
        SceneManager.LoadScene("Level2BScene");
    }

    private void LoadLevel3A()
    {
        SceneManager.LoadScene("WinGameScene");
    }

    private void LoadLevel3B()
    {
        SceneManager.LoadScene("Level3BScene");
    }

    private void LoadLoseGame()
    {
        SceneManager.LoadScene("LoseGameScene");
    }

    private void LoadWinGame()
    {
        SceneManager.LoadScene("WinGameScene");
    }

    #endregion Level Loading Methods for Invoking

    //Handles any collisions for Enemies
    private void FrogCollision(GameObject enemy)
    {
        //Default to if velocity decreasing, Jose wins
        if (_rbody.velocity.y < -0.1f && (enemy.CompareTag("FroggyBoi") || enemy.CompareTag("SnowMan")))
        {
            //Kill enemy instead of Jose
            StompEnemy(enemy);
            return;
        }

        _isDead = true;
        //Reduce lives by one
        int lives = PlayerPrefs.GetInt("Lives");
        lives--;
        PlayerPrefs.SetInt("Lives", lives);

        _rbody.velocity = Vector2.up * 8f;
        _boxCollider.enabled = false;

        //If Lives = 0 go to lose game, else reset level
        if (lives <= 0)
        {
            Invoke(nameof(LoadLoseGame), 2f);
        }
        else
        {
            Invoke(SceneName, 2f);
        }
    }

    private void StompEnemy(GameObject enemy)
    {
        _rbody.velocity = new Vector2(_rbody.velocity.x, 0);

        Destroy(enemy);
        _rbody.AddForce(20 * JumpForce * Vector2.up);
    }
}