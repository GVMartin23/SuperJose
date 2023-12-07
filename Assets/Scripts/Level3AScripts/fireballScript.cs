using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    Rigidbody2D _rbody;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (_rbody.transform.position.y < -3) 
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
