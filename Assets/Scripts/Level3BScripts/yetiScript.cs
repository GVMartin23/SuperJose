using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class yetiScript : MonoBehaviour
{

    Animator animator;
    public GameObject snowBallPrefab;
    float throwTime;
    // Start is called before the first frame update
    void Start()
    {
        throwTime = 0f;
        animator = GetComponent<Animator>();
        //Invoke("ThrowSnowBall", 1.15f);
    }

    // Update is called once per frame
    void Update()
    {
        throwTime += Time.deltaTime;

        //Invoke("ThrowSnowBall", 2.5f);
        if (throwTime > 3f && !animator.GetBool("canThrow"))
        {
            animator.SetBool("canThrow", true);
            Invoke("ThrowSnowBall", 1.15f);
            throwTime = 0f;
        }
    }

    void ThrowSnowBall()
    {
        var snoowball = Instantiate(snowBallPrefab,new Vector3(7.34f,39.5f,0),Quaternion.identity);
        snoowball.GetComponent<Rigidbody2D>().AddForce(new Vector3(10, 0, 0));
        animator.SetBool("canThrow", false);
        
    }

    
}

