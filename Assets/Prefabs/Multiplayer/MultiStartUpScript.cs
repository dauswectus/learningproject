using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MultiStartUpScript : NetworkBehaviour {

	public GameObject CameraCollider;
	public GameObject PlayerCamera;
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<MouseLook> ().enabled = true;
			GetComponent<FPSInputController> ().enabled = true;
			GetComponent<CharacterMotorExt> ().enabled = true;
			GetComponent<CharacterMotor> ().enabled = true;
			//GetComponent<PlayerStatsScript> ().enabled = true;
			GameObject PlayerCamera_ = (GameObject)Instantiate (PlayerCamera, CameraCollider.transform.position, CameraCollider.transform.rotation);
			PlayerCamera_.transform.parent = CameraCollider.transform;

		}
		gameObject.name = "Player";
		
	}
}
