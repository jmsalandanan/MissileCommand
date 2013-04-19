using UnityEngine;
using System.Collections;

public class EnemyView : MonoBehaviour {
	//public Rigidbody _enemy;
	private GameObject _bomb;
	private GameObject _explosion;
	private Vector3 randCoord;
	

	// Use this for initialization
	void Start () {
			EnemySignals.enemyRelease.dispatch();
	}
	
	// Update is called once per frame
	void Update () {
		if(_bomb!=null)
		{
		  //dropEnemy();	
		}	
	
	}
	public void init(){

	}

	public void dropEnemy(){

		_bomb = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bomb"));
		_bomb.transform.position.Set(-3,25,-5);
		_bomb.transform.Translate(Vector3.down * 0.5f);

		if(_bomb.transform.position.y>0&&_bomb.transform.position.y<=2) //Change to oncollision w/ ground
		{	
			EnemySignals.explodeSignal.dispatch();
		}
	}
	
	public void explosion()
	{
			Debug.Log (_bomb.transform.position);
			_explosion = (GameObject) GameObject.Instantiate((Resources.Load("Prefabs/BombExplosion")),_bomb.transform.position,Quaternion.Euler(_bomb.transform.position));
			Destroy(_bomb);
			Destroy (_explosion,0.2f);			
	}

}
