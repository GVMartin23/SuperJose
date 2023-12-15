using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireCannonScript : MonoBehaviour
{
    public GameObject cannon;
    private bool _fire;
    private string tagToFind = "firebox";
    private List<GameObject> colliderList = new List<GameObject>();

    public AudioClip whoosh;
    private AudioSource _audiosource;

    // Start is called before the first frame update
    private void Start()
    {
        //Enables fire, locates all firecannon colliders
        _fire = true;
        PopulateGameObjectsList();

        //Sets audio volume
        _audiosource = GetComponent<AudioSource>();
        _audiosource.volume = 0.1f;
    }

    // Update is called once per frame
    private void Update()
    {
        //If _fire, call Whoosh and Fire
        if (_fire == true)
        {
            _fire = false;
            Invoke("Whoosh", 1.3f);
            Invoke("Fire", 2);
        }
    }

    private void Fire()
    {
        //Enables cannon particles
        cannon.GetComponent<ParticleSystem>().enableEmission = true;

        //Enables all canaon colliders
        foreach (GameObject obj in colliderList)
        {
            BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();

            boxCollider.enabled = true;
        }

        //Call cease 1 second later
        Invoke("Cease", 1);
    }

    private void Cease()
    {
        //Disables particles
        cannon.GetComponent<ParticleSystem>().enableEmission = false;

        //Disables colliders
        foreach (GameObject obj in colliderList)
        {
            BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();

            boxCollider.enabled = false;
        }

        _fire = true;
    }

    private void PopulateGameObjectsList()
    {
        //Get all firecannon colliders
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tagToFind);
        colliderList.AddRange(foundObjects);
    }

    private void Whoosh()
    {
        //Play audio
        _audiosource.PlayOneShot(whoosh);
    }
}