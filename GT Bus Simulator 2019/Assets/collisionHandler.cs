using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public  float duration;
    private bool active = false;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") {
            active = true;
            rb.useGravity = true;
        }
           

    }
    private void FixedUpdate()
    {
        if (active)
        {
            duration -= Time.deltaTime;
        }
        if(duration <= 0)
        {
            this.gameObject.SetActive(false);
        }   
    }
}
