using UnityEngine;
using System.Collections;

public class InteriorCameraScript : MonoBehaviour {

	int cameraMode;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			cameraMode = other.GetComponentInChildren<OriginRotation> ().cameraMode;
			Debug.Log (other + "Entered in cameramode: "+cameraMode);
			if (cameraMode != 3 && cameraMode != 4) 
			{
				other.GetComponentInChildren<OriginRotation> ().ChangeCamera (3);
			}
			other.GetComponentInChildren<OriginRotation> ().canChangeCamera = false;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Log (other + "Exited");
			other.GetComponentInChildren<OriginRotation> ().ChangeCamera (cameraMode);
			other.GetComponentInChildren<OriginRotation> ().canChangeCamera = true;
		}
	}
}
