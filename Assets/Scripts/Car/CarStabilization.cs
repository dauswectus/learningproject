using UnityEngine;
using System.Collections;


public class CarStabilization : MonoBehaviour {
	public WheelCollider wheelL;
	public WheelCollider wheelR;
	public Rigidbody myRigidBody;
	public float antiRollVal = 5000f;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		WheelHit hit;
		float travelL=1.0f;
		float travelR=1.0f;
		bool groundedL = wheelL.GetGroundHit(out hit);
		if (groundedL){
			travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
		}
		bool groundedR = wheelR.GetGroundHit(out hit);
		if (groundedR){
			travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;
			
			
		}
		
		float antiRollForce = (travelL - travelR) * antiRollVal;
		
		
		
		if (groundedL)
			
			myRigidBody.AddForceAtPosition(wheelL.transform.up * -antiRollForce,
			                             
			                             wheelL.transform.position);  
		
		if (groundedR)
			
			myRigidBody.AddForceAtPosition(wheelR.transform.up * antiRollForce,
			                             
			                             wheelR.transform.position);  
	}
}