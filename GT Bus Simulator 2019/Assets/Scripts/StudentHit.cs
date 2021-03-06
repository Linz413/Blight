﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentHit : MonoBehaviour
{
    public StudentAI script;
    public GameObject student;
    // Start is called before the first frame update
    void Start()
    {
        student = transform.parent.gameObject;
        script = student.GetComponent<StudentAI>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (script.atBusStop)
        {
            if (other.attachedRigidbody != null)
            {
                PeopleCollection busPickUp = other.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
                if (busPickUp != null)
                {
                    script.PickedUp(busPickUp);
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (script.atBusStop)
        {
            if (c.attachedRigidbody != null)
            {
                PeopleCollection busPickUp = c.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
                if (busPickUp != null && script.state.Equals(StudentState.PickedUp))
                {
                    script.PickedUp(busPickUp);
                }
            }
        }
        else
        {
            if (c.attachedRigidbody != null)
            {
                PeopleCollection busPickUp = c.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
                if (busPickUp != null && script.hasBeenHit != true)
                {
                    script.HitStudent(busPickUp);
                }
            }
        }
    }
}
