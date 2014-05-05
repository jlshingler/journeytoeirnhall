using UnityEngine;
using System.Collections;

public class ToLvl2 : MonoBehaviour {
	bool transport = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		transport = true;
		Application.LoadLevel ("Lvl2");
	}
	
	void OnGUI(){
		if (transport){
			GUI.Box(new Rect((Screen.width / 2) - 50, Screen.height / 2, 100, 30), "To Level 2");
		}
	}
}