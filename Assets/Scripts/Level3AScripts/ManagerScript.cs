using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject jose;
    public GameObject fireball;
    private Rigidbody2D _fireBody;

    // Start is called before the first frame update
    void Start()
    {
        _fireBody = fireball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jose.transform.position.x < 10)
        {
            int chance = Random.Range(0, 1000);
            if (chance < 5)
            {
                Rain();
            }
        }
    }

    void Rain()
    {
        int location = Random.Range(-25, -3);
        Vector2 pos = new Vector2(location, 6);
        GameObject _fireB = Instantiate(fireball, pos, Quaternion.identity);
        Rigidbody2D _fireBB = _fireB.GetComponent<Rigidbody2D>();
        _fireBB.gravityScale = 1;
    }
}