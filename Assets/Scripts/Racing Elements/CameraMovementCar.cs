using UnityEngine;
using System.Collections;

public class CameraMovementCar : MonoBehaviour {

	public GameObject CameraOrigin;
	public GameObject Car;
	private Transform myTransform;
	[SerializeField]
	private float step;
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}

	// Update is called once per frame
	void Update () {
		/*float direction = Mathf.Sign (CameraOrigin.transform.position.x - myTransform.position.x);
		float direction2 = Mathf.Sign (CameraOrigin.transform.position.z - myTransform.position.z);
		myTransform.position = new Vector3 (CameraOrigin.transform.position.x + direction * step*Time.deltaTime, CameraOrigin.transform.position.y, CameraOrigin.transform.position.z + direction2 * step * Time.deltaTime);*/
		myTransform.position = Vector3.MoveTowards(myTransform.position, CameraOrigin.transform.position, step);
		myTransform.LookAt(Car.transform.position);
	}
}
