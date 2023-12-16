using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class moving_platform_script : MonoBehaviour
{
    public bool UpDown;
    public float UpperBound;
    public float LowerBound;
    public int speed;
    private Rigidbody2D _rbody;

    // Start is called before the first frame update
    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _rbody.velocity = UpDown ? Vector2.up * speed : Vector2.right * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        //Moves either up and down in  given range or left to right
        if (UpDown)
        {
            Vector2 position = _rbody.transform.position;
            float posY = position.y;

            if (posY > UpperBound)
            {
                _rbody.velocity = Vector2.down * speed;
            }
            else if (posY < LowerBound)
            {
                _rbody.velocity = Vector2.up * speed;
            }
        }
        else
        {
            Vector2 pos = _rbody.transform.position;
            float posX = pos.x;

            if (posX > UpperBound)
            {
                _rbody.velocity = Vector2.left * speed;
            }
            else if (posX < LowerBound)
            {
                _rbody.velocity = Vector2.right * speed;
            }
        }
    }
}