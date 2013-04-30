using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyView : MonoBehaviour {
	
	public Vector3 randSpawnPoint;
	private RaycastHit _enemyLine;
	private int _x;
	private int _randSpawnTrigger;
	private int _nextWaveCounter;
 	private List<GameObject> _units = new List<GameObject>();
	private List<GameObject> _explosions = new List<GameObject>();
	 public static Collider enemyCollision1 = new Collider();
	private float _delay = 0;
	private int ENEMY_COUNT = 5;
	private int EXPLOSION_COUNT = 5;
	private int PREV_ENEMY_COUNT;
	public static int levelCount;
	private int _numberOfEnemies;
	private AudioSource _explosionAudio;
	
 

	void Start () {	
		_x=0; 
		_nextWaveCounter = 0;
		levelCount = 1;
		PREV_ENEMY_COUNT = 0;
		_numberOfEnemies = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if(!PlayerView.paused){
			if(_units!=null)
				EnemySignals.checkEnemies.dispatch();
			
			if(_nextWaveCounter == _numberOfEnemies){					
				_x = 0;
				_nextWaveCounter = 0;
				_delay = 0;
				levelCount++;
				PlayerView.playerAmmo +=5;
				Debug.Log (levelCount);
				PREV_ENEMY_COUNT = ENEMY_COUNT;
				ENEMY_COUNT +=2;
				EXPLOSION_COUNT +=2;
				_numberOfEnemies +=2;
				init ();
							
			}
				_delay +=Time.deltaTime;
			if(_delay>2){
				if(_x<_numberOfEnemies){
					EnemySignals.enemyRelease.dispatch();
					_delay = 0;
			}
				}
		}
		
	}
		
	public void init(){
		//Initializations for bomb and explosions.
		for(int bmbCtr = PREV_ENEMY_COUNT; bmbCtr<ENEMY_COUNT; bmbCtr++){
			Bomb _bomb = new Bomb();
			_units.Add (_bomb.init());
			_units[bmbCtr].name = "bomb"+bmbCtr;
		}
		
		for(int explosionCtr = PREV_ENEMY_COUNT; explosionCtr<EXPLOSION_COUNT; explosionCtr++){
			Explosion _explosion = new Explosion();
			_explosions.Add(_explosion.init());
			_explosions[explosionCtr].particleEmitter.emit = false;
		}
		
		_explosionAudio = new AudioSource();
		_explosionAudio = gameObject.AddComponent<AudioSource>();
		_explosionAudio.clip = Resources.Load("Grenade") as AudioClip;

	}

	public void dropEnemy(Vector3 position){
	randSpawnPoint = new Vector3(Random.Range (-9.5f,8.0f),20,-3f);
	_units[_x].transform.position = randSpawnPoint;
	_units[_x].SetActive(true);
	_x=_x+1;
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
					_units[bmbCtr].transform.Translate(0,0.01f * levelCount,0);
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
				PlayerView.score +=10;

			}
		}
	}
	
	public void explodeAnimation(Vector3 position)
	{
		for(int explosionCtr = 0; explosionCtr < EXPLOSION_COUNT; explosionCtr++){			
							_explosions[explosionCtr].transform.position = position;
							_explosions[explosionCtr].particleEmitter.Emit(100);							
							_nextWaveCounter++;
							_explosionAudio.Play();
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