using UnityEngine;
using System.Collections;

public class LightSwitchScript : MonoBehaviour {

    public bool isItOn;
    public GameObject connectedItem;

	// Use this for initialization
	void Start () {
		isItOn = true;
	}

    public void UseItem()
    {
        if (isItOn)
        {
			if (gameObject.GetComponent<Light> () != null) {
				gameObject.GetComponent<Light> ().enabled = true;
			}
			if (connectedItem.GetComponent<TrailScript> () != null) {
				connectedItem.GetComponent<TrailScript> ().trailIsActive = true;
			}
            isItOn = false;
        }
        else
        {
			if (gameObject.GetComponent<Light> () != null) {
				gameObject.GetComponent<Light> ().enabled = false;
			}
			if (connectedItem.GetComponent<TrailScript> () != null) {
				connectedItem.GetComponent<TrailScript> ().trailIsActive = false;
			}
            isItOn = true;
        }
    }
}
