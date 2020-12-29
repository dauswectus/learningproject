using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerGameControllerScript : MonoBehaviour {

	public GameObject carMirror;
	public GameObject Player;

	public GameObject carCamera;
	public bool isCarCameraOn;

	public GameObject inventory;
	public bool isInventoryOn;

	public GameObject sideCarCamera;
	public bool isSideCarCameraOn;

	private int count;

	// Use this for initialization

	void Start () {
		isCarCameraOn = false;
		isSideCarCameraOn = true;
		isInventoryOn = false;
		Cursor.visible = false;
		count = 0;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Insert)) {
			SceneManager.LoadScene("Test_Map");
		}
		if (Input.GetKeyDown (KeyCode.F4) && count == 0) {
			count++;

		}
		if (Input.GetKeyDown (KeyCode.F4) && count == 1) {
			Application.Quit();
		}
		if ((Input.GetButtonDown ("Tab") || Input.GetKeyDown (KeyCode.I)) && isInventoryOn) {
			Component[] temp = Player.GetComponentsInChildren<MouseLook>();
			foreach (MouseLook look in temp) {
				look.enabled = true;
			}
			Cursor.visible = false;
			isInventoryOn = false;
			inventory.SetActive (false);

		} 
		else if ((Input.GetButtonDown ("Tab") || Input.GetKeyDown (KeyCode.I)) && !isInventoryOn)
		{
			isInventoryOn = true;
			Component[] temp = Player.GetComponentsInChildren<MouseLook>();
			foreach (MouseLook look in temp) {
				look.enabled = false;
			}
			Cursor.visible = true;
			inventory.SetActive (true);
		}

	}
}
