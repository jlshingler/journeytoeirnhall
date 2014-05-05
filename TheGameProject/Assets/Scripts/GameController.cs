using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public List<Flower> flowers; 
	public List<Potion> potions;
	public List<Misc> misc;
	public GameObject inventoryList;
	public Toolbox inventory;
	
	public RecipeParser recipes;
	
	public bool open = false; // is inventory open
	public bool crafting = false; // is crafting window open
	public bool pause = false; // is game paused
	
	private int hudInt = 0;
	private string[] hudStrings = new string[]{"[1]\n\n\n", "[2]\n\n\n", "[3]\n\n\n", "[4]\n\n\n"};
	private string[] keys = new string[]{"1", "2", "3", "4"};
	
	int menuInt = 0;
	string[] menuStrings = new string[]{"Flowers", "Potions", "Misc"};
	string description = "";
	bool showText = false;
	
	public bool canPick = false;
	public Flower flower;
	float flowerTime = 1.0f;
	float flowerDelay = 0.5f;
	public AudioClip pick;
	
	public bool showSuccess = false;
	float time = 0.0f;
	float timeDelay = 1.5f;
	string item = "";
	
	public bool canPickUp = false;
	public string potion;
	public GameObject potionObject;
	
	public bool obstacleInView; // in range of obstacle
	public string obstacle; // get obstacle name
	GameObject obstacleObject; // get gameObject of obstacle to interact
	
	int craftingCap = 0;
	string[] craftList = new string[3];
	
	public GameObject flame; // preset particle for use in explosion
	
	public GUISkin skin;
	
	public GameObject bottle;
	
	// Use this for initialization
	void Start () {
		inventoryList = GameObject.Find("InventoryList");
		inventory = inventoryList.GetComponent<Toolbox>();
		//inventory = inventoryList.SendMessage("Instance");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.I)){ // check if player opens inventory
			open = !open;
			crafting = false;
			if(open){
				pause = true;
			}
			else{
				pause = false;
			}
		}
		if(Input.GetKeyUp(KeyCode.C)){  // check if player opens crafting
			crafting = !crafting;
			open = false;
			if(crafting){
				pause = true;
			}
			else{
				pause = false;
			}
		}
		if(Input.GetKeyUp(KeyCode.P)){ // check if player pauses screen
			pause = !pause;
			crafting = false;
			open = false;
		}
		
		if(Input.GetKeyUp(KeyCode.Q)){
			Application.Quit();
		}

		if (canPick && !crafting && !open && !pause && !canPickUp){ //pick flower
			flowerTime += Time.deltaTime;
			if (flowerTime > flowerDelay){
				if(Input.GetKeyUp(KeyCode.E)){
					audio.PlayOneShot(pick);
					inventory.addFlower(flower);
					time = 0.0f;
					flowerTime = 0.0f;
					item = flower.fName;
					showSuccess = true;
				}
			}
		}
		
		if(pause){
			Time.timeScale = 0.0f;
		}
		else{
			Time.timeScale = 1.0f;
		}
		
		if(!crafting && !open && !pause){ //cycle through HUD options
			for(int i = 0; i < 4; i++){
				string key = keys[i];
				if (Input.GetKeyUp(key)){ 
					hudInt = i;
				}
			}
		}
		
		if(!crafting && !open && !pause && obstacleInView){ //HUD
			potions = inventory.getPotions();
			for(int i = 0; i < potions.Count; i++){
				if (Input.GetKeyUp(KeyCode.Return) && hudInt == i){ 
					Vector3 bottlePos = new Vector3(transform.position.x + 1f, transform.position.y + 1.5f, transform.position.z + 1f);
					GameObject potionName = (GameObject)Instantiate(bottle, new Vector3(bottlePos.x, bottlePos.y, bottlePos.z), transform.rotation);
					potionName.name = potions[i].pName;
					if(potions[i].num > 0){ 
						potions[i].num -= 1;
					}
				}
			}
		}
		
		if(showSuccess){
			time += Time.deltaTime;
			if (time > timeDelay){
				showSuccess = false;
			}
		}
	}
	
	public void setObstacleInView(GameObject gO){
		obstacleInView = true;
		obstacle = gO.name;
		obstacleObject = gO;
	}
	
	public void setObstacleNotInView(){
		obstacleInView = false;
	}
	
	public void setCanPickTrue(Flower f){
		canPick = true;
		flower = f;
	}
	
	public void setCanPickFalse(){
		canPick = false;
	}
	
	public void setCanPickUpTrue(string p){
		canPickUp = true;
		potion = p;
	}
	
	public void setCanPickUpFalse(){
		canPickUp = false;
	}
	
	public void setPotionObject(GameObject p){
		potionObject = p;
	}
	
	public void OnGUI(){
		GUI.skin = skin;
		float textSize = (Screen.height * .075f) / 4;
		GUI.skin.box.fontSize = (int)textSize;
		GUI.skin.label.fontSize = (int)textSize;
		GUI.skin.button.fontSize = (int)textSize;
		if(pause && !crafting && !open){
			GUI.Box(new Rect(Screen.width * .4f, Screen.height / 2, Screen.width / 5f, 30), "Paused");
			/*if(GUI.Button(new Rect(Screen.width * .4286f, Screen.height * .55f, Screen.width / 7f, 30), "Save?")){
				print("save attempt");
				PlayerPrefs.SetInt("Saved Game", 1);
				PlayerPrefs.SetString("Level", Application.loadedLevelName);
				PlayerPrefs.SetFloat("PlayerPX", transform.position.x);
				PlayerPrefs.SetFloat("PlayerPY", transform.position.y);
				PlayerPrefs.SetFloat("PlayerPZ", transform.position.z);
				PlayerPrefs.SetFloat("PlayerRX", transform.rotation.x);
				PlayerPrefs.SetFloat("PlayerRY", transform.rotation.y);
				PlayerPrefs.SetFloat("PlayerRZ", transform.rotation.z);
				flowers = inventory.getFlowers();
				for(int i = 0; i < flowers.Count; i++){
						string key = "Flower" + i.ToString();
						string keyNum = key + "Num";
						string keyDesc = key + "Desc";
						PlayerPrefs.SetString(key, flowers[i].fName);
						PlayerPrefs.SetInt(keyNum, flowers[i].num);
						PlayerPrefs.SetString(keyDesc, flowers[i].description);
				}				
				potions = inventory.getPotions();
				for(int i = 0; i < potions.Count; i++){
						string key = "Potion" + i.ToString();
						string keyNum = key + "Num";
						string keyDesc = key + "Desc";
						PlayerPrefs.SetString(key, potions[i].pName);
						PlayerPrefs.SetInt(keyNum, potions[i].num);
						PlayerPrefs.SetString(keyDesc, potions[i].description);
				}
				misc = inventory.getMisc();
				for(int i = 0; i < misc.Count; i++){
						string key = "Potion" + i.ToString();
						string keyDesc = key + "Desc";
						PlayerPrefs.SetString(key, misc[i].mName);
						PlayerPrefs.SetString(keyDesc, misc[i].description);
				}
				PlayerPrefs.Save();
			}*/
		}
		
		if (canPick && !crafting && !open && !pause && !canPickUp){ //display option to pick flower
			GUI.Box(new Rect(Screen.width / 2 - 90, Screen.height / 2, 200, 50), "Press E to Pick " + flower.fName);
		}
		
		if (showSuccess){
			GUI.Label(new Rect(5, 5, 200, 50), "Picked Up " + item);
		}
		
		if (canPickUp && !crafting && !open && !pause){ //display option to pick up dropped potion
			GUI.Box(new Rect(Screen.width / 2 - 90, Screen.height / 2, 200, 50), "Press E to Pick Up " + potion);
			if(Input.GetKeyUp(KeyCode.E)){
				audio.PlayOneShot(pick);
				inventory.addPotion(potion);
				time = 0.0f;
				item = potion;
				showSuccess = true;				
				Destroy(potionObject.gameObject);
				canPickUp = false;
			}
		}
		
		if(!crafting && !open && !pause){ //HUD
			Screen.showCursor = false;
			Screen.lockCursor = true;
			GUI.Box(new Rect(Screen.width * .3f, Screen.height * .85f, Screen.width * .4f, Screen.height * .125f), "");
			potions = inventory.getPotions();
			hudInt = GUI.Toolbar(new Rect(Screen.width * .31f, Screen.height * .865f, Screen.width * .38f, Screen.height * .1f), hudInt, hudStrings);
			for(int i = 0; i < 4; i++){
				if (i < potions.Count){ 
					hudStrings[i] = "[" + (i + 1).ToString() + "]\n" + potions[i].pName + "\nx" + potions[i].num;
					//GUI.Box(new Rect(Screen.width * .3f + ((Screen.height * .1f) * space), Screen.height * .865f, Screen.height * .1f, Screen.height * .1f), text);
				}
			}
		}
		else{
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		
		if (obstacleInView && !crafting && !open && !pause && !canPick && !canPickUp){ //display option to use potion
			GUI.Box(new Rect(Screen.width / 2 - 90, Screen.height / 2, 200, Screen.height / 12), "Obstacle is " + obstacle + ". \n Select Return to use Active Potion");
		}
		
		if (open){ //inventory is open
			//GUI.skin.box.wordWrap = true;
			GUI.Box(new Rect(Screen.width * .055f, Screen.height * .125f, Screen.width * .44f, Screen.height * .75f), "Inventory");
			
			menuInt = GUI.Toolbar(new Rect(Screen.width * .11f, Screen.height * .175f, Screen.width * .33f, 30), menuInt, menuStrings);
			
			if (menuInt == 0){ //flower menu
				flowers = inventory.getFlowers();
				int space = 0;
				for(int i = 0; i < flowers.Count; i++){
					if (flowers[i].num > 0){ //only list items user carries
						if(GUI.Button(new Rect(Screen.width * .166f, Screen.height * .25f + (space * 25), Screen.width * .222f, 20), flowers[i].fName + " x" + flowers[i].num)){
							//print(flowers[i].description);
							//print(flowers[i].num);
							
							description = "Name: " + flowers[i].fName + "\nType: " + flowers[i].type.ToString() + "\nDescription:" + flowers[i].description;
							showText = true;
						}
						space++;
					}
				}
			}
			else if (menuInt == 1){ //potion menu
				potions = inventory.getPotions();
				int space = 0;
				for(int i = 0; i < potions.Count; i++){
					if (potions[i].num > 0){ //only list items user carries
						if(GUI.Button(new Rect(Screen.width * .166f, Screen.height * .25f + (space * 25), Screen.width * .222f, 20), potions[i].pName + " x" + potions[i].num)){
							//print(potions[i].description);
							//print(potions[i].num);
							
							description = "Name: " + potions[i].pName + "\nDescription: " + potions[i].description;
							showText = true;
							//currentPotion = potions[i].pName;
						}
						space++;
					}
				}
			}
			else{ //misc menu
				misc = inventory.getMisc();
				int space = 0;
				for(int i = 0; i < misc.Count; i++){
					if(GUI.Button(new Rect(Screen.width * .166f, Screen.height * .25f + (space * 25), Screen.width * .222f, 20), misc[i].mName)){
						//print(misc[i].description);
						
						description = misc[i].description;
						showText = true;
					}
					space++;
				}				
			}
			
			if(showText){ //show description of flower
				GUI.Box(new Rect(Screen.width * .5833f, Screen.height * .288f, Screen.width * .33f, Screen.height * .35f), description);
				if(GUI.Button(new Rect(Screen.width * .6944f, Screen.height * .338f + Screen.height * .234f, Screen.width * .11f, 30), "Close")){
					showText = false;
				}
			}
		
		}
		
		if(crafting){
			GUI.Box(new Rect(Screen.width * .055f, Screen.height * .125f, Screen.width * .44f, Screen.height * .75f), "Available Ingredients");
			GUI.Box(new Rect(Screen.width * .505f, Screen.height * .125f, Screen.width * .44f, Screen.height * .75f), "Crafting");
			
			flowers = inventory.getFlowers();
				int space = 0;
				for(int i = 0; i < flowers.Count; i++){ //list flowers for crafting
					if (flowers[i].num > 0){ //only list items user carries
						if(GUI.Button(new Rect(Screen.width * .166f, Screen.height * .25f + (space * 25), Screen.width * .222f, 20), flowers[i].fName + " x" + flowers[i].craftingCount)){
							showText = false;
							if(craftingCap < 3 && flowers[i].craftingCount > 0){
							audio.PlayOneShot(pick);
							flowers[i].toCraft++;
							flowers[i].craftingCount--;
							craftList[craftingCap] = flowers[i].fName;
							craftingCap++;
							}
						}
						space++;
					}
				}
				space = 0;
				for(int i = 0; i < flowers.Count; i++){ //list items chosen to be crafted
					if (flowers[i].toCraft > 0){ 
						if(GUI.Button(new Rect(Screen.width * .616f, Screen.height * .25f + (space * 25), Screen.width * .222f, 20), flowers[i].fName + " x" + flowers[i].toCraft)){}
						space++;
					}
				}
				
				if(GUI.Button(new Rect(Screen.width * .616f, Screen.height * .75f, Screen.width * .222f, 20), "Clear")){ //clear selections
					for(int i = 0; i < flowers.Count; i++){
						flowers[i].toCraft = 0;
						flowers[i].craftingCount = flowers[i].num;
						craftingCap = 0;
					}
				}
				
				if(craftingCap == 3){
					if(GUI.Button(new Rect(Screen.width * .616f, Screen.height * .75f - 25, Screen.width * .222f, 20), "Craft")){
						audio.PlayOneShot(pick);
						//recipes.parseRecipes();
						string[] craftAttempt = recipes.checkRecipes(craftList[0], craftList[1], craftList[2]);
						if(craftAttempt != null){
							//print("succeeded crafting");
							if (inventory.addPotion(craftAttempt[0], craftAttempt[1], craftList[0], craftList[1], craftList[2])){
								description = "Success! Created " + craftAttempt[0] + "!";
								showText = true;
							}
							else {
								description = "You already have the maximum number of that potion in your inventory!";
								showText = true;
							}
						}
						else{
							//print("failed to craft");
							description = "Failed Attempt. Try Again!";
							showText = true;
						}
						for(int i = 0; i < flowers.Count; i++){
							flowers[i].toCraft = 0;
							flowers[i].num = flowers[i].craftingCount;
							craftingCap = 0;
						}
					}
				}
			if(showText){ //show description of flower
				GUI.Box(new Rect(Screen.width * .565f, Screen.height * .44f, Screen.width * .33f, Screen.height * .166f), description);
				if(GUI.Button(new Rect(Screen.width * .676f, Screen.height * .544f, Screen.width * .11f, 30), "Close")){
					showText = false;
				}
			}
		}
	}
	
}
