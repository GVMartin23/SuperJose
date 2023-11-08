using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Jose;

    private Vector3 offset = new Vector3(0, 0, -10);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (Jose != null)
        {

            if (Jose.transform.position.x >= 45)
            {
                transform.position = new Vector3(45, offset.y, offset.z);
            }
            else if (Jose.transform.position.x <= -45)
            {
                transform.position = new Vector3(-45, offset.y, offset.z);
            }
            else
            {

                transform.position = new Vector3(Jose.transform.position.x, offset.y, offset.z);

            }
        }
    }
}
