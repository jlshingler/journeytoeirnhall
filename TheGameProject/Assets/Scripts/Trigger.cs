using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	public GameObject self;
	public GameObject game;
	void Start () {
		game = GameObject.Find("FPC");
	}
	
	void OnTriggerEnter(Collider other){
		if (other.name == "FPC"){
			game.SendMessage("setObstacleInView", self);
		}
	}
	
	void OnTriggerExit(Collider other){
		game.SendMessage("setObstacleNotInView");
	}
}
