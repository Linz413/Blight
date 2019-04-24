using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaLure : MonoBehaviour
{

    private float aliveTime = 10f;
    public float rotateSpeed = 40f;
    private ArrayList students = new ArrayList();
    private float counter;
    public bool specialPizza = true;

    void Start()
    {
        counter = 0f;
    }

    public void Update()
    {
        //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        int stillPizza = Eaten();
    }

    void OnTriggerEnter(Collider c)
    {
        //print(c);
        if (c.attachedRigidbody != null)
        {
            StudentAI student = c.attachedRigidbody.gameObject.GetComponent<StudentAI>();
            if (student != null)
            { 
                if (counter <= aliveTime)
                {
                    student.StudentLured(true, this.gameObject.GetComponent<Rigidbody>());
                    students.Add(student);
                }
            }
        }
        //if (!specialPizza)
        //{
        //    StudentStop student = c.gameObject.GetComponent<StudentStop>();
        //    print(student);
        //    if (student != null)
        //    {
        //        if (counter <= aliveTime)
        //        {
        //            student.StudentLuredChild(true, this.gameObject.GetComponent<Rigidbody>());
        //            students.Add(student);
        //        }
        //    }
        //}
    }

    private void OnDestroy()
    {
        foreach (StudentAI student in students)
        {
            student.StudentLured(false, null);
        }
    }

    // 0 means pizza was destroyed
    // 1 means the pizza is still there
    public int Eaten()
    {
        counter += Time.deltaTime;
        if (counter >= aliveTime)
        {
            Destroy(this.gameObject);
            return 0;
        }
        return 1;
    }

    

}
