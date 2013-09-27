using UnityEngine;
using System.Collections;

public class SceneControllerScript : MonoBehaviour {
	
	private float firstPress = 0.0f;
	
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
		// Check whether the ESC key is pressed, if its pressed twice in a second, the application will quit
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(firstPress <= 0.0f || firstPress < Time.time - 1.0f || firstPress > Time.time)
				firstPress = Time.time;
			
			else if(firstPress >= Time.time - 1.0f)
				Application.Quit();
		}
	}
}
