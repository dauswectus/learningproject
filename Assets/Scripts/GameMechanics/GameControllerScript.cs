using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameControllerScript : MonoBehaviour {

	public float time;
	public TimeSpan currentTime;
	public Transform sunTransform;
	public Light Sun;
	public Text timeText;
	public int days;

	public float intensity;
	public Color fogday = Color.gray;
	public Color fognight = Color.black;

	public int speed;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		ChangeTime ();
	}
	public void ChangeTime(){
		time += Time.deltaTime * speed; 
		if (time > 86400) {
			days++;
			time = 0;
		}
		currentTime = TimeSpan.FromSeconds (time);
		string[] tempTime = currentTime.ToString ().Split (":" [0]);
		timeText.text = tempTime [0] + ":" + tempTime [1];
		sunTransform.rotation = Quaternion.Euler (new Vector3 ((time - 21600) / 56400 * 360, 0, 0));
		if (time < 43200) {
			intensity = 1 - (43200 - time) / 43200;

		}
		else {
			intensity = 1 - ((43200 - time) / 43200 * -1);
		}
		RenderSettings.fogColor = Color.Lerp (fognight, fogday, intensity * intensity);
		Sun.intensity = intensity;
	}
}
