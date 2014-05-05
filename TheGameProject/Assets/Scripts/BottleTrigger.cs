using UnityEngine;
using System.Collections;

public class BottleTrigger : MonoBehaviour {
	public GameObject game;
	void Start () {
		game = GameObject.Find("FPC");
	}
	
	void OnTriggerEnter(Collider other){
		if (other.name == "FPC"){
			game.SendMessage("setCanPickUpTrue", this.gameObject.name);
			game.SendMessage("setPotionObject", this.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other){
		game.SendMessage("setCanPickUpFalse");
	}
}
