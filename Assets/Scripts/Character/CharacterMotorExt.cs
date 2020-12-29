using UnityEngine;
using System.Collections;

public class CharacterMotorExt : MonoBehaviour {

	public int runSpeed;
	public bool isInCar;
	public bool exitCar;
	public GameObject carSeat;
	public GameObject activeCar;
	public Transform carExitTransform;
    private CharacterController controller;
	private CharacterMotor motor;
	private float counter;
	public float slerpSpeed;

	// Use this for initialization
	void Start () {
		motor = GetComponent<CharacterMotor> ();
		activeCar = null;
		exitCar = false;
		isInCar = false;
		slerpSpeed = 0.03f;
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body != null && !body.isKinematic)
			body.velocity += hit.controller.velocity / hit.collider.attachedRigidbody.mass;
		//Debug.Log ("Velocity: " + hit.controller.velocity / hit.collider.attachedRigidbody.mass );
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Sprint")|| Input.GetAxis ("SprintJoy") > 0)
		{
			motor.movement.maxForwardSpeed = runSpeed;
		}
		else
		{
			motor.movement.maxForwardSpeed = 6;
		}
		if (isInCar == true) {
			carExitTransform = activeCar.GetComponent<CarExitPositions> ().driverSeat;
			transform.position = carSeat.transform.position;
			if (Input.GetAxis ("Mouse Y") != 0 || Input.GetAxis ("Mouse X") != 0) {
				counter = 0 ;
			} 
			else
			{
				counter += Time.deltaTime;
			}
			if (counter >= 5 || Input.GetButtonDown("ResetCamera")) 
			{
				counter = 5;
				transform.rotation = Quaternion.Slerp (transform.rotation, activeCar.transform.rotation, slerpSpeed);
			}

		}
		else if(exitCar == true) 
		{
			transform.position = carExitTransform.position;
			exitCar = false;
		}
	}
    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject);
        if (hit.collider.GetComponent<Rigidbody>() != null)
        {
            Rigidbody body = hit.collider.GetComponent<Rigidbody>();

            if (body != null && !body.isKinematic)
            {
                body.velocity += hit.controller.velocity / body.mass;
            }
        }
    }*/
}
