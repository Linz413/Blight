using UnityEngine;
using System;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;
    bool playingSound;
    public AudioSource audioSource;

    private WheelCollider[] m_Wheels;

    private float defaultMaxTorque;
    public float yOffsetCOM;
    

    public GameObject FrontWheelLeft;
    public GameObject FrontWheelRight;
    public GameObject BackWheelLeft;
    public GameObject BackWheelRight;
    private WheelCollider fwl;
    private WheelCollider fwr;
    private WheelCollider bwl;
    private WheelCollider bwr;
    public float boostAmount;
    public float boostTime = 10f;
    public float maxBoostTime;

    private WheelFrictionCurve defaultCurve;
    private WheelFrictionCurve driftingCurve;
    private WheelFrictionCurve driftingCurveFront;
    public float exs, exv, asy, asv, stif;
    public Boolean slideForwardWheels;
    private Rigidbody rb;
    public float velocity;
    private Vector3 prevPos;
    

    // Find all the WheelColliders down in the hierarchy.
	void Start()
	{
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = rb.centerOfMass - new Vector3(0, yOffsetCOM, 0);
		m_Wheels = GetComponentsInChildren<WheelCollider>();
        playingSound = false;

        defaultMaxTorque = maxTorque;
        for (int i = 0; i < m_Wheels.Length; ++i) 
		{
			var wheel = m_Wheels [i];

			// Create wheel shapes only when needed.
			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
        fwl = FrontWheelLeft.GetComponent<WheelCollider>();
        fwr = FrontWheelRight.GetComponent<WheelCollider>();
        bwl = BackWheelLeft.GetComponent<WheelCollider>();
        bwr = BackWheelRight.GetComponent<WheelCollider>();
        defaultCurve = bwl.sidewaysFriction;
        driftingCurve = new WheelFrictionCurve();
        driftingCurve.extremumSlip = exs;
        driftingCurve.extremumValue = exv;
        driftingCurve.asymptoteSlip = asy;
        driftingCurve.asymptoteValue = asv;
        driftingCurve.stiffness = stif;
        prevPos = this.transform.position;
        velocity = Vector3.Magnitude(this.transform.position - prevPos) / Time.deltaTime;
        maxBoostTime = boostTime;
	}

    //alters the max torque to give a speed boost to the car
    public void speedBoost(bool condition, float superSpeed)
    {
        if (condition)
        {
            maxTorque = superSpeed;
        }
        else
        {
            maxTorque = defaultMaxTorque;
        }
    }
    public void drift(bool condition, bool forwardWheelsToo)
    {
        if (condition)
        {
            if (forwardWheelsToo)
            {
                fwl.sidewaysFriction = driftingCurve;
                fwr.sidewaysFriction = driftingCurve;
            }
            bwl.sidewaysFriction = driftingCurve;
            bwr.sidewaysFriction = driftingCurve;
        } else
        {
            fwl.sidewaysFriction = defaultCurve;
            fwr.sidewaysFriction = defaultCurve;
            bwl.sidewaysFriction = defaultCurve;
            bwr.sidewaysFriction = defaultCurve;
        }
    }

    // This is a really simple approach to updating wheels.
    // We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
    // This helps us to figure our which wheels are front ones and which are rear.
    private void FixedUpdate()
    {
        //velocity
        velocity = Vector3.Magnitude(this.transform.position - prevPos) / Time.deltaTime;
        prevPos = this.transform.position;
    }
    void Update()
	{
        // adds speed boost
        if (Input.GetKey(KeyCode.LeftShift) && boostTime > 0f){
            speedBoost(true, boostAmount   );
            boostTime -= Time.deltaTime;
        } else
        {
            speedBoost(false, boostAmount);
        }
        
        drift(Input.GetKey(KeyCode.C),slideForwardWheels);

       

        m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);
        //Debug.Log(maxTorque);

		float angle = maxAngle * Input.GetAxis("Horizontal");
		float torque = maxTorque * Input.GetAxis("Vertical");

        if (playingSound && angle == 0 && torque == 0)
        {
            playingSound = false;
            audioSource.Pause();
        }
        else if (!playingSound && (angle != 0 || torque != 0))
        {
            playingSound = true;
            audioSource.Play();
        }
        Vector3 velocity = rb.velocity;
        Vector3 lvl = transform.InverseTransformDirection(velocity);
        float handBrake = Input.GetKey(KeyCode.S) && lvl.z > 0 ? brakeTorque : 0;
        if(Input.GetKey(KeyCode.S) && lvl.z > 0)
        {
            rb.AddForce(-rb.velocity * rb.mass / 8, ForceMode.Impulse);
        }

		foreach (WheelCollider wheel in m_Wheels)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			// Update visual wheels if any.
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// Assume that the only child of the wheelcollider is the wheel shape.
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}
		}
	}
}
