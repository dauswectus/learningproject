using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public bool motor;
    public bool steering;
}

public class AltCarMove : MonoBehaviour
{
	private Rigidbody myRigidBody;

	[Header("Is the car active")]
	public bool active;

	[Header("Wheel Colliders")]
    public WheelCollider RearRight;
    public WheelCollider RearLeft;
	public WheelCollider FrontRight;
	public WheelCollider FrontLeft;

	[Header("Speed Variables")]
	public int currentspeed;
	public float altSpeed;
	private Vector3 velocity;
	private Vector3 localVelocity;
	public float[] wheelTorques = new float[4];

	[Header("Axle Infos")]
    public List<AxleInfo> axleInfos;

	[Header("Motor Properties")]
    public float maxMotorTorque;
	public float motorTorque;
	private float motor;

	[Header("Steering Properties")]
    public float maxSteeringAngle;
	public float minSteeringAngle;

	[Header("Breaking Properties")]
	public float frontBrake;
	public float rearBrake;
	public float handBrake;
	[Header("Lights")]
	public GameObject leftTailLight;
	public GameObject rightTailLight;

	[Header("Axis")]
	private float verticalAxis;

	[Header("ETC.")]
	public float speedDivision;

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);
		rotation *= Quaternion.Euler (0, 270, 90);

		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
    }
	void Start ()
	{
		myRigidBody = GetComponent<Rigidbody> ();
	}

	public void Brake()
    {
		RearRight.brakeTorque = rearBrake;
		RearLeft.brakeTorque = rearBrake;
		FrontRight.brakeTorque = frontBrake;
		FrontLeft.brakeTorque = frontBrake;
    }

    public void FixedUpdate()
    {
        if (active)
        {
			wheelTorques [0] = RearLeft.rpm;
			wheelTorques [1] = RearRight.rpm;
			wheelTorques [2] = FrontLeft.rpm;
			wheelTorques [3] = FrontRight.rpm;

			//verticalAxis = Input.GetAxis ("Vertical");
			if (Input.GetAxis ("Vertical") > 0 || Input.GetAxis ("Vertical") < 0) 
			{
				verticalAxis = Input.GetAxis ("Vertical");
			}
			else if (Input.GetAxis ("VerticalJoyCar") > 0 || Input.GetAxis ("VerticalJoyCar") < 0) 
			{
				verticalAxis = Input.GetAxis ("VerticalJoyCar");
			}
			else 
			{
				verticalAxis = 0;
			}

			float steering;
			float speedfactor = currentspeed / speedDivision;
			float steerangle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, speedfactor);

			currentspeed = ((int)(GetComponent<Rigidbody>().velocity.magnitude * 3.6f));
			velocity = myRigidBody.velocity;
			localVelocity = transform.InverseTransformDirection (velocity);
			altSpeed = (int)(localVelocity.z * 3.6f);

			motorTorque = Mathf.Lerp(maxMotorTorque,0,speedfactor/2);
			motor = motorTorque * verticalAxis;
				
            gameObject.layer = 9;

            steerangle *= Input.GetAxis("Horizontal");
            steering = steerangle;

			bool userBrakes = (altSpeed > 0 && verticalAxis < 0 || altSpeed < 0 && verticalAxis > 0);

			if (Input.GetButton ("Jump"))
			{
				RearRight.brakeTorque = handBrake;
				RearLeft.brakeTorque = handBrake;
			}
			if (userBrakes) {
				leftTailLight.GetComponent<Light> ().enabled = true;
				rightTailLight.GetComponent<Light> ().enabled = true;

				FrontRight.brakeTorque = frontBrake;
				FrontLeft.brakeTorque = frontBrake;
				RearRight.brakeTorque = rearBrake;
				RearLeft.brakeTorque = rearBrake;
				motor = 0;
			}
			if(!userBrakes && !Input.GetButton ("Jump"))
			{
				leftTailLight.GetComponent<Light> ().enabled = false;
				rightTailLight.GetComponent<Light> ().enabled = false;
				RearRight.brakeTorque = 0;
				RearLeft.brakeTorque = 0;
				FrontRight.brakeTorque = 0;
				FrontLeft.brakeTorque = 0;	
			}

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
						axleInfo.leftWheel.steerAngle = steering;
						axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
        else
        {
			foreach (AxleInfo axleInfo in axleInfos)
			{
				ApplyLocalPositionToVisuals(axleInfo.leftWheel);
				ApplyLocalPositionToVisuals(axleInfo.rightWheel);
			}
            gameObject.layer = 0;
			Brake();
        }
    }
}