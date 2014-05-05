using UnityEngine;
using System.Collections;
using System.IO;

public class RecipeParser : MonoBehaviour {
	//private int counter = 0;
	//private string line;
	private string[] lines = new string[5];
	private string[] recipe = new string[5];
	private string[] returnPotion = new string[2];

	
	public void parseRecipes(){
		//counter = 0;
		//System.IO.StreamReader file = new System.IO.StreamReader(@"Assets\Resources\Recipes.csv");
		//while((line = file.ReadLine()) != null){
		//	lines[counter] = line;
		//	counter++;
		//}
		//file.Close();
		//lines = File.ReadAllLines(Application.dataPath + "/Resources/Recipes.csv");
		lines[0] = "Basic Fire Potion,Flame Reeds,Sunburst Daisies,Firebuds,This potion offers the most basic of explosive powers.";
		lines[1] = "Basic Frost Potion,Amethyst Blooms,Icebuds,Icebuds,This potion offers basic control over ice. Often used in freezing sections of water into solid ice. It is versatile because it can be crafted with several different recipes.";
		lines[2] = "Basic Frost Potion,Ice Mushrooms,Icebuds,Icebuds,This potion offers basic control over ice. Often used in freezing sections of water into solid ice. It is versatile because it can be crafted with several different recipes.";
		lines[3] = "Earth Potion,Mushrooms,Mushrooms,Crystal Buds,This is a basic earth potion often used in making natural bridges.";
		lines[4] = "Burning Potion,Firebuds,Firebuds,Firebuds,A gentle fire potion often used in burning away overgrowth.";

	}
	
	public string[] checkRecipes(string flowerA, string flowerB, string flowerC){
		parseRecipes();
		for(int i = 0; i < lines.Length; i++){
			recipe = lines[i].Split(',');
			//print(recipe[1] + " vs " + flowerA);
			//print(recipe[2] + " vs " + flowerB);
			//print(recipe[3] + " vs " + flowerC);
			if ((recipe[1] == flowerA && recipe[2] == flowerB && recipe[3] == flowerC) ||
				(recipe[3] == flowerA && recipe[1] == flowerB && recipe[2] == flowerC) ||
				(recipe[2] == flowerA && recipe[3] == flowerB && recipe[1] == flowerC) ||
				(recipe[1] == flowerA && recipe[3] == flowerB && recipe[2] == flowerC) ||
				(recipe[3] == flowerA && recipe[2] == flowerB && recipe[1] == flowerC) ||
				(recipe[2] == flowerA && recipe[1] == flowerB && recipe[3] == flowerC)){
				//print("success");
				returnPotion[0] = recipe[0];
				returnPotion[1] = recipe[4];
				return returnPotion;
				//print(p.pName + ": " + p.description);
			}
			else{
				//print("failure");
			}
		}
		//print("if success this will not print");
		return null;
	}
}
