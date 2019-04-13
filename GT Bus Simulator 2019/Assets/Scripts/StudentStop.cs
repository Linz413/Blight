using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentStop : MonoBehaviour
{
    public StudentAI script;
    public GameObject student;
    // Start is called before the first frame update
    void Start()
    {
        student = transform.parent.gameObject;
        script = student.GetComponent<StudentAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            PeopleCollection busPickUp = other.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
            if (busPickUp != null)
            {
                script.stoppedForBus = true;
                script.StopStudent();
            }
            //StudentAI studentTwo = other.attachedRigidbody.GetComponent<StudentAI>();
            //if (studentTwo != null)
            //{
            //    script.StopStudent();
            //}
        }
           
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            PeopleCollection bus = c.attachedRigidbody.GetComponent<PeopleCollection>();
            if (bus != null)
            {
                script.ContinueStudent();
            }
            //StudentAI studentTwo = c.attachedRigidbody.GetComponent<StudentAI>();
            //if (studentTwo != null)
            //{
            //    script.ContinueStudent();
            //}
        }
    }
}
