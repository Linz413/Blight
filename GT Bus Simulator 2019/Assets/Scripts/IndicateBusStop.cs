using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateBusStop : MonoBehaviour
{
    private StudentAI script;
    private GameObject student;
    // Start is called before the first frame update
    void Start()
    {
        student = transform.parent.gameObject;
        script = student.GetComponent<StudentAI>();
        if (!script.atBusStop)
        {
            transform.gameObject.SetActive(false);
        }
    }

}
