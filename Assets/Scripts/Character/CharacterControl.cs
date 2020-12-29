using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	private Transform myTransform;

	public float verticalMovementSpeed;
	public float horizontalMovementSpeed;

	public float moveVertical;
	public float moveHorizontal;
	public float gravity;
	public float maxFallSpeed;
	public float verticalVelocity;
	public float jumpSpeed;

	public bool canMove;
	public bool canJump;
	public bool isGrounded;

	private RaycastHit groundRayHit;

	CharacterController characterController;

	public float myAng = 0.0f;
	public float slidingForce;
	public Vector3 normals;
	public bool isSliding;





	// Use this for initialization
	void Start () {

		slidingForce = 2f;
		characterController = GetComponent<CharacterController> ();
		myTransform = transform;
		verticalMovementSpeed = 10;
		horizontalMovementSpeed = 10;
		gravity = -10;
		maxFallSpeed = 20;
		jumpSpeed = 5;

		canMove = true;

	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		normals = new Vector3 (hit.normal.x, hit.normal.y, hit.normal.z);
		myAng = Vector3.Angle(Vector3.up, hit.normal);

		//Debug.Log (myAng);
	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector3 movement = new Vector3 (moveHorizontal, verticalVelocity, moveVertical);

		
		moveVertical = Input.GetAxis ("Vertical") * verticalMovementSpeed;
		moveHorizontal = Input.GetAxis ("Horizontal") * horizontalMovementSpeed;

		movement = myTransform.rotation * movement;

		if (characterController.isGrounded && !isSliding) {
			canMove = true;
			canJump = true;		
			verticalMovementSpeed = 10;
			horizontalMovementSpeed = 10;

			if (Input.GetButton ("Jump") && myAng < 45 && moveVertical >= 0) {
				Debug.Log ("Space");

				verticalVelocity = jumpSpeed;
			}

			else 
			{
				canJump = false;
				canMove = false;
			}
		}

		else
		{
			if(moveVertical != 0)
			{
				verticalMovementSpeed -= 10 * Time.deltaTime;
			}
			if ( moveHorizontal != 0 )
			{
				horizontalMovementSpeed -= 10 * Time.deltaTime;
			}
			verticalVelocity += gravity * Time.deltaTime;
		}

		characterController.Move(movement * Time.deltaTime);
	}
	void Update()
	{
		if (myAng >= characterController.slopeLimit) 
		{
			isSliding = true;
			canJump = false;
			canMove = false;
			
		}
		else
		{
			slidingForce = 2f;
			canMove = true;
			canJump = true;
			isSliding = false;
		}
		if (isSliding) 
		{
			slidingForce += 2 * Time.deltaTime;
			characterController.Move (normals * slidingForce * Time.deltaTime);
		}
	}
}






