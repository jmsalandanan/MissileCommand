using UnityEngine;
using System.Collections;

public class Bomb{
private GameObject _bomb;

	// Use this for initialization
	public GameObject init () {
		_bomb = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Enemy"));
		return _bomb;
	}
	
}