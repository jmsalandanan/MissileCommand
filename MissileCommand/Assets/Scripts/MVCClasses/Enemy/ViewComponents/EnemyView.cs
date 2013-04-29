using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyView : MonoBehaviour {
	//public Rigidbody _enemy;
	
	public Vector3 randSpawnPoint;
	private RaycastHit _enemyLine;
	private int x;
	private int randSpawnTrigger;
	private int nextWaveCounter;
 	private List<GameObject> _units = new List<GameObject>();
	private List<GameObject> _explosions = new List<GameObject>();
	 public static Collider enemyCollision1 = new Collider();
	 public static int score= 0;
	private float delay = 0;
	private int ENEMY_COUNT = 5;
	private int EXPLOSION_COUNT = 5;
	
 

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!PlayerView.paused){
		if(_units!=null)
		EnemySignals.checkEnemies.dispatch();
		
		randSpawnTrigger = Random.Range (0,10);
		if(nextWaveCounter == 5)
		{
			delay +=Time.deltaTime;
			if(delay>0.1)
			{
				x = 0;
			nextWaveCounter = 0;
			delay = 0;
			}		
		}		

		if(randSpawnTrigger == 5&&x<5)
		{
			EnemySignals.enemyRelease.dispatch();
		}
		}
		
	}
		
	public void init(){
		//Initializations for bomb and explosions.
		for(int bmbCtr = 0; bmbCtr<ENEMY_COUNT; bmbCtr++){
			Bomb _bomb = new Bomb();
			_units.Add (_bomb.init());
			_units[bmbCtr].name = "bomb"+bmbCtr;
		}
		
		for(int explosionCtr = 0; explosionCtr<EXPLOSION_COUNT; explosionCtr++){
			Explosion _explosion = new Explosion();
			_explosions.Add(_explosion.init());
			_explosions[explosionCtr].particleEmitter.emit = false;
		}
		
		x=0; 
		nextWaveCounter = 0;
		score = 0;
	}

	public void dropEnemy(Vector3 position){
	randSpawnPoint = new Vector3(Random.Range (-9.5f,8.0f),20,-2f);
	_units[x].transform.position = randSpawnPoint;
	_units[x].SetActive(true);
	x=x+1;
	}	
		
public void checkConditions()
{
		
			for(int bmbCtr = 0; bmbCtr < ENEMY_COUNT; bmbCtr++){
				if(_units[bmbCtr]!=null){			
					if(_units[bmbCtr].transform.position.y<=2.0f&&_units[bmbCtr].activeSelf){	
						_units[bmbCtr].SetActive(false);
						explodeAnimation(_units[bmbCtr].transform.position);
						Vector3 rayDirection = transform.TransformDirection(Vector3.down);		
						if(Physics.Raycast(_units[bmbCtr].transform.position,rayDirection,out _enemyLine,5f)){				
						if(_enemyLine.collider.gameObject.CompareTag("base")){
           		 	 	Debug.DrawLine(_units[bmbCtr].transform.position, _enemyLine.point, Color.red);
						enemyCollision1 = _enemyLine.collider;
						Debug.Log ("Enemy hits something");
						Debug.Log (enemyCollision1.name);
						PlayerSignals.destroyBase.dispatch();
						}
					}				
				}
				if(_units[bmbCtr].activeSelf){
					_units[bmbCtr].transform.Translate(0,0.05f,0);
				}
			}
		}
}	
	public void enemyHit(string name)
	{
		for(int bmbCtr = 0; bmbCtr < ENEMY_COUNT; bmbCtr++){
			if(_units[bmbCtr].name == PlayerView.collider1.name)
			{
				explodeAnimation(_units[bmbCtr].transform.position);
				_units[bmbCtr].SetActive(false);

				score +=10;
			}
		}
	}
	
	public void explodeAnimation(Vector3 position)
	{
		for(int explosionCtr = 0; explosionCtr < EXPLOSION_COUNT; explosionCtr++){			
							_explosions[explosionCtr].transform.position = position;
							_explosions[explosionCtr].particleEmitter.Emit(100);							
							nextWaveCounter++;
							break;
					}	
	}
	
		public void onDestroy(){
		for(int bmbCtr = 0; bmbCtr<ENEMY_COUNT; bmbCtr++){
			Destroy(_units[bmbCtr]);
		}
		
		for(int explosionCtr = 0; explosionCtr<EXPLOSION_COUNT; explosionCtr++){
			Destroy (_explosions[explosionCtr]);
		}
				Destroy(this);
	}
	
	
}