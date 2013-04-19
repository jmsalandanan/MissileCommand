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
	private GameObject _explosion2;
	private GameObject _explosion3;
	private GameObject _explosion4;
	private GameObject _explosion5;
	public Vector3 randSpawnPoint;
	private int x;
	private int randSpawnTrigger;
	private int nextWaveCounter;
 	private List<GameObject> _units = new List<GameObject>();
	private List<GameObject> _explosions = new List<GameObject>();
	private float delay = 0;
	private float spawnDelay = 0;
 

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(_units!=null)
		EnemySignals.checkEnemies.dispatch();
		
		randSpawnTrigger = Random.Range (0,10);
		spawnDelay +=Time.deltaTime;
		if(randSpawnTrigger == 5&&x<5&&spawnDelay>2)
		{
			EnemySignals.enemyRelease.dispatch();
			spawnDelay = 0;
		}
		if(nextWaveCounter == 5)
		{
			delay +=Time.deltaTime;
			if(delay>1)
			{
				x = 0;
			nextWaveCounter = 0;
			_explosion.SetActive(false);
			_explosion2.SetActive(false);
			_explosion3.SetActive(false);
			_explosion4.SetActive(false);
			_explosion5.SetActive(false);	
				delay = 0;
			}		
		}		
	}
		
	public void init(){
		_bomb = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb2 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb3 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb4 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb5 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		
		_units.Add(_bomb);
		_units.Add(_bomb2);
		_units.Add(_bomb3);
		_units.Add(_bomb4);
		_units.Add(_bomb5);
		
		_explosion = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/BombExplosion"));
		_explosion2 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/BombExplosion"));
		_explosion3 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/BombExplosion"));
		_explosion4 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/BombExplosion"));
		_explosion5 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/BombExplosion"));

		_explosions.Add(_explosion);
		_explosions.Add(_explosion2);
		_explosions.Add(_explosion3);
		_explosions.Add(_explosion4);
		_explosions.Add(_explosion5);
		
		_explosion.SetActive(false);
		_explosion2.SetActive(false);
		_explosion3.SetActive(false);
		_explosion4.SetActive(false);
		_explosion5.SetActive(false);
		x=0; 
		nextWaveCounter = 0;
	}

	public void dropEnemy(Vector3 position){
	randSpawnPoint = new Vector3(Random.Range (-9.5f,8.0f),20,Random.Range (-6.5f,-2f));
	_units[x].transform.position = randSpawnPoint;
	_units[x].SetActive(true);
	x=x+1;
	}	
	
	public void spawnExplosion(Vector3 position){
		
			foreach(GameObject explosionIns in _explosions){
				if(!explosionIns.activeSelf){
				explosionIns.transform.position = position;
				explosionIns.SetActive(true);
				break;
				}
			}
	}
	
public void checkConditions()
{
	foreach(GameObject _bombIns in _units)
		{
		if(_bombIns!=null)
			{			
			if(_bombIns.transform.position.y<=2.0f&&_bombIns.activeSelf)
				{	
					Debug.Log ("Collision!");
					//_explosion = (GameObject) GameObject.Instantiate((Resources.Load("Prefabs/BombExplosion")),_bombIns.transform.position,Quaternion.Euler(_bombIns.transform.position));
					_bombIns.SetActive(false);
					spawnExplosion(_bombIns.transform.position);
					//Destroy (_explosion,0.5f);
					nextWaveCounter++;
				}
			if(_bombIns.activeSelf)
				{
					_bombIns.transform.Translate(0,-0.1f,0);
				}
			
			}
		}	 
}


}