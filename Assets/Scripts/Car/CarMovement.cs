using UnityEngine;
using System.Collections;

/**
 * Idle mód: Amikor nem használják a kocsit akkor lefagy, wheel colliderek kikapcsolnak, és a kerekeknek cilinder colliderei lesznek.
 */

public class CarMovement : MonoBehaviour {

   public WheelCollider FrontLeft;
    public WheelCollider FrontRight;
    public WheelCollider RearRight;
    public WheelCollider RearLeft;

    public float speed = 300.0f;
	float acceleration;
    public float braking = 2.0f;
    public float turning = 20.0f;
	public int maxspeed = 150;
	public float rpm ;
	public float currentspeed;
	public bool active = false;




    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {

		if(FrontLeft.rpm < 0 && FrontLeft.rpm > 0)
		{
			currentspeed = 0;
		}
		else
		{
			currentspeed = (int)((2*Mathf.PI*RearLeft.radius)*RearLeft.rpm*60/1000);
			rpm = RearLeft.rpm;
		}

		if (active) {
			gameObject.layer = 9;
			if (currentspeed < maxspeed) {
				acceleration = Input.GetAxis ("Vertical") * speed;
			} else {
				acceleration = 0;
			}
				
			RearRight.motorTorque = acceleration * speed;
				RearLeft.motorTorque = acceleration ;

			//Kanyarodás
			FrontRight.steerAngle = Input.GetAxis ("Horizontal") * turning;
			FrontLeft.steerAngle = Input.GetAxis ("Horizontal") * turning;


			if (Input.GetKey (KeyCode.Space)) {
				RearRight.brakeTorque = braking*1000;
				RearLeft.brakeTorque = braking*1000;
			} else {
				/*RearRight.brakeTorque = 0;
				RearLeft.brakeTorque = 0;
				FrontRight.brakeTorque = 0;
				FrontLeft.brakeTorque = 0;*/
			}
		} else
		{
			gameObject.layer = 0;
		}
    }
}