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
    private int currWaypoint;
    private float moveSpeed = 2;
    private float turnSpeed = 200;
    private float jumpForce = 4;

    public GameObject[] waypoints;
    private StudentState state;

    private readonly float interpolation = 10;
    private readonly float walkScale = 0.33f;
    private readonly float backwardsWalkScale = 0.16f;
    private readonly float backwardRunScale = 0.66f;

    private bool wasGrounded;
    private Vector3 currentDirection = Vector3.zero;
    private float currentV = 5f;
    private float currentH = 5f;

    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.25f;

    private bool isGrounded;
    private List<Collider> collisions = new List<Collider>();


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = StudentState.Idle;
        currWaypoint = -1;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
        anim.SetBool("Grounded", isGrounded);
        switch (state)
        {
            case StudentState.Idle:
                if (anim.GetBool("Grounded"))
                {
                    state = StudentState.Walk;
                }
                break;
            case StudentState.Walk:
                if (!agent.pathPending && agent.remainingDistance == 0)
                {
                    SetNextWaypoint();
                }
                break;
            case StudentState.Pizza:
                
                break;
            case StudentState.Hit:
                
                break;
            case StudentState.PickedUp:

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
            print(currentV);
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


}
