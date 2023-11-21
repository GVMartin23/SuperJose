using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class enemy_script : MonoBehaviour
{
    private Rigidbody2D _rbody;
    public float enemySpeed;
    public LayerMask _wallLayer;
    private bool _goingRight = true;


    // Start is called before the first frame update
    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //choose when to turn around
        Vector3 center = transform.position;
        Vector2 directions = _goingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(center, directions, 0.5f, _wallLayer);
        if (hit.collider != null)
        {
            _goingRight = !_goingRight;
        }

        // go in the chosen direction
        if (_goingRight)
        {
            _rbody.velocity = new Vector2(enemySpeed, _rbody.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = !_goingRight;
        }
        else
        {
            _rbody.velocity = new Vector2(-enemySpeed, _rbody.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = !_goingRight;
        }
    }
}