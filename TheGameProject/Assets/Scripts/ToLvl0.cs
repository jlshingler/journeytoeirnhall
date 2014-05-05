using UnityEngine;
using System.Collections;

public class ToLvl0 : MonoBehaviour {
	public GUISkin skin;
	bool showText = false;
	string description = "\n\nYou are on a journey to a new town to meet a contact. There will be obstacles along the way. Craft potions to overcome them! \n\n" +
	"Move using AWSD Keys. \nUse Mouse to Look Around. \n'P' Pauses Game. \n'I' Opens Inventory. \n'C' Opens Crafting Menu. \n It takes 3 Flowers to create a potion. \n" +
	"Hazards are present in the world. Watch out!\nPress 'Q' at any time to Quit.";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.skin = skin;
		float textSize = (Screen.height * .075f) / 4;
		GUI.skin.box.fontSize = (int)textSize;
		GUI.skin.label.fontSize = (int)textSize * 2;
		GUI.skin.button.fontSize = (int)textSize;
		
		GUI.Label(new Rect(Screen.width * .1f, Screen.height * .35f, 300, 50), "Journey to Eirnhall");
		if(GUI.Button(new Rect(Screen.width * .2f, Screen.height / 2, 150, 20), "Instructions")){
			showText = true;
		}
		if(GUI.Button(new Rect(Screen.width * .2f, Screen.height / 2 + 25, 150, 20), "Start Game")){
			Application.LoadLevel ("Lvl0");
		}
		/*if(PlayerPrefs.HasKey("Saved Game")){
			if(GUI.Button(new Rect(Screen.width * .2f, Screen.height / 2 + 50, 150, 20), "Load Game")){
				print("save game exists");
			}
		}
		if(GUI.Button(new Rect(Screen.width * .2f, Screen.height / 2 + 75, 150, 20), "Delete Saves")){
			PlayerPrefs.DeleteAll();
		}*/
		if(showText){ //show description of flower
			GUI.Box(new Rect(Screen.width * .565f, Screen.height * .3f, Screen.width * .33f, Screen.height * .4f), description);
			if(GUI.Button(new Rect(Screen.width * .676f, Screen.height * .625f, Screen.width * .11f, 30), "Close")){
				showText = false;
			}
		}
	}
}
