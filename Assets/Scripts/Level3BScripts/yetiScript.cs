using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class yetiScript : MonoBehaviour
{
    private Animator animator;
    public GameObject snowBallPrefab;
    private float throwTime;

    // Start is called before the first frame update
    private void Start()
    {
        //Initialiaze
        throwTime = 0f;
        animator = GetComponent<Animator>();
        //Invoke("ThrowSnowBall", 1.15f);
    }

    // Update is called once per frame
    private void Update()
    {
        throwTime += Time.deltaTime;

        //If past certain time, and animation can play
        //Throw new snowball
        if (throwTime > 9f && !animator.GetBool("canThrow"))
        {
            animator.SetBool("canThrow", true);
            Invoke("ThrowSnowBall", 1.15f);
            throwTime = 0f;
        }
    }

    private void ThrowSnowBall()
    {
        //Create new snowball
        var snoowball = Instantiate(snowBallPrefab, new Vector3(7.34f, 39.5f, 0), Quaternion.identity);
        //snoowball.GetComponent<Rigidbody2D>().AddForce(new Vector3(-5, 0, 0));
        //Reset canThrow
        animator.SetBool("canThrow", false);
    }
}