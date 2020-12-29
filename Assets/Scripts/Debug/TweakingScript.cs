using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TweakingScript : MonoBehaviour {

	private bool isOpen;

	public GameObject car;
	public WheelCollider rearLeft;
	public WheelCollider rearRight;
	public WheelCollider frontLeft;
	public WheelCollider frontRight;

	public GameObject rearLeft_UI;
	public GameObject rearRight_UI;
	public GameObject frontLeft_UI;
	public GameObject frontRight_UI;

	public GameObject TweakUI;


	// Use this for initialization
	void Start () {
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F2) && !isOpen) {
			TweakUI.SetActive (true);
			Cursor.visible = true;
			isOpen = true;
		} 
		else if(Input.GetKeyDown (KeyCode.F2) && isOpen)
		{
			TweakUI.SetActive (false);
			Cursor.visible = false;
			isOpen = false;
		}
	}
	public void Run()
	{
		Component[] inputTexts;
		inputTexts = TweakUI.GetComponentsInChildren<InputField> ();
		foreach(InputField it in inputTexts)
		{
			Debug.Log (it.text);
		}

	}
}
