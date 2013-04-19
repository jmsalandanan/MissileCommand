using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyView : MonoBehaviour {
	//public Rigidbody _enemy;
	private GameObject _bomb;
	private GameObject _bomb2;
	private GameObject _bomb3;
	private GameObject _bomb4;
	private GameObject _bomb5;
	private GameObject _explosion;
	public Vector3 randSpawnPoint;
	private int x;
	private int randSpawnTrigger;
	//private List<GameObject> enemyList;
 	public List<GameObject> units = new List<GameObject>();
 
	// Use this for initialization
	void Start () {
	//EnemySignals.enemyRelease.dispatch();
	}
	
	// Update is called once per frame
	void Update () {
		if(units!=null)
		EnemySignals.checkEnemies.dispatch();
	
		randSpawnTrigger = Random.Range (0,10);
	if(randSpawnTrigger == 5&&x<5)
		{
		EnemySignals.enemyRelease.dispatch();
		}
		
	}
	public void init(){
		
		// var index = attackerList.FindIndex (function (tr : Transform) tr.name == "TransformNameHere");
		_bomb = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb2 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb3 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb4 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb5 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
     	units.Add(_bomb);
		units.Add(_bomb2);
		units.Add(_bomb3);
		units.Add(_bomb4);
		units.Add(_bomb5);
		x=0;
	}
	
	void FixedUpdate(){

	}

	public void dropEnemy(Vector3 position){
	randSpawnPoint = new Vector3(Random.Range (-9.5f,8.0f),20,Random.Range (-6.5f,-2f));
	units[x].transform.position = randSpawnPoint;
	x=x+1;
	}	
	public void explosion()
	{
			Debug.Log (_bomb.transform.position);
	
	}
	
	public void checkConditions()
	{
	foreach(GameObject _bombIns in units)
		{
		if(_bombIns!=null)
		{
		  //dropEnemy();	
			Debug.Log(_bombIns.transform.position.y);
			Debug.Log (x);
		}
		
		if(_bombIns.transform.position.y<=2.0f)
		{	
			Debug.Log ("Collision!");
			//EnemySignals.explodeSignal.dispatch();
			_explosion = (GameObject) GameObject.Instantiate((Resources.Load("Prefabs/BombExplosion")),_bombIns.transform.position,Quaternion.Euler(_bombIns.transform.position));
			_bombIns.SetActive(false);
			Destroy (_explosion,0.5f);		
		}
			if(_bombIns.activeSelf)
			{
			_bombIns.transform.Translate(0,-0.1f,0);
			}
			
	}
	}

}
