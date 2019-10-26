using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    // public AudioSource audioSourse;
    // public AudioClip Boom;
    public Object ExplosionEffect;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter(Collision collsionObject)
    {
        Destroy(gameObject);
    }
}
