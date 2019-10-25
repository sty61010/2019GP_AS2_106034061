using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    // Start is called before the first frame update
    
    public AudioSource audioSourse;
    public AudioClip EnemyAttacked;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Bullet"){
            audioSourse.PlayOneShot(EnemyAttacked);
            Destroy(gameObject);
        }
    }
}
