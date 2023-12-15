using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

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
    private ManagerScript _manager;

    // Start is called before the first frame update
    private void Start()
    {
        //Get references to manger script and rigidbody
        _manager = FindAnyObjectByType<ManagerScript>();
        _rbody = boss.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Follow player if out of set range
        var distance = Math.Abs(Vector2.Distance(boss.transform.position, jose.transform.position));

        if (distance < range && jose.transform.position.x < 8)
        {
            Chase();
        }
    }

    private void Shoot()
    {
        //Get fireball
        GameObject fireBall = _manager.GetFireball();
        Rigidbody2D _fireBB = fireBall.GetComponent<Rigidbody2D>();

        fireBall.transform.position = gameObject.transform.position;

        //Shoot fireball in a cone around the player
        int rand = UnityEngine.Random.Range(0, 5);
        Vector3 aim = jose.transform.position - transform.position;
        aim.x -= rand;
        //Vector3 aimNormalized = aim.normalized;
        _fireBB.velocity = aim * 0.5f;

        fireBall.SetActive(true);
    }

    private void Chase()
    {
        //Randomize movment towards player
        System.Random rand = new System.Random();

        float xDiff = jose.transform.position.x - transform.position.x;
        float curY = _rbody.velocity.y;
        float yVel = (float)(rand.NextDouble() * 2 - 1);

        if (boss.transform.position.y > yUpBound) //Move down if too far away
        {
            Vector3 direction = jose.transform.position - transform.position;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 velocity = normalizedDirection * speed;
            _rbody.velocity = velocity;
        }
        else if (boss.transform.position.y < yLowBound) //Move up if too close
        {
            Vector2 newVel = new Vector2(xDiff, 1);
            newVel.Normalize();
            _rbody.velocity = newVel * speed;
        }
        else
        {
            //Randomly move in range
            Vector2 newVel = new Vector2(xDiff, curY + yVel);
            newVel.Normalize();
            _rbody.velocity = newVel * speed;
        }

        //Randomly shoot
        int chance = UnityEngine.Random.Range(0, 1000);
        if (chance < 5)
        {
            Shoot();
        }
    }
}