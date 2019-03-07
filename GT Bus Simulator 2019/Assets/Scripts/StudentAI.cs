using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StudentState
{
    Idle,
    Walk,
    Pizza,
    Hit,
    PickedUp
};

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
public class StudentAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private UnityEngine.AI.NavMeshHit hit;
    private Rigidbody rigidBody;
    private Animator anim;
    public int currWaypoint = -1;
    private float moveSpeed = 2;
    private float turnSpeed = 200;
    private float jumpForce = 4;
    public Rigidbody pizzaRigidBody = null;

    public GameObject[] waypoints;
    private StudentState state;

    private readonly float interpolation = 10;
    private readonly float walkScale = 0.33f;
    private readonly float backwardsWalkScale = 0.16f;
    private readonly float backwardRunScale = 0.66f;

    private bool wasGrounded;
    private Vector3 currentDirection = Vector3.zero;
    private float currentV = 2f;
    private float currentH = 2f;

    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.25f;

    private bool isGrounded;
    private bool isLured;
    public bool atBusStop = false;
    private List<Collider> collisions = new List<Collider>();

    private float counter;
    private float deathTime = 3f;

    private bool hasBeenHit = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = StudentState.Idle;
        isGrounded = true;
        isLured = false;
        counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
        anim.SetBool("Grounded", true);
        print(state);
        if (currWaypoint == waypoints.Length)
        {
            currWaypoint = -1;
        }
        if (atBusStop && !agent.pathPending && agent.remainingDistance == 0 && state != StudentState.Idle)
        {
            state = StudentState.Idle;
        }
        print(state);
        switch (state)
        {
            case StudentState.Idle:
                if (anim.GetBool("Grounded") && !isLured)
                {
                    state = StudentState.Walk;
                } else
                {
                    anim.SetFloat("MoveSpeed", 0);
                }
                break;
            case StudentState.Walk:
                if (!agent.pathPending && agent.remainingDistance == 0)
                {
                    currentV = 2f;
                    currentH = 2f;
                    SetNextWaypoint();
                }
                break;
            case StudentState.Pizza:
                if (pizzaRigidBody != null)
                {
                    followPizza(pizzaRigidBody);
                } else
                {
                    state = StudentState.Walk;
                    agent.SetDestination(waypoints[currWaypoint].transform.position);
                }     
                break;
            case StudentState.Hit:
                // Play an oof audio
                anim.enabled = false;
                agent.isStopped = true;
                agent.ResetPath();
                counter += Time.deltaTime;
                if (counter >= deathTime)
                {
                    Destroy(this.gameObject);
                }
                // NEED TO REMOVE OBJECT AT SOME POINT I GUESS
                break;
            case StudentState.PickedUp:
                // IMPLEMENT LATER
                break;
            default:
                break;
        }
        
        wasGrounded = isGrounded;
    }

    private void SetNextWaypoint()
    {
        if (waypoints.Length != 0)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

            transform.position += transform.forward * currentV * moveSpeed * Time.deltaTime;
            transform.Rotate(0, currentH * turnSpeed * Time.deltaTime, 0);

            anim.SetFloat("MoveSpeed", currentV);
            //print(currentV);
            //JumpingAndLanding();
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
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.75f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!collisions.Contains(collision.collider))
            {
                collisions.Add(collision.collider);
            }
        }
        else
        {
            if (collisions.Contains(collision.collider))
            {
                collisions.Remove(collision.collider);
            }
            if (collisions.Count == 0)
            {
                isGrounded = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0) {
            isGrounded = false;
        }
    }

    public void StudentLured(bool lured, Rigidbody p)
    {
        if (lured && p != null)
        {
            isLured = true;
            state = StudentState.Pizza;
            pizzaRigidBody = p;
        }
        else
        {
            isLured = false;
            state = StudentState.Walk;
            agent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        
    }

    void followPizza(Rigidbody pizzaLure)
    {
        agent.SetDestination(pizzaLure.position);
        //print(agent.remainingDistance);
        if (agent.remainingDistance < 5)
        {
            agent.SetDestination(agent.transform.position);
            state = StudentState.Idle;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (atBusStop)
        {
            if (c.attachedRigidbody != null)
            {
                PeopleCollection busPickUp = c.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
                if (busPickUp != null)
                {
                    print("Picked Up");
                    Destroy(this.gameObject);
                    busPickUp.ReceivePickup();
                    state = StudentState.PickedUp;
                }
            }
        } else
        {
            if (c.attachedRigidbody != null)
            {
                PeopleCollection busPickUp = c.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
                if (busPickUp != null && hasBeenHit != true)
                {
                    busPickUp.HitStudent();
                    state = StudentState.Hit;
                    GetComponent<AudioSource>().Play();
                    hasBeenHit = true;
                }
            }
            
        }
    }

}
