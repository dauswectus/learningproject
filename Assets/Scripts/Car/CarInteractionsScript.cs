using UnityEngine;
using System.Collections;

public class CarInteractionsScript : MonoBehaviour {

	public GameObject parent;
	private GameObject tmp;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && parent.GetComponent<ConfigurableJoint>() != null)
		{
			parent.GetComponent<ConfigurableJoint> ().xMotion = ConfigurableJointMotion.Free;
			parent.GetComponent<ConfigurableJoint> ().yMotion = ConfigurableJointMotion.Free;
			parent.GetComponent<ConfigurableJoint> ().zMotion = ConfigurableJointMotion.Free;
			parent.GetComponent<AltCarMove> ().active = false;
			Destroy (parent.GetComponent<ConfigurableJoint> ());
			tmp = null;
		}
		if (tmp == null)
		{
			parent.GetComponent<AltCarMove> ().active = false;
		} 
		else
		{
			parent.GetComponent<AltCarMove> ().active = true;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<Rigidbody>() != null && other.name == "TrailerHook" && parent.GetComponent<ConfigurableJoint> () == null)
		{
			parent.AddComponent<ConfigurableJoint> ();
			parent.GetComponent<ConfigurableJoint> ().connectedBody = other.GetComponentInParent<Rigidbody> ();
			parent.GetComponent<ConfigurableJoint> ().axis = new Vector3 (0, 1, 0);
			parent.GetComponent<ConfigurableJoint> ().anchor = new Vector3 (-0.47f, 0.0f, 3.04f);
			parent.GetComponent<ConfigurableJoint> ().autoConfigureConnectedAnchor = false;
			parent.GetComponent<ConfigurableJoint> ().connectedAnchor = new Vector3 (-0.017f, -0.62f, -3.64f);
			parent.GetComponent<ConfigurableJoint> ().enableCollision = true;
			//parent.GetComponent<ConfigurableJoint> ().breakForce = 100000;
			parent.GetComponent<ConfigurableJoint> ().xMotion = ConfigurableJointMotion.Locked;
			parent.GetComponent<ConfigurableJoint> ().yMotion = ConfigurableJointMotion.Locked;
			parent.GetComponent<ConfigurableJoint> ().zMotion = ConfigurableJointMotion.Locked;
			parent.GetComponent<AltCarMove> ().active = true;
			tmp = other.gameObject;
		}
	}
}
