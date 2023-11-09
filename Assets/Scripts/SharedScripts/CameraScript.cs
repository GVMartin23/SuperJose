using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Jose;

    private Vector3 offset = new Vector3(0, -1, -10);

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void LateUpdate()
    {
        var yPos = offset.y;

        if (Jose != null)
        {
            if (Jose.transform.position.x >= 45)
            {
                transform.position = new Vector3(45, yPos, offset.z);
            }
            else if (Jose.transform.position.x <= -45)
            {
                transform.position = new Vector3(-45, yPos, offset.z);
            }
            else
            {
                transform.position = new Vector3(Jose.transform.position.x, yPos, offset.z);
            }
        }
    }
}