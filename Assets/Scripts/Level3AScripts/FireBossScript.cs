using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Rigidbody2D))]

public class FireBossScript : MonoBehaviour
{
    public float xLeftBound;
    public float xRightBound;
    public float yLowBound;
    public float yUpBound;
    public float speed;
    public float range;
    public float rateOfFire;
    public GameObject jose;
    public GameObject boss;
    private Rigidbody2D _josePos;
    private Rigidbody2D _rbody;
    private bool _isChase;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = boss.GetComponent<Rigidbody2D>();
        _josePos = jose.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Math.Abs(Vector2.Distance(boss.transform.position, jose.transform.position));

        if (_isChase == false && distance < range)
        {
        //  _isChase = true;
            Chase();
        }
        else if (distance >= range)
        {
            _isChase = false;
            Wander();
        }
    }

    void Shoot()
    {

    }

    void Wander()
    {
        int chance = UnityEngine.Random.Range(0, 1000);
        if (chance < 2 )
        {
            System.Random rand = new System.Random();
            float curX = _rbody.velocity.x;
            float curY = _rbody.velocity.y;

            float xVel = (float)(rand.NextDouble() * 2 - 1);
            float yVel = (float)(rand.NextDouble() * 2 - 1);

            if (boss.transform.position.y > yUpBound)
            {
                Vector2 tooFarUp = new Vector2(curX, -1);
                tooFarUp.Normalize();
                _rbody.velocity = tooFarUp * speed;
            }
            if (boss.transform.position.y <= yLowBound)
            {
                Vector2 tooFarDown = new Vector2(curX, 1);
                tooFarDown.Normalize();
                _rbody.velocity = tooFarDown * speed;
            }
            if (boss.transform.position.x > xRightBound)
            {
                Vector2 tooFarLeft = new Vector2(1, curY);
                tooFarLeft.Normalize();
                _rbody.velocity = tooFarLeft * speed;
            }
            if (boss.transform.position.x <= xLeftBound)
            {
                Vector2 tooFarRight = new Vector2(-1, curY);
                tooFarRight.Normalize();
                _rbody.velocity = tooFarRight * speed;
            }
            else
            {
                Vector2 newVel = new Vector2(curX + xVel, curY + yVel);
                newVel.Normalize();
                _rbody.velocity = newVel * speed;
            }
        }
    }

    void Chase()
    {
        _rbody.transform.position = Vector2.MoveTowards(boss.transform.position, jose.transform.position, speed);
        int chance = UnityEngine.Random.Range(0, 100);
        if (chance < 2)
        {
            Shoot();
        }
    }
}