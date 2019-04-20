using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeCollisionScript : MonoBehaviour
{
    public AudioClip audio;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = audio;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.rigidbody)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
