using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftElbow;
    public GameObject rightElbow;
    public GameObject hip;
    public float angleLimit;
    public float movementSpeed;
    public float hipscaling;
    public float msscaling;

    private float leftArmAngle = 0;
    private float rightArmAngle = 0;
    private float hipAngle = 0;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("a"))
        //{
        //    anim.Play("Left Arm");
        //}
        //if (Input.GetKeyDown("d"))
        //{
        //    anim.Play("Right Arm");
        //}

    }
    private void FixedUpdate()
    {
        //leftArm.transform.Rotate(new Vector3(0, 0, decayConstant));
        //if (Input.GetKey("a"))
        //{
        //    if (currentLeft < angleLimit) {
        //        leftArm.transform.Rotate(new Vector3(0, 0, -decayConstant));
        //        currentLeft += decayConstant;
        //    }
        //}
        //https://answers.unity.com/questions/341962/smooth-transition-for-getaxis-when-pressing-opposi.html disable this for this to work well
        //Debug.Log(Input.GetAxis("Horizontal"));
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            if (leftArmAngle < angleLimit)
            {
                leftArm.transform.Rotate(new Vector3(0, 0, -movementSpeed));
                leftElbow.transform.Rotate(new Vector3(0, 0, -movementSpeed));
                leftArmAngle += movementSpeed;

            }
            if (rightArmAngle > 0)
            {
                rightArm.transform.Rotate(new Vector3(0, 0, -movementSpeed));
                rightElbow.transform.Rotate(new Vector3(0, 0, -movementSpeed));
                rightArmAngle -= movementSpeed;
            }
            if (hipAngle > -angleLimit * hipscaling)
            {
                hip.transform.Rotate(new Vector3(0, -movementSpeed *msscaling, 0));
                hipAngle -= movementSpeed * msscaling;
            }
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            if (rightArmAngle < angleLimit)
            {
                rightArm.transform.Rotate(new Vector3(0, 0, movementSpeed));
                rightElbow.transform.Rotate(new Vector3(0, 0, movementSpeed));
                rightArmAngle += movementSpeed;
            }
            if (leftArmAngle > 0)
            {
                leftArm.transform.Rotate(new Vector3(0, 0, movementSpeed));
                leftElbow.transform.Rotate(new Vector3(0, 0, movementSpeed));
                leftArmAngle -= movementSpeed;

            }
            if (hipAngle < angleLimit * hipscaling)
            {
                hip.transform.Rotate(new Vector3(0, movementSpeed *msscaling, 0));
                hipAngle += movementSpeed *msscaling;
            }
        } else {
            if (leftArmAngle > 0)
            {
                leftArm.transform.Rotate(new Vector3(0, 0, movementSpeed));
                leftElbow.transform.Rotate(new Vector3(0, 0, movementSpeed));
                leftArmAngle -= movementSpeed;

            }else if (rightArmAngle > 0)
            {
                rightArm.transform.Rotate(new Vector3(0, 0, -movementSpeed));
                rightElbow.transform.Rotate(new Vector3(0, 0, -movementSpeed));
                rightArmAngle -= movementSpeed;
            }
            if (hipAngle < 0)
            {
                hip.transform.Rotate(new Vector3(0, movementSpeed * msscaling, 0));
                hipAngle += movementSpeed *msscaling;
            } else if (hipAngle > 0)
            {
                hip.transform.Rotate(new Vector3(0, -movementSpeed *msscaling, 0));
                hipAngle -= movementSpeed *msscaling;
            }

        }
    }
}
