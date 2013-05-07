using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerView:MonoBehaviour {


    public static Collider collider1 = new Collider();
    private Ray ray;
	private EnemyView _enemyView;
	private SoundManager _soundManager;
    private Vector3 vec;
    private LayerMask layerMask;
	public static Vector3 localHit;
	private Rigidbody newObj;
	public float memberVariable = 0.0F;
	private float _basePositionX = -7.75f;
	private Vector3 _basePosition;
	private GameObject _base;
	private GameObject _base2;
	private GameObject _base3;
	private GameObject _base4;
	private GameObject _playerWeapon;
	private GameObject _playerWeaponFire;
	private GameObject _base5;
	public static bool paused = false;
	public static int playerLife;
	public static int score= 0;
	private List <GameObject> _baseList = new List<GameObject>();
	public Transform cameraTransform = null;
	public static int playerAmmo;
	 private AudioSource _shootAudio;  
	
	

    // Use this for initialization
    void Start () {
		cameraTransform = Camera.main.transform;
    }
  
	public void init(){
		Debug.Log (Camera.main.transform.position);
		Debug.Log ("Init");
		_base = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Base"));
		_base2 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Base"));
		_base3 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Base"));
		_base4 = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Base"));

		_base.name = "base1";
		_base2.name = "base2";
		_base3.name = "base3";
		_base4.name = "base4";

		_baseList.Add(_base);
		_baseList.Add(_base2);
		_baseList.Add(_base3);
		_baseList.Add(_base4);
		
		_playerWeapon = GameObject.Find("bazooka");
		_playerWeaponFire = GameObject.Find ("Flame");
		_playerWeaponFire.particleEmitter.emit = false;
		
		paused = false;
		playerLife = 4;
		playerAmmo = 15;
		score = 0;
		_shootAudio = new AudioSource();
		_shootAudio = gameObject.AddComponent<AudioSource>();

		_shootAudio.clip = Resources.Load("Cannon") as AudioClip;
		for(int x=0;x<4;x++){
			_basePosition = new Vector3(_basePositionX,0f,-2f);
			_baseList[x].transform.position= _basePosition;
			_basePositionX += 5;
		}
		
	}
	
    void Update () {
		if(!paused){
			RaycastHit hit;
			ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			if(Input.GetButtonDown("Fire1"))
			{
				_shootAudio.Play();
				if (Physics.Raycast(ray, out hit, 100f)){
            		collider1 = hit.collider;
            		Debug.DrawLine(Camera.main.transform.position, ray.direction, Color.red);
					Debug.Log (collider1);
            		Debug.Log(collider1.name);
					Debug.Log (hit);
					Debug.Log (hit.collider);
					Debug.Log (ray.direction);
					Debug.Log (ray);
					EnemySignals.destroyEnemy.dispatch();
					collider1.name = null;
					collider1 = null;
				}
				else{
					Debug.Log ("Miss!");
					Debug.DrawLine(Camera.main.transform.position, ray.direction, Color.green);
				}
		_playerWeapon.animation.Play("shoot");
		_playerWeaponFire.particleEmitter.Emit(1);
		playerAmmo -=1;
		}
						
			if(playerLife <=0||playerAmmo<=0){
				Debug.Log ("Game Over");
				Time.timeScale = 0f;
				PlayerSignals.disableSignals.dispatch();
				EnemySignals.disableSignals.dispatch();
				PlayerSignals.onGameOver.dispatch();
				UILabel scoreLabel = GameObject.Find("ScoreLabel").GetComponent<UILabel>();
				scoreLabel.text = score.ToString();
			}		
		  	if(Input.GetKey(KeyCode.P)){
				PlayerSignals.onPause.dispatch();
		  	}
					
		}
	}
	
	public void fireWeapon(){
		Debug.Log ("asd");
		/*if(playerAmmo!=0){
			_shootAudio.Play();

				if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            		collider1 = hit.collider;
            		Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            		Debug.Log(collider1.name);
					Debug.Log (hit);
					EnemySignals.destroyEnemy.dispatch();
					collider1.name = null;
					collider1 = null;
				}
				else{
					Debug.Log ("Miss!");
	
				
				}
		_playerWeapon.animation.Play("shoot");
		_playerWeaponFire.particleEmitter.Emit(1);
		playerAmmo -=1;
		}*/
} 

	
	public void deactivateBase(){
		foreach(GameObject _baseIns in _baseList){
			if(_baseIns.name == EnemyView.enemyCollision1.name){
				_baseIns.SetActive(false);
				playerLife -= 1;
				Debug.Log (playerLife);		
			}
		}			
	}
	 
    public bool togglePause(){
       if(Time.timeScale == 0f){
		PlayerSignals.enableSignals.dispatch();
		EnemySignals.enableSignals.dispatch();
		PlayerSignals.showPauseMenu.dispatch();
		Debug.Log ("UnPause");
		Time.timeScale = 1f;
         return(false);
       }
        else{
		Debug.Log ("Pause");
		PlayerSignals.disableSignals.dispatch();
		EnemySignals.disableSignals.dispatch();
		PlayerSignals.showPauseMenu.dispatch();
		Time.timeScale = 0f;
         return(true);    
       }
    }
	
	public void onMainMenuClicked(){
		PlayerSignals.showPauseMenu.dispatch ();
		PlayerSignals.disableSignals.dispatch();
		EnemySignals.disableSignals.dispatch();
		Time.timeScale = 1f;
	}
	
	public void onDestroy(){
		for(int x=0;x<4;x++){
			Destroy(_baseList[x]);
		}
			Destroy(this);
			
	}
	
}