using UnityEngine;
using System.Collections;

public class CameraHead : MonoBehaviour {

	private Transform myTransform; 
	public bool isCarCamera = false;
	private Vector3 myLocalPosition;

	void Start () {
		myTransform = transform;
		myTransform.localPosition = new Vector3(0.5f,0.6f,0);
		myLocalPosition = myTransform.localPosition;
	}
	

	void Update () {
		/*if(!isCarCamera)
		{
			myTransform.localPosition = Vector3.MoveTowards (myTransform.localPosition,myLocalPosition,3.0f);
			//myTransform.localPosition = new Vector3(0.5f,0.6f,0.0f);
		}
		else
		{
			myTransform.localPosition = new Vector3(0.0f,0.6f,0.0f);
		}*/
		/*if (Input.GetButtonUp ("Tab")) { //Váltja hogy az ember melyik oldalán legyen a kamera.
			if (right == true){
				myTransform.localPosition = new Vector3(-0.5f,0.6f,0);
				right = false;
			}
			else{
				myTransform.localPosition = new Vector3(0.5f,0.6f,0);
				right = true;
			}
		}*/
	}
}
