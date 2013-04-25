using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerView:MonoBehaviour {

    public RaycastHit hit;
    public static Collider collider1 = new Collider();
    private Ray ray;
	private EnemyView _enemyView;
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
	public static int playerLife =4;
	private List <GameObject> _baseList = new List<GameObject>();
	public Transform cameraTransform = Camera.main.transform;
	public static int playerAmmo = 15;
    // Use this for initialization
    void Start () {
		 //point = (Camera) GameObject.FindObjectOfType(typeof(Camera));
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
		for(int x=0;x<4;x++){
			_basePosition = new Vector3(_basePositionX,0f,-2f);
			_baseList[x].transform.position= _basePosition;
			_basePositionX += 5;
		}
		
	}
	
    void Update () {
      
        // Find the centre of the Screen
        vec.x = (float)Screen.width / 2;
        vec.y = (float)Screen.height / 2;
        vec.z = 0;

		if(Input.GetButtonDown("Fire1"))
		{
			ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			PlayerSignals.fireSignal.dispatch();
		}


	}
	
	public void fireWeapon(){
		if(playerAmmo!=0)
		{
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //remove layerMask if you remove it in line above
        		{
			    // Create the actual Ray based on the screen vector above
           		//stores the object hit
            	collider1 = hit.collider;
				
            	// Draws a line to show the actual ray.
            	Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            	Debug.Log(collider1.name); // Outputs the name of the object hit
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
		foreach(GameObject _baseIns in _baseList)
		{
			if(_baseIns.name == EnemyView.enemyCollision1.name)
			{
				_baseIns.SetActive(false);
				playerLife -= 1;
				Debug.Log (playerLife);
				
			}
		}
			
	}
}