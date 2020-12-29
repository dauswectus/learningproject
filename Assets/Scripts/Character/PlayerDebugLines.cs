using UnityEngine;
using System.Collections;

public class PlayerDebugLines : MonoBehaviour {
	private Transform myTransform;
	// Use this for initialization
	void Start () {
	myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	Debug.DrawLine(myTransform.position, new Vector3(myTransform.position.x,myTransform.position.y+1,myTransform.position.z),Color.green);
	Debug.DrawLine(myTransform.position, new Vector3(myTransform.position.x+1,myTransform.position.y,myTransform.position.z),Color.red);
	Debug.DrawLine(myTransform.position, new Vector3(myTransform.position.x,myTransform.position.y,myTransform.position.z+1),Color.blue);
	}
}
