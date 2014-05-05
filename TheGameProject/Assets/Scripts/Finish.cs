using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	bool transport = false;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		transport = true;
	}
	
	void OnTriggerExit(Collider other){
		transport = false;
		Application.Quit();
	}
	
	void OnGUI(){
		if (transport){
			GUI.Box(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 30), "Good job! You finished the game!");
		}
	}
}
