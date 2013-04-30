using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerView:MonoBehaviour {

    public RaycastHit hit;
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
	public Transform cameraTransform = Camera.main.transform;
	public static int playerAmmo;
	 private AudioSource _shootAudio;  
	
	

    // Use this for initialization
    void Start () {
		
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
			if(Input.GetButtonDown("Fire1")){
				ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
				PlayerSignals.fireSignal.dispatch();
				_shootAudio.Play();
			}
		
			if(playerLife <=0||playerAmmo==0){
				Debug.Log ("Game Over");
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
		if(playerAmmo!=0){
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            	collider1 = hit.collider;
            	Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            	Debug.Log(collider1.name);
				Debug.Log (hit);
				localHit = transform.InverseTransformPoint(hit.point);
				Debug.Log (localHit);
				EnemySignals.destroyEnemy.dispatch();		
			}

		_playerWeapon.animation.Play("shoot");
		_playerWeaponFire.particleEmitter.Emit(1);
		playerAmmo -=1;
		}

} 
	public string returnCollider(){
		return collider1.name;
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
		GameObject.Find ("Player").GetComponent<MouseLook>().enabled = true;
		Debug.Log ("UnPause");
		Time.timeScale = 1f;
         return(false);
       }
       else{
		Debug.Log ("Pause");
		PlayerSignals.disableSignals.dispatch();
		EnemySignals.disableSignals.dispatch();
		PlayerSignals.showPauseMenu.dispatch();
		GameObject.Find ("Player").GetComponent<MouseLook>().enabled = false;
		Time.timeScale = 0f;
         return(true);    
       }
    }
	
	public void onMainMenuClicked(){
		//togglePause ();
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