using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBoulderScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy boulder when colliding with special object
        if (collision.gameObject.CompareTag("SNOWEND"))
        {
            Destroy(gameObject);
        }
    }
}