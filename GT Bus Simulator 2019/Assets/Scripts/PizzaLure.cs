using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaLure : MonoBehaviour
{
    
    int studentsLured = 0;
    public float rotateSpeed = 40f;
    private ArrayList students = new ArrayList();

    void Start()
    {
        
    }

    public void Update()
    {
        //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider c)
    {
        print(c);
        if (c.attachedRigidbody != null)
        {
            print("WTF");
            StudentAI student = c.attachedRigidbody.gameObject.GetComponent<StudentAI>();
            if (student != null)
            {
                int stillPizza = Eaten();
                if (stillPizza == 1)
                {
                    student.StudentLured(true, this.gameObject.GetComponent<Rigidbody>());
                    students.Add(student);
                }
            }
        }
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
        studentsLured++;
        if (studentsLured >= 2)
        {
            Destroy(this.gameObject);
            return 0;
        }
        return 1;
    }

    

}
