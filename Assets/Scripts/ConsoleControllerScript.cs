using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class ConsoleControllerScript : MonoBehaviour {
	
	public int fontSize = 30;
	public Font font;
	public float cursorSpeed = 0.4f;
	public char cursorChar = '_';
	public int maxLines = 17;
	public string correctPass = "";
	public Transform textRendererObject;
	public Transform imageRendererObject;
	
	private float startTime = 0.0f;
	private string passInput = "";
	private bool passInputEnabled = false;
	private float passInputAt = -1.0f;
	private bool showImage = false;
	private float showImageAt = -1.0f;
	
	private List<string> lines = new List<string>();
	private List<List<object>> scheduledMessages = new List<List<object>>();
	
	private TextMesh textMesh;
	
	/**
	 * Called on start
	 */
	void Start() {
		// Get the text mesh component, to draw the text on
		textMesh = textRendererObject.GetComponent<TextMesh>();
		
		// Hide the image
		imageRendererObject.renderer.enabled = false;
		
		// Set the start time
		startTime = Time.time;
			
		// Schedule some messages
		scheduledMessages.Add(new List<object>(new object[]{1.0f, "BOOTING FROM FLOPPY..."}));
		scheduledMessages.Add(new List<object>(new object[]{2.5f, "INITIALIZING SYSTEM..."}));
		scheduledMessages.Add(new List<object>(new object[]{4.5f, "SYSTEM INITIALIZED!"}));
		scheduledMessages.Add(new List<object>(new object[]{5.0f, "STARTING MOHICANEN OS..."}));
		scheduledMessages.Add(new List<object>(new object[]{5.0f, "__________"}));
		scheduledMessages.Add(new List<object>(new object[]{5.2f, ""}));
		scheduledMessages.Add(new List<object>(new object[]{5.2f, "MOHICANEN OS v0.24"}));
		scheduledMessages.Add(new List<object>(new object[]{5.2f, "COPYRIGHT © 2013 MOHICANEN"}));
		scheduledMessages.Add(new List<object>(new object[]{5.8f, ""}));
		scheduledMessages.Add(new List<object>(new object[]{5.8f, "VERIFYING ACCOUNT CREDENTIALS..."}));
		scheduledMessages.Add(new List<object>(new object[]{7.0f, "ACCOUNT CREDENTIALS EXPIRED!"}));
		scheduledMessages.Add(new List<object>(new object[]{7.2f, ""}));
		scheduledMessages.Add(new List<object>(new object[]{7.2f, "ENTER SECURITY CODE:"}));
		
		// Set the pass input timer
		passInputAt = Time.time + 7.5f;
	}
	
	/**
	 * Called each frame
	 */
	void Update () {
		// Check whether a scheduled line should be shown
		for(int i = 0; i < scheduledMessages.Count; i++) {
			float time = (float) scheduledMessages[i][0];
			string msg = (string) scheduledMessages[i][1];
			
			if(startTime + time <= Time.time) {
				addLine(msg);
				scheduledMessages.RemoveAt(i);
				i--;
			}
		}
		
		// Check whether the pass input should be enabled
		if(passInputAt >= 0.0f && passInputAt <= Time.time) {
			// Disable the pass input timer
			passInputAt = -1.0f;
			
			// Enable the pass input
			passInputEnabled = true;
		}
		
		// Check whether the image should be shown
		if(showImageAt >= 0.0f && showImageAt <= Time.time) {
			// Disable the image timer
			showImageAt = -1.0f;
			
			// Show the image
			showImage = true;
			
			// Enable the image renderer and disable the text renderer// Hide the image
			imageRendererObject.renderer.enabled = true;
			textMesh.renderer.enabled = false;
		}
		
		// Get the user input
		if(passInputEnabled) {
				        foreach (char c in Input.inputString) {
				if(c == "\b"[0]) {
	                if (passInput.Length != 0)
	                    passInput = passInput.Substring(0, passInput.Length - 1);
				
				} else if(c == "\n"[0] || c == "\r"[0]) {
					// Check whether the pass was valid or not
					bool passValid = passInput.Trim().ToLower().Equals(correctPass.Trim().ToLower());
					
					// Verify the pass
					addLine("VERIFYING SECURITY CODE: " + passInput + "...");
					
					if(passValid) {
						// Hide the input field
						passInputEnabled = false;
						
						// Schedule some messages
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 1.5f, "SECURITY CODE IS VALID!"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 1.8f, ""}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 2.0f, "LOGGING IN TO SYSTEM..."}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 2.5f, "SUCCESFULLY LOGGED IN!"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 2.7f, ""}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 2.9f, "READING DATA... 0%"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 3.4f, "READING DATA... 25%"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 3.9f, "READING DATA... 50%"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 4.4f, "READING DATA... 75%"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 4.9f, "READING DATA... DONE"}));
						
						// Schedule the image timer
						showImageAt = Time.time + 5.3f;
						
						// Reset the pass field
						passInput = "";
						
					} else {
						// Hide the input field
						passInputEnabled = false;
						
						// Schedule some messages
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 1.2f, "INVALID SECURITY CODE!"}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 1.5f, ""}));
						scheduledMessages.Add(new List<object>(new object[]{Time.time + 1.8f, "ENTER SECURITY CODE:"}));
						
						// Schedule the pass input timer
						passInputAt = Time.time + 2.0f;
						
						// Reset the pass field
						passInput = "";
					}
					
					
				} else {
					passInput += c;
				}
	        }
		}
		
		// Check whether the image is being shown
		if(showImage) {
			// Check whether the ESC key is pressed, if its pressed twice in a second, the application will quit
			if(Input.GetKeyDown(KeyCode.Escape))
				Application.LoadLevel(0);
		}
	}
	
	/**
	 * Called on GUI update
	 */
	void OnGUI () {
		//Rect textSpace = new Rect(0 + textMargin, 0 + textMargin, Screen.width - textMargin, Screen.height - textMargin);
		
		string text = "";
		
		foreach(string line in lines)
			text += line + "\n";
		
		if(passInputEnabled == true) {
			text += "> " + passInput;
			
			// Show the cursor
			if(Time.time % (cursorSpeed * 2) <= cursorSpeed)
				text += cursorChar;
		}
		
		textMesh.text = text;
		
		//GUI.Label(textSpace, text, style);
	}
	
	void addLine(string line) {
		// Make sure there arent 17 or mores lines already, if so, remove them to match a count of 16 lines
		while(lines.Count >= 17)
			lines.RemoveAt(0);
		
		// Add the new line
		lines.Add(line);
	}
}
