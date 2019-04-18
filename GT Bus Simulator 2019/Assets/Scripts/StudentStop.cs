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
            WheelDrive busPickUp = other.attachedRigidbody.gameObject.GetComponent<WheelDrive>();
            PeopleCollection peopleColl = other.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
            if (busPickUp != null && peopleColl != null && busPickUp.velocity < 5f)
            {
                if (!script.atBusStop)
                {
                    script.stoppedForBus = true;
                    script.StopStudent();
                } 
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            WheelDrive busPickUp = other.attachedRigidbody.gameObject.GetComponent<WheelDrive>();
            PeopleCollection peopleColl = other.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
            if (busPickUp != null && peopleColl != null && busPickUp.velocity < 3f)
            {
                if (script.atBusStop)
                {
                    script.GoToBus(busPickUp, peopleColl);
                }
            }
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
        }
    }
}
