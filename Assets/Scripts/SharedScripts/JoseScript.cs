using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class JoseScript : MonoBehaviour
{
    public LayerMask GroundLayer;
    public LayerMask EnemyLayer;
    public string SceneName;
    public float Speed;
    public float JumpForce;
    private bool _startedJump = false;
    private bool _hasJumped = false;
    private bool _isDead = false;

    public GameObject PlayerJump;
    public GameObject PlayerWalk;
    private Rigidbody2D _rbody;
    private BoxCollider2D _boxCollider;

    public float CoyoteTime;
    private float _lastJumpTime = 0;

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
        if (Input.GetKeyDown(KeyCode.Space) && WasGrounded())
        {
            _startedJump = true;
            PlayerJump.SetActive(true);
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
        _rbody.velocity = new Vector2(xdir * Speed, _rbody.velocity.y);

        if (_startedJump)
        {
            var yforce = Vector2.up * JumpForce;
            _rbody.velocity += yforce;
            _startedJump = false;
            _hasJumped = true;
        }
    }

    //Handles all movement animations for Jose
    //Currently can only move left and right, jumping later
    private void HandleMovement()
    {
        //Facing left animations
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            PlayerJump.GetComponent<SpriteRenderer>().flipX = true;
            PlayerWalk.GetComponent<SpriteRenderer>().flipX = true;
        }

        //Facing right animations
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            PlayerJump.GetComponent<SpriteRenderer>().flipX = false;
            PlayerWalk.GetComponent<SpriteRenderer>().flipX = false;
        }

        //Set grounded animations
        if (!IsGrounded())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerJump.SetActive(true);
            PlayerWalk.SetActive(false);
        }
        else if (IsGrounded() && ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow))))
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

    //Check if grounded by raycasting on sides of Jose
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left * .5f, Vector2.down, 0.65f, GroundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.right * .5f, Vector2.down, 0.65f, GroundLayer);
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
        if (collision.gameObject.CompareTag("FroggyBoi") || collision.gameObject.CompareTag("SnowMan"))
        {
            FrogCollision(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Way to trigger to collide with Jose as enemies
        if (collision.gameObject.CompareTag("FroggyBoi") || collision.gameObject.CompareTag("SnowMan"))
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
        if (_rbody.velocity.y < 0.1f && (enemy.CompareTag("FroggyBoi") || enemy.CompareTag("SnowMan")))
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
        //enemy.GetComponent<BoxCollider2D>().enabled = false;
        //enemy.GetComponent<Rigidbody2D>().AddForce(20 * Vector2.up);
        
        Destroy(enemy);
        _rbody.AddForce(20 * JumpForce * Vector2.up);
    }
}