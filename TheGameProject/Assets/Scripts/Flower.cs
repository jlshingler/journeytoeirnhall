using UnityEngine;
using System.Collections;

[System.Serializable]
public class Flower : MonoBehaviour {
	public string fName;
	public string description;
	public int num;
	public Type type;
	
	public int craftingCount;
	public int toCraft;
}

public enum Type{
	fire, 
	ice,
	water,
	crystal,
	mushroom
}