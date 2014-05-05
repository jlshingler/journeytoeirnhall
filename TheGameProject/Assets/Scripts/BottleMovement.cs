using UnityEngine;
using System.Collections;

public class BottleMovement : MonoBehaviour {
	float speed = 10f;
	bool moving = true;
	
	public GameObject flame;
	public GameObject game;
	public GameObject water;
	public GameObject toDelete;
	public GameObject iceDeer;
	public GameObject rockDeer;
	
	public AudioClip explode;
	public AudioClip freeze;
	public AudioClip rock;
	
	bool destroy = false;
	float time = 0.0f;
	// Use this for initialization
	void Start () {
		game = GameObject.Find("FPC");
		water = GameObject.Find("Water Over Ice");
	}
	
	// Update is called once per frame
	void Update () {
		if(moving){
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		}
		//print(this.name);
		
		if(destroy){
			//this.gameObject.renderer.enabled = false;
			time += Time.deltaTime;
			//print(time);
			if (time > 1.0f){
				Destroy(this.gameObject);
			}
		}
	}
	
	void OnCollisionEnter(Collision other){
		//print("hit something");
		if(other.gameObject.name != "FPC"){
			moving = false;
			if(other.gameObject.tag == "obstacle"){
				destroy = true;
			}
			if(other.gameObject.name == "Rock"){
				if (this.name == "Basic Fire Potion"){
					audio.PlayOneShot(explode);
					Vector3 flamePos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					Instantiate(flame, new Vector3(flamePos.x, flamePos.y, flamePos.z), Quaternion.Euler(-90,0,0));
					Destroy(other.gameObject.gameObject);
					//obstacleInView = false;
					game.SendMessage("setObstacleNotInView");
				}
			}
			if(other.gameObject.name == "Water"){
				if (this.name == "Basic Frost Potion"){
					audio.PlayOneShot(freeze);
					Vector3 icePos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 1f, other.gameObject.transform.position.z);
					other.gameObject.transform.position = icePos;
					other.gameObject.renderer.enabled=true;
					water.renderer.enabled=false;
					//obstacleInView = false;
					game.SendMessage("setObstacleNotInView");
				}
			}
			if(other.gameObject.name == "Spike Pit Terrain"){
				if (this.name == "Earth Potion"){
					audio.PlayOneShot(rock);
					Vector3 rockPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 8f, other.gameObject.transform.position.z);
					other.gameObject.transform.position = rockPos;
					GameObject rocks = GameObject.Find("Spike Pit");
					Destroy(rocks.gameObject);
					//obstacleInView = false;
					game.SendMessage("setObstacleNotInView");
				}
			}
			if(other.gameObject.name == "Giant Deer Collider"){
				if (this.name == "Basic Fire Potion"){
					audio.PlayOneShot(explode);
					Vector3 flamePos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					Instantiate(flame, new Vector3(flamePos.x, flamePos.y, flamePos.z), Quaternion.Euler(-90,0,0));
					Destroy(other.gameObject.gameObject);
					toDelete = GameObject.Find("To Delete");
					Destroy(toDelete.gameObject);
					//obstacleInView = false;
					game.SendMessage("setObstacleNotInView");
				}
				else if (this.name == "Basic Frost Potion"){
					audio.PlayOneShot(freeze);
					Destroy(other.gameObject.gameObject);
					Vector3 deerPos = other.gameObject.transform.position;
					Instantiate(iceDeer, new Vector3(deerPos.x, deerPos.y, deerPos.z), Quaternion.Euler(0,270,270));
					toDelete = GameObject.Find("To Delete");
					Destroy(toDelete.gameObject);
					//obstacleInView = false;
					game.SendMessage("setObstacleNotInView");
				}
				else if (this.name == "Earth Potion"){
					audio.PlayOneShot(rock);
					Destroy(other.gameObject.gameObject);
					Vector3 deerPos = other.gameObject.transform.position;
					Instantiate(rockDeer, new Vector3(deerPos.x, deerPos.y, deerPos.z), Quaternion.Euler(0,270,270));
					toDelete = GameObject.Find("To Delete");
					Destroy(toDelete.gameObject);
					//obstacleInView = false;
					game.SendMessage("setObstacleNotInView");
				}
			}
		}
	}
}
