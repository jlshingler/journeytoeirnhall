using UnityEngine;
using System.Collections;

public class FlowerTrigger : MonoBehaviour {
	public Flower flower;
	public GameObject game;
	void Start () {
		game = GameObject.Find("FPC");
	}
	
	void OnTriggerEnter(Collider other){
		if (other.name == "FPC"){
			game.SendMessage("setCanPickTrue", flower);
		}
	}
	
	void OnTriggerExit(Collider other){
		game.SendMessage("setCanPickFalse");
	}
}
