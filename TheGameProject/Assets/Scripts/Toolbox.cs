using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Toolbox : MonoBehaviour {
	public static List<Flower> flowers;
	public static List<Potion> potions;
	public static List<Misc> misc;
	
	void Awake () {
		initialize();
		DontDestroyOnLoad(gameObject);
	}
	private static Toolbox _instance = null;
	public static Toolbox Instance{
		get{ 
			if (_instance == null){
				_instance = new Toolbox();
				//DontDestroyOnLoad(this);
			}
			return _instance; 
		}
	
	}
	
	public Toolbox returnInstance(){
		return _instance;
	}
	
	protected Toolbox(){
		initialize();
	}
	
	protected void initialize() {
		//items = this.AddComponent("List<Item>") as List<Item>;
		flowers = new List<Flower>();
		potions = new List<Potion>();
		misc = new List<Misc>();
		Misc instructions = new Misc();
		instructions.mName = "Instructions";
		instructions.description = "You are on a journey to a new town to meet a contact. There will be obstacles along the way. Craft potions to overcome them! \n\n" +
	"Move using AWSD Keys. \nUse Mouse to Look Around. \n'P' Pauses Game. \n'I' Opens Inventory. \n'C' Opens Crafting Menu. \n It takes 3 Flowers to create a potion. \n" +
	"Hazards are present in the world. Watch out!\nPress 'Q' at any time to Quit.";
		misc.Add(instructions);
		Misc recipeBook = new Misc();
		recipeBook.mName = "Recipes";
		recipeBook.description = "Your recipes will be stored here when you complete them for the first time.";
		misc.Add(recipeBook);
		//print("test");
	}
	
	
	
	public List<Flower> getFlowers(){
		return flowers;
	}
	
	public void addFlower(Flower f){
		int check = -1;
		int count = 0;
		for(int i = 0; i < flowers.Count; i++){ //check for flower already in list
			if(flowers[i].fName == f.fName){
				check = i;
				count = flowers[i].num;
			}
		}
		if (check > -1){ //if flower already in list, increment
			if (count < 5){ //max count of flowers
				flowers[check].num += 1;
				flowers[check].craftingCount += 1;
			}
		}
		else{ //otherwise add new flower
			Flower flower = new Flower();
			flower.fName = f.fName;
			flower.description = f.description;
			flower.num = 1;
			flower.craftingCount = 1;
			flowers.Add(flower);
		}
	}
	
	public void remFlower(Flower f){
		if(flowers.Find(Flower => Flower.fName == f.fName) != null){ //see if flower exists in list already
			flowers[flowers.FindIndex(Flower => Flower.fName == f.fName)].num -= 1; //change 1 to f.num at some point plz self
		}
	}
	
	public List<Potion> getPotions(){
		return potions;
	}
	
	public bool addPotion(string name, string desc, string a, string b, string c){
		//print("adding potion " + name);
		int check = -1;
		int count = 0;
		for(int i = 0; i < potions.Count; i++){ //check for potion already in list
			if(potions[i].pName == name){
				check = i;
				count = potions[i].num;
			}
		}
		if (check > -1){ //if potion already in list, increment
			if (count < 3){
				potions[check].num += 1;
				return true;
			}
			else{
				return false;
			}
		}
		else{ //otherwise add new potion
			misc[1].description = misc[1].description + "\n" + name + ": " + a + ", " + b + ", " + c;
			Potion potion = new Potion();
			potion.pName = name;
			potion.description = desc;
			potion.num = 1;
			potions.Add(potion);
			return true;
		}
	}
	
	public void addPotion(string name){
		int check = -1;
		for(int i = 0; i < potions.Count; i++){ //check for potion already in list
			if(potions[i].pName == name){
				check = i;
			}
		}
		if (check > -1){ //if potion already in list, increment
			potions[check].num += 1;
		}
	}
	
	public void remPotion(Potion p){
		if(potions.Find(Potion => Potion.pName == p.pName) != null){ //see if potion exists in list already
			potions[potions.FindIndex(Potion => Potion.pName == p.pName)].num -= 1; //change 1 to p.num at some point plz self
		}
	}
	
	public List<Misc> getMisc(){
		return misc;
	}
	
	public void addMisc(Misc m){
		//print("adding misc " + m.mName);
		misc.Add(m);
	}
}
