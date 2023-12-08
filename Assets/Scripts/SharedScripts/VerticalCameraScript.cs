using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCameraScript : MonoBehaviour
{
    public GameObject Jose;

    private JoseScript _joseScript;
    private Rigidbody2D _joseRigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        _joseRigidBody = Jose.GetComponent<Rigidbody2D>();
        _joseScript = FindObjectOfType<JoseScript>();
        //Set camera at correct Y value
        transform.position = new Vector3(_joseRigidBody.transform.position.x, _joseRigidBody.transform.position.y + 3.285f, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (Jose != null && !_joseScript._isDead)
        {
            transform.position = new Vector3(_joseRigidBody.transform.position.x, _joseRigidBody.transform.position.y + 3.285f, transform.position.z);

        }
    }
}
