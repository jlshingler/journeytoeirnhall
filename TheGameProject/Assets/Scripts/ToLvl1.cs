using UnityEngine;
using System.Collections;

public class ToLvl1 : MonoBehaviour {
	bool transport = false;
	//public GameObject game;
	//public Toolbox inventory;
	// Use this for initialization
	void Start () {
	//game = GameObject.Find("FPC");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		transport = true;
		//inventory = game.inventory;
		Application.LoadLevel ("Lvl1");
		//Vector3 pos = new Vector3(20, 22, 1);
		//game.transform.position = pos;
		//game.inventory = inventory;
	}
	
	void OnGUI(){
		if (transport){
			GUI.Box(new Rect((Screen.width / 2) - 50, Screen.height / 2, 100, 30), "To Level 1");
		}
	}
}
