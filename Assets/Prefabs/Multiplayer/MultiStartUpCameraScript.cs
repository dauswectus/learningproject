using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MultiStartUpCameraScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<Camera> ().enabled = true;
		}
	}

}
