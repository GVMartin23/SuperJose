using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Jose;

    private Rigidbody2D _joseRigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        _joseRigidBody = Jose.GetComponent<Rigidbody2D>();
        //Set camera at correct Y value
        transform.position = new Vector3(_joseRigidBody.transform.position.x, -1, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (Jose != null)
        {
            var offset = transform.position.x - Jose.transform.position.x;
            if (offset < -3 && _joseRigidBody.velocity.x > 0)
            {
                transform.position = new Vector3(Jose.transform.position.x - 2, -1, -10);
            }
            else if (offset > 3 && _joseRigidBody.velocity.x < 0)
            {
                transform.position = new Vector3(Jose.transform.position.x + 2, -1, -10);
            }
        }
    }
}