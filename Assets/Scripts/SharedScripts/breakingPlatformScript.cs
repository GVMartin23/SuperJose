using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakingPlatformScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    public GameObject snowFall;
    void Start()
    {
        snowFall.SetActive(false);
        snowFall.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Invoke("BreakPlatform", 1);
        }
    }

    void BreakPlatform()
    {
        gameObject.SetActive(false);
        snowFall.SetActive(true);
        Invoke("BuildPlatform", 5);
        

    }

    void BuildPlatform()
    {
        gameObject.SetActive(true);

    }

}
