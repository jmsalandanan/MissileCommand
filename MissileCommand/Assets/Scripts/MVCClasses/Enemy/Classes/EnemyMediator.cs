using UnityEngine;
using System.Collections;

public class EnemyMediator : MonoBehaviour {
	private EnemyView _enemyView;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void init(){
		_enemyView = gameObject.AddComponent<EnemyView>();
		_enemyView.init();
	}
	public void releaseEnemy(Vector3 position){
		_enemyView.dropEnemy(position);
	}
	public void explode(){
		//_enemyView.explosion();//store coordinates to db? then send it here	
	}
	public void checkEnemy(){
		_enemyView.checkConditions();	
	}
	public void enemyHit(string name){
		_enemyView.enemyHit(name);
	}
	public void onDestroy(){
		_enemyView.onDestroy();	
	}
}
