using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class FireCannonScript : MonoBehaviour
{
    public GameObject cannon;
    bool _fire;
    string tagToFind = "firebox";
    private List<GameObject> colliderList = new List<GameObject> ();


    // Start is called before the first frame update
    void Start()
    {
        _fire = true;
        PopulateGameObjectsList();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fire == true)
        {
            _fire = false;
            Invoke("Fire", 2);
        }
    }

    void Fire()
    {
        cannon.GetComponent<ParticleSystem>().enableEmission = true;

        foreach (GameObject obj in colliderList)
        {
            BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();

            boxCollider.enabled = true;
        }
        Invoke("Cease", 1);
    }

    void Cease()
    {
        cannon.GetComponent<ParticleSystem>().enableEmission = false;

        foreach (GameObject obj in colliderList)
        {
            BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();

            boxCollider.enabled = false;
        }
        _fire = true;
    }

    void PopulateGameObjectsList()
    {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tagToFind);
        colliderList.AddRange(foundObjects);
    }
}
