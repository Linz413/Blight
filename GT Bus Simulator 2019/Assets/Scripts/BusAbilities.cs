﻿using System.Collections;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 up = new Vector3(0, 1, 0);
            Vector3 forceToApply = 5 * up;
            bus.AddForce(forceToApply, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 up = new Vector3(0, 1, 0);
            Rigidbody go = Instantiate(projectile, (bus.position + up), projectile.rotation);
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
            position = Camera.main.ScreenToWorldPoint(position);
            go.gameObject.GetComponent<PizzaLure>().specialPizza = false;
            go.transform.LookAt(position);    
            //Debug.Log(position);
            go.velocity = go.transform.forward * 10;
        }

    }
}