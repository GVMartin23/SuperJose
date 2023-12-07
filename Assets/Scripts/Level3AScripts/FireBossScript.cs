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
    public GameObject fireball;
    private Rigidbody2D _rbody;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = boss.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Math.Abs(Vector2.Distance(boss.transform.position, jose.transform.position));

        if ( distance < range && jose.transform.position.x < 8)
        {
            Chase();
        }
    }

    void Shoot()
    {
        GameObject _fireB = Instantiate(fireball, _rbody.transform.position, Quaternion.identity);
        Rigidbody2D _fireBB = _fireB.GetComponent<Rigidbody2D>();
        Vector3 aim = jose.transform.position - transform.position;
        Vector3 aimNormalized = aim.normalized;
        _fireBB.velocity = aimNormalized * 4;
    }

    void Chase()
    {
        System.Random rand = new System.Random();

        float xDiff = jose.transform.position.x - transform.position.x;
        float curY = _rbody.velocity.y;
        float yVel = (float)(rand.NextDouble() * 2 - 1);

        if (boss.transform.position.y > yUpBound)
        {
            Vector3 direction = jose.transform.position - transform.position;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 velocity = normalizedDirection * speed;
            _rbody.velocity = velocity;
        }
        else if (boss.transform.position.y < yLowBound)
        {
            Vector2 newVel = new Vector2(xDiff, 1);
            newVel.Normalize();
            _rbody.velocity = newVel * speed;
        }
        else
        {
             Vector2 newVel = new Vector2(xDiff, curY + yVel);
             newVel.Normalize();
             _rbody.velocity = newVel * speed;
        }

        int chance = UnityEngine.Random.Range(0, 1000);
        if (chance < 3)
        {
            Shoot();
        }
    }
}










//void Wander()
//{
//    int chance = UnityEngine.Random.Range(0, 1000);
//    if (chance < 2)
//    {
//        System.Random rand = new System.Random();
//        float curX = _rbody.velocity.x;
//        float curY = _rbody.velocity.y;
//
//        float xVel = (float)(rand.NextDouble() * 2 - 1);
//        float yVel = (float)(rand.NextDouble() * 2 - 1);
//
//        if (boss.transform.position.y > yUpBound)
//        {
//            Vector2 tooFarUp = new Vector2(curX, -1);
//            tooFarUp.Normalize();
//            _rbody.velocity = tooFarUp * speed;
//        }
//        if (boss.transform.position.y <= yLowBound)
//        {
//            Vector2 tooFarDown = new Vector2(curX, 1);
//            tooFarDown.Normalize();
//            _rbody.velocity = tooFarDown * speed;
//        }
//        if (boss.transform.position.x > xRightBound)
//        {
//            Vector2 tooFarLeft = new Vector2(1, curY);
//            tooFarLeft.Normalize();
//            _rbody.velocity = tooFarLeft * speed;
//        }
//        if (boss.transform.position.x <= xLeftBound)
//        {
//            Vector2 tooFarRight = new Vector2(-1, curY);
//            tooFarRight.Normalize();
//            _rbody.velocity = tooFarRight * speed;
//        }
//        else
//        {
//            Vector2 newVel = new Vector2(curX + xVel, curY + yVel);
//            newVel.Normalize();
//            _rbody.velocity = newVel * speed;
//        }
//    }
//}