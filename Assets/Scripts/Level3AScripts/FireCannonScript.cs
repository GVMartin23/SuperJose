using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class FireCannonScript : MonoBehaviour
{
    public GameObject cannon;
    bool _fire;

    // Start is called before the first frame update
    void Start()
    {
        _fire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fire == true)
        {
            _fire = false;
            Invoke("Fire", 1);
        }
        //else if (_fire == false)
        //{
        //    Invoke("Cease", 1);
        //}
    }

    void Fire()
    {
        cannon.GetComponent<ParticleSystem>().enableEmission = true;
        Invoke("Cease", 1);
    }

    void Cease()
    {
        cannon.GetComponent<ParticleSystem>().enableEmission = false;
        _fire = true;
    }
}
