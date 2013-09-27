using UnityEngine;
using System.Collections;

public class IntroControllerScript : MonoBehaviour {
	
	/**
	 * Called on start
	 */
	void Start() {
		// Hide the cursor
		Screen.showCursor = false;
	}
	
	/**
	 * Called each frame
	 */
	void Update () {
		// Check whether the space bar is pressed, if so, continue to the next scene
		if(Input.GetKeyDown(KeyCode.Space))
			Application.LoadLevel(1);
	}
}
