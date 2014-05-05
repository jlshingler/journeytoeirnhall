using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {
	public string Name;
	public string Description;
	public int Num;
	public type Type;
	
	public int CraftingCount;
	public int toCraft;
}

public enum type{
	fire, 
	ice,
	water,
	crystal,
	mushroom,
	potion
}
