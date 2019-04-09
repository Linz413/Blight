using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public enum CarState
{
    Idle,
    Drive,
    SlowDownStudent,
    SlowDownOtherCar,
    AvoidBus
}
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class CarAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Rigidbody rigidBody;
    public int currWaypoint = -1;
    //public Rigidbody pizzaRigidBody = null;

    public GameObject[] waypoints;
    private CarState state;

    private Vector3 currentDirection = Vector3.zero;
    private CarController m_CarController;

    private List<Collider> collisions = new List<Collider>();

    private Transform target;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        state = CarState.Idle;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_CarController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currWaypoint == waypoints.Length)
        {
            currWaypoint = -1;
        }
        //print(state);
        switch (state)
        {
            case CarState.Idle:
                state = CarState.Drive;
                break;
            case CarState.Drive:
                m_CarController.Move(0, 2f, 0, 0);
                if (!agent.pathPending && agent.remainingDistance < 3)
                {
                    SetNextWaypoint();
                }
                break;
            case CarState.SlowDownStudent:
                agent.isStopped = true;
                agent.ResetPath();
                m_CarController.Move(0, 0, -1f, 1f);
                break;
            case CarState.SlowDownOtherCar:
                agent.isStopped = true;
                agent.ResetPath();
                m_CarController.Move(0, 0, -1f, 1f);
                break;
            case CarState.AvoidBus:
                // HAVE CAR HONK AUDIO
                agent.isStopped = true;
                agent.ResetPath();
                m_CarController.Move(0, 0, -1f, 1f);
                break;
            default:
                break;
        }
    }

    private void SetNextWaypoint()
    {
        if (waypoints.Length != 0)
        {
            currWaypoint += 1;
            agent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        else
        {
            Debug.Log("The waypoint array is empty.");
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.attachedRigidbody != null)
        {
            // a student has entered the trigger
            StudentAI student = c.attachedRigidbody.GetComponent<StudentAI>();
            if (student != null)
            {
                //print("STUDENT");
                if (!student.atBusStop)
                {
                    state = CarState.SlowDownStudent;
                }      
            }
            // another car has entered the trigger
            CarAI otherCar = c.attachedRigidbody.GetComponent<CarAI>();
            if (otherCar != null)
            {
                //print("OTHER CAR");
                state = CarState.SlowDownOtherCar;
            }
            // the bus has entered the trigger
            PeopleCollection bus = c.attachedRigidbody.GetComponent<PeopleCollection>();
            if (bus != null)
            {
                print("BUS");
                state = CarState.AvoidBus;
                target = bus.transform;
            }
        }   
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            // a student has entered the trigger
            StudentAI student = c.attachedRigidbody.GetComponent<StudentAI>();
            if (student != null)
            {
                state = CarState.Drive;
                agent.SetDestination(waypoints[currWaypoint].transform.position);
            }
            // another car has entered the trigger
            CarAI otherCar = c.attachedRigidbody.GetComponent<CarAI>();
            if (otherCar != null)
            {
                state = CarState.Drive;
                agent.SetDestination(waypoints[currWaypoint].transform.position);
            }
            // the bus has entered the trigger
            PeopleCollection bus = c.attachedRigidbody.GetComponent<PeopleCollection>();
            if (bus != null)
            {
                state = CarState.Drive;
                agent.SetDestination(waypoints[currWaypoint].transform.position);
            }
        } 
        
    }
}
