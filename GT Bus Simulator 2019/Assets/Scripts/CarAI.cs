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
    private int currWaypoint;
    private float moveSpeed = 2;
    private float turnSpeed = 200;
    private float jumpForce = 4;
    private int itemsInTigger = 0;
    //public Rigidbody pizzaRigidBody = null;

    public GameObject[] waypoints;
    private CarState state;
    private float randomSpeed;

    private bool wasGrounded;
    private Vector3 currentDirection = Vector3.zero;
    private CarController m_CarController;

    private List<Collider> collisions = new List<Collider>();

    private float m_AccelWanderAmount = 0.1f;
    private float m_AccelWanderSpeed = 0.1f;
    private float m_ReachTargetThreshold = 2;
    private float desiredSpeed = 30f;
    private float m_BrakeSensitivity = 1f;
    private float m_AccelSensitivity = 0.04f;
    private float m_SteerSensitivity = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        state = CarState.Idle;
        currWaypoint = -1;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        randomSpeed = Random.value * 100;
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
        
        if (currWaypoint == waypoints.Length)
        {
            currWaypoint = -1;
        }
        print(state);
        //print(this.gameObject.GetComponent<Collider>());
        switch (state)
        {
            case CarState.Idle:
                state = CarState.Drive;
                break;
            case CarState.Drive:
                //float accelBrakeSensitivity = (desiredSpeed < m_CarController.CurrentSpeed)
                //                                  ? m_BrakeSensitivity
                //                                  : m_AccelSensitivity;

                //// decide the actual amount of accel/brake input to achieve desired speed.
                //float accel = Mathf.Clamp((desiredSpeed - m_CarController.CurrentSpeed) * accelBrakeSensitivity, -1, 1);

                //// add acceleration 'wander', which also prevents AI from seeming too uniform and robotic in their driving
                //// i.e. increasing the accel wander amount can introduce jostling and bumps between AI cars in a race
                //accel *= (1 - m_AccelWanderAmount) +
                //         (Mathf.PerlinNoise(Time.time * m_AccelWanderSpeed, randomSpeed) * m_AccelWanderAmount);

                //// calculate the local-relative position of the target, to steer towards
                //Vector3 localTarget = transform.InverseTransformPoint(waypoints[currWaypoint].transform.position);

                //// work out the local angle towards the target
                //float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

                //// get the amount of steering needed to aim the car towards the target
                //float steer = Mathf.Clamp(targetAngle * m_SteerSensitivity, -1, 1) * Mathf.Sign(m_CarController.CurrentSpeed);

                //m_CarController.Move(10f, 10f, 0f, 0f);
                if (!agent.pathPending && agent.remainingDistance == 0)
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
                m_CarController.Move(0, 0, -10f, -10f);
                break;
            case CarState.AvoidBus:
                
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

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
       
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.attachedRigidbody != null)
        {
            // a student has entered the trigger
            StudentAI student = c.attachedRigidbody.GetComponent<StudentAI>();
            if (student != null)
            {
                itemsInTigger++;
                state = CarState.SlowDownStudent;
            }
            // another car has entered the trigger
            CarAI otherCar = c.attachedRigidbody.GetComponent<CarAI>();
            if (otherCar != null)
            {
                itemsInTigger++;
                state = CarState.SlowDownOtherCar;
            }
            // the bus has entered the trigger
            PeopleCollection bus = c.attachedRigidbody.GetComponent<PeopleCollection>();
            if (bus != null)
            {
                itemsInTigger++;
                state = CarState.AvoidBus;
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
                itemsInTigger--;
            }
            // another car has entered the trigger
            CarAI otherCar = c.attachedRigidbody.GetComponent<CarAI>();
            if (otherCar != null)
            {
                itemsInTigger--;
            }
            // the bus has entered the trigger
            PeopleCollection bus = c.attachedRigidbody.GetComponent<PeopleCollection>();
            if (otherCar != null)
            {
                itemsInTigger--;
            }
        }
        if (itemsInTigger == 0)
        {
            state = CarState.Drive;
        }
    }
}
