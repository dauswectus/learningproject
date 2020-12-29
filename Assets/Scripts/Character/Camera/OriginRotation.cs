using UnityEngine;
using System.Collections;

public class OriginRotation : MonoBehaviour {
	/**
	 * Beállítja a "CameraOrigin" pozícióját.
	 * C-vel a kamera pozícióját változtatja.
	 */

	private Transform myTransform;
	public GameObject Player;
	[SerializeField]
	private Vector3[] onFootCameras = new Vector3[4];
	private float camera_x_pos;
	public GameObject CameraRotation;
	public GameObject CameraHead;
	public int cameraMode;
	public bool canChangeCamera;

	void ChangePosition(Vector3 CurrentPos){
		transform.localPosition = CurrentPos;
	}
	
	void Start () {
		myTransform = transform;
		onFootCameras[0] = new Vector3(0.5f,0,-10);
		onFootCameras[1] = new Vector3(0.5f,0,-5);
		onFootCameras[2] = new Vector3(0.5f,0,-2);
		onFootCameras[3] = new Vector3(0 ,0, -0.1f);
		myTransform.localPosition = onFootCameras[0];
		cameraMode = 1;
		canChangeCamera = true;
	}

	public void ChangeCamera(int modeChange)
	{
		cameraMode = modeChange;
		switch (cameraMode) {
		case 1:
			ChangePosition (onFootCameras[0]);
			break;
		case 2:
			ChangePosition (onFootCameras[1]);
			break;
		case 3:
			ChangePosition (onFootCameras[2]);
			break;
		case 4:
			ChangePosition (onFootCameras[3]);
			break;
		}
	}
	public void ChangeCamera()
	{
		if (cameraMode == onFootCameras.Length) {
			cameraMode = 1;
			if (GetComponentInParent<CharacterMotorExt> ().isInCar == false) {
				Player.GetComponent<MeshRenderer> ().enabled = true;
			}
			CameraHead.transform.localPosition = new Vector3(0.5f,0.6f,0);
			Player.GetComponentInParent<CharacterMotorExt> ().slerpSpeed = 0.03f;

		} 
		else {
			cameraMode++;
		}
		switch (cameraMode) {
		case 1:
			ChangePosition (onFootCameras[0]);
			break;
		case 2:
			ChangePosition (onFootCameras[1]);
			break;
		case 3:
			ChangePosition (onFootCameras[2]);
			break;
		case 4:
			ChangePosition (onFootCameras [3]);
			Player.GetComponent<MeshRenderer> ().enabled = false;
			CameraHead.transform.localPosition = new Vector3 (0, 0.6f, 0);
			Player.GetComponentInParent<CharacterMotorExt> ().slerpSpeed = 0.5f;

			break;
		}

	}

	void Update () {
		myTransform.rotation = CameraRotation.transform.rotation;

		if (GetComponentInParent<CharacterMotorExt> ().isInCar) {
			CameraHead.GetComponent<CameraHead> ().isCarCamera = true;
		} 
		else {
			CameraHead.GetComponent<CameraHead> ().isCarCamera = false;
		}
		if (Input.GetButtonDown ("ChangeCamera") && canChangeCamera) {
			ChangeCamera ();
		}
	}
}
