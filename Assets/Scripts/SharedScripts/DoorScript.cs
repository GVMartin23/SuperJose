using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    BoxCollider2D boxCol;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _animator.SetBool("Opened", true);
        
    }


}
