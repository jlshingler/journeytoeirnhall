using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	bool respawn = false;
	public GUISkin skin;
	
	public GameObject game;
	void Start () {
		game = GameObject.Find("FPC");
	}
	//This fires off when the player enters hazards
	void OnTriggerEnter(Collider other)
	{
		//restart level
		if(other.name == "FPC"){
			respawn = true;
		}
	}
	
	public void OnGUI(){
		GUI.skin = skin;
		float textSize = (Screen.height * .075f) / 4;
		GUI.skin.box.fontSize = (int)textSize;
		if(respawn){ 
			game.SendMessage("setObstacleNotInView");
			Time.timeScale = 0.0f;
			GUI.Box(new Rect(Screen.width * .33f, Screen.height * .45f, Screen.width * .33f, Screen.height * .1f), "You have died! \nWatch out for Hazards! \nPress Return to Restart Level.");
			if(Input.GetKeyUp(KeyCode.Return)){
				respawn = false;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}
}

