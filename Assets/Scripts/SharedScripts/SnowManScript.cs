using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManScript : MonoBehaviour
{
    public GameObject SnowBallPrefab;
    public LayerMask JoseLayer;
    public float SightLine;
    public float ShootSpeed;
    public float ShootDelay;

    [Header("Hack for Shooting Down")]
    public bool AlwaysShoot;

    private float _lastShot;
    private bool _goingRight;
    private float _halfWidth;
    private float _halfHeight;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        _goingRight = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _halfWidth = _spriteRenderer.bounds.size.x / 2;
        _halfHeight = _spriteRenderer.bounds.size.y / 2;
        _lastShot = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (DetectPlayer() || AlwaysShoot) Shoot();
    }

    //Shoot in direction of player
    private void Shoot()
    {
        //Shooting Delay
        if (Time.time - _lastShot < ShootDelay) return;

        //Calculate spawn position
        Vector3 spawnPoint = _goingRight ? transform.position + (Vector3.right * _halfWidth) : transform.position + (Vector3.left * _halfWidth);

        if (AlwaysShoot)
        {
            //Hack for shooting down
            spawnPoint = transform.position - Vector3.down;
        }

        GameObject SnowBall = Instantiate(SnowBallPrefab, spawnPoint, Quaternion.identity);

        //Set velocity
        int direction = _goingRight ? 1 : -1;
        SnowBall.GetComponent<Rigidbody2D>().velocity = new Vector2(ShootSpeed * direction, 0);

        //Put Particles behind snowball depending on direction
        SnowBall.transform.eulerAngles = new Vector3(0, 0, _goingRight ? 0 : 180);
        _lastShot = Time.time;

        if (AlwaysShoot)
        {
            //Hack for shooting down
            SnowBall.GetComponent<Rigidbody2D>().velocity = Vector2.down * ShootSpeed;
            SnowBall.transform.eulerAngles = new Vector3(0, 0, 90);
        }
    }

    //Detect if Player is able to be seen by snowman
    private bool DetectPlayer()
    {
        Vector3 topCenter = transform.position + (Vector3.up * _halfHeight / 2);
        Vector3 bottomCenter = transform.position + (Vector3.down * _halfHeight / 2);

        //Check 4 corners of sprite, raycast our a set distance for Jose
        RaycastHit2D hitTopLeft = Physics2D.Raycast(topCenter, Vector2.left, SightLine, JoseLayer);
        RaycastHit2D hitBotLeft = Physics2D.Raycast(bottomCenter, Vector2.left, SightLine, JoseLayer);
        RaycastHit2D hitTopRight = Physics2D.Raycast(topCenter, Vector2.right, SightLine, JoseLayer);
        RaycastHit2D hitBotRight = Physics2D.Raycast(bottomCenter, Vector2.right, SightLine, JoseLayer);

        bool hitLeft = hitTopLeft.collider != null || hitBotLeft.collider != null;
        bool hitRight = hitTopRight.collider != null || hitBotRight.collider != null;

        //See Infront
        if ((hitRight && _goingRight) || (hitLeft && !_goingRight))
        {
            return true;
        }
        else if (hitLeft || hitRight) //See Behind
        {
            Flip();
            return true;
        }

        return false;
    }

    private void Flip()
    {
        _goingRight = !_goingRight;
        _spriteRenderer.flipX = _goingRight;
    }
}