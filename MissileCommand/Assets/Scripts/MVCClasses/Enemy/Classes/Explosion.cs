using UnityEngine;
using System.Collections;

public class Explosion{
private GameObject _explosion;
	
	public GameObject init()
	{
		_explosion = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/BombExplosion"));
		return _explosion;
	}
}
