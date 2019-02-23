using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusAbilities : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody bus;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Rigidbody clone;
            clone = (Rigidbody) Instantiate(projectile, bus.position, projectile.rotation);
            clone.velocity = transform.forward * 20;
        }

    }
}
