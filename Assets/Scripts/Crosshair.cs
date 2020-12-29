using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	
	
	public Texture2D CrosshairTexture;
    public Texture2D InteractTexture;
		
	private Rect positionCrosshair;
    private Rect positionInteract;
    public bool OriginalOn = true;
	// Use this for initialization
	void Start () {
		positionCrosshair = new Rect((Screen.width - CrosshairTexture.width) / 2, ((Screen.height - CrosshairTexture.height / 3) /2), CrosshairTexture.width, CrosshairTexture.height);
        positionInteract = new Rect((Screen.width - InteractTexture.width) / 2, ((Screen.height - InteractTexture.height / 3) / 2), InteractTexture.width, InteractTexture.height);
        //Cursor.visible = false;
    }
	
    /**
        Egy raycast a fegyverbõl, hogy meddig megy a golyó, majd a másik a képernyõre vetít abból a pontból, majd a képernyõn elhelyezi a célkeresztet.
    */

	// Update is called once per frame
	void Update () {
			
		if(Input.GetButton("Aiming"))
		{
			//OriginalOn = true;
		}
		else
		{
			OriginalOn = false;
		}
	}
	void OnGUI() { 
		
		if(OriginalOn == true) 
		{ 
		
			GUI.DrawTexture(positionCrosshair, CrosshairTexture); 
		}
		else
		{
			GUI.DrawTexture(positionInteract, InteractTexture); 
		}
	}
}