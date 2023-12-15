using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        //Get animator
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If colliding with player, OpenDoor
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        //Open door animation
        _animator.SetBool("Opened", true);
    }
}