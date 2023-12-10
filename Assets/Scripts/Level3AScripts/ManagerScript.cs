using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject jose;
    public GameObject fireball;
    ObjectPool fireballPool;

    // Start is called before the first frame update
    void Start()
    {
        fireballPool = new ObjectPool(fireball, true, 10);
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
        GameObject fireBall = GetFireball();
        int location = Random.Range(-25, -3);
        Vector2 pos = new Vector2(location, 6);
        Rigidbody2D _fireBB = fireBall.GetComponent<Rigidbody2D>();
        _fireBB.gravityScale = 1;
        fireBall.transform.position = pos;
        fireBall.SetActive(true);
    }

    public GameObject GetFireball()
    {
        return fireballPool.GetObject();
    }
}