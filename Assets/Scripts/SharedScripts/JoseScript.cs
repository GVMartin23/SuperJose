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
    private bool _goingRight = true;
    private bool _canStartJump = true;
    private bool _inWallJump = false;

    public GameObject PlayerJump;
    public GameObject PlayerWalk;
    private Rigidbody2D _rbody;
    private BoxCollider2D _boxCollider;

    public float CoyoteTime;
    private float _lastJumpTime = 0;
    public bool _isDead = false;

    [Header("Wall Jumping")]
    public Transform WallCheckRight;

    public Transform WallCheckLeft;
    private bool _isWallTouch;
    private bool _isSliding;
    private bool _isWallJumping;
    public float WallSlidingSpeed;
    public Vector2 WallJumpForce;
    public float WallJumpDuration;

    private AudioSource _audioSource;
    public AudioClip winGame;
    public AudioClip loseGame;
    public AudioClip die;
    public AudioClip finishLevel;
    public AudioClip jump;
    public AudioClip zoom;
    public bool stopMusic = false;

    private StopMusic _stopMusic;
    private bool _canDie;

    private SpriteRenderer _spriteRenderer;
    private GameObject walking;
    private GameObject jumping;

    // Start is called before the first frame update
    private void Start()
    {
        Scene s = SceneManager.GetActiveScene();

        if (s.name == "Level2AScene" || s.name == "Level3Scene")
        {
            _goingRight = false;
        }

        //Initialize auido and collider
        _canDie = true;
        _stopMusic = FindObjectOfType<StopMusic>();
        _audioSource = GetComponent<AudioSource>();
        _rbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (Input.GetKeyDown(KeyCode.Space) && _canStartJump)
        {
            _startedJump = true;
            _canStartJump = false;
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

        if (_startedJump && WasGrounded() && !_inWallJump)
        {
            PlayerJump.SetActive(true);
            _audioSource.PlayOneShot(jump);
            _rbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            _startedJump = false;
            _hasJumped = true;
            _canStartJump = true;
        }

        //All Wall Jumping in method below found at https://www.youtube.com/watch?v=sfDnN-Im7rY
        bool isWallTouchRight = Physics2D.OverlapBox(WallCheckRight.position, new Vector2(.3f, .5f), 0, WallJumpLayer);
        bool isWallTouchLeft = Physics2D.OverlapBox(WallCheckLeft.position, new Vector2(.3f, .5f), 0, WallJumpLayer);
        _isWallTouch = isWallTouchLeft || isWallTouchRight;

        _isSliding = _isWallTouch && !IsGrounded() && xdir != 0;

        //Set sliding speed
        if (_isSliding)
        {
            _rbody.velocity = new Vector2(_rbody.velocity.x, Mathf.Clamp(_rbody.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }

        //Start Wall Jump
        if (_isSliding && _startedJump)
        {
            _isWallJumping = true;
            Invoke(nameof(StopWallJump), WallJumpDuration);
        }

        //Used to make sure the player is looking at the wall when kicking
        //Should prevent spamming spacebar to wall jump
        bool movingRight = xdir == 1;
        bool canWallJump = _isWallJumping && !_hasJumped && ((movingRight && isWallTouchRight) || (!movingRight && isWallTouchLeft));

        if (canWallJump)
        {
            xdir = isWallTouchLeft ? -1 : 1; //Was weird when looking away from wall sliding on
            _rbody.velocity = new Vector2(-xdir * WallJumpForce.x, WallJumpForce.y);
            _startedJump = false;
            _hasJumped = true;
            _inWallJump = true;
        }
        else if (!_isWallJumping && !_inWallJump)
        {
            _rbody.velocity = new Vector2(xdir * Speed, _rbody.velocity.y);
        }
    }

    private void StopWallJump()
    {
        //Allows for new Jump
        _isWallJumping = false;
        _canStartJump = true;
        Invoke(nameof(StopInWallJump), .15f);
    }

    private void StopInWallJump()
    {
        //Enables movement inputs
        _inWallJump = false;
    }

    //Handles all movement animations for Jose
    //Currently can only move left and right, jumping later
    private void HandleMovement()
    {
        //Facing left animations
        if ((Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)) && !_goingRight))
        {
            _goingRight = true;
            Flip();
        }

        //Facing right animations
        if ((Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)) && _goingRight))
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
        else if ((IsGrounded() || _isSliding) && (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow))))
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
        //Reverses sprites
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
        _canStartJump = true;
        Invoke(nameof(StopInWallJump), .15f);

        //Chech for which door Jose has collided with, then loads correct level
        if (collision.gameObject.CompareTag("Level2ADoor"))
        {
            stopMusic = true;
            _stopMusic.Stop();
            _goingRight = false;
            _audioSource.PlayOneShot(finishLevel);
            Invoke(nameof(LoadLevel2A), 2);
        }
        else if (collision.gameObject.CompareTag("Level2BDoor"))
        {
            stopMusic = true;
            _stopMusic.Stop();
            _audioSource.PlayOneShot(finishLevel);
            Invoke(nameof(LoadLevel2B), 2);
        }
        else if (collision.gameObject.CompareTag("Level3ADoor"))
        {
            stopMusic = true;
            _stopMusic.Stop();
            _audioSource.PlayOneShot(finishLevel);
            _goingRight = false;
            Invoke(nameof(LoadLevel3A), 2);
        }
        else if (collision.gameObject.CompareTag("Level3BDoor"))
        {
            stopMusic = true;
            _stopMusic.Stop();
            _audioSource.PlayOneShot(finishLevel);
            Invoke(nameof(LoadLevel3B), 2);
        }
        else if (collision.gameObject.CompareTag("WinGame"))
        {
            _canDie = false;
            stopMusic = true;
            _stopMusic.Stop();
            _audioSource.PlayOneShot(winGame);
            Invoke(nameof(LoadWinGame), 4);
        }

        //Check if Jose has collided with an enemy or hazard
        if (collision.gameObject.CompareTag("FroggyBoi") || collision.gameObject.CompareTag("SnowMan") || collision.gameObject.CompareTag("NonDodgeableEnemy") || collision.gameObject.CompareTag("firebox"))
        {
            FrogCollision(collision.gameObject);
        }

        //Pepper collision
        if (collision.gameObject.CompareTag("pepper"))
        {
            //Increases Joses Speed and Jump Force
            Speed *= 1.5f;
            JumpForce *= 1.5f;
            _audioSource.PlayOneShot(zoom);
            //Change color of Jose to red
            _spriteRenderer.color = Color.red;
            PlayerWalk.GetComponent<SpriteRenderer>().color = Color.red;
            PlayerJump.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke("UnPepper", 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Way to trigger to collide with Jose as enemies
        if (collision.gameObject.CompareTag("FroggyBoi") || collision.gameObject.CompareTag("SnowMan") || collision.gameObject.CompareTag("NonDodgeableEnemy") || collision.gameObject.CompareTag("firebox"))
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
        SceneManager.LoadScene("Level3AScene");
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
        if (_canDie == true)
        {
            //Default to if velocity decreasing, Jose wins
            if (_rbody.velocity.y < -0.01f && (enemy.CompareTag("FroggyBoi") || enemy.CompareTag("SnowMan")))
            {
                //Kill enemy instead of Jose
                StompEnemy(enemy);
                return;
            }

            _audioSource.PlayOneShot(die);

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
                stopMusic = true;
                _stopMusic.Stop();
                _audioSource.PlayOneShot(loseGame);

                Invoke(nameof(LoadLoseGame), 2f);
            }
            else
            {
                Invoke(SceneName, 2f);
            }
        }
    }

    private void StompEnemy(GameObject enemy)
    {
        _rbody.velocity = new Vector2(_rbody.velocity.x, 0);

        Destroy(enemy);
        _rbody.AddForce(20 * JumpForce * Vector2.up);
    }

    private void UnPepper()
    {
        // Reset Speed and Jumping, and Jose's color
        PlayerWalk.GetComponent<SpriteRenderer>().color = Color.white;
        PlayerJump.GetComponent<SpriteRenderer>().color = Color.white;
        _spriteRenderer.color = Color.white;
        Speed /= 1.5f;
        JumpForce /= 1.5f;
    }
}