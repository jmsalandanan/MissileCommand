using UnityEngine;
using System.Collections;

public class MainApplication : MonoBehaviour {
	private EnemyController _enemyController;
	private PlayerController _playerController;

	// Use this for initialization
	void Start () {
		startApplication();
		addListeners();
	}
	
	// Update is called once per frame
	void Update () {

	}


	
	private void startApplication(){
	_enemyController = gameObject.AddComponent<EnemyController>();	
	_enemyController.init();
	_playerController = gameObject.AddComponent<PlayerController>();
	_playerController.init();
	}
	
	private void addListeners(){
		EnemySignals.destroyEnemy.add(onEnemyHit);
	}
	
	private void removeListeners(){
		EnemySignals.destroyEnemy.remove(onEnemyHit);
	}
		
	private void onEnemyHit(){
		Debug.Log("Enemy Hit: ");
	}
	

}
