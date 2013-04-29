using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private EnemyMediator _enemyMediator;
	private EnemyProxy _enemyProxy;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void addSignals(){
		EnemySignals.enemyRelease.add (onEnemyRelease);
		EnemySignals.explodeSignal.add (onExplode);
		EnemySignals.checkEnemies.add (onCheck);
		EnemySignals.destroyEnemy.add (onDestroy);
		EnemySignals.enableSignals.add(enableSignals);//transfer to diff functions
		EnemySignals.disableSignals.add(DisableSignals);
	}
	
	public void removeSignals(){
		EnemySignals.enemyRelease.remove (onEnemyRelease);
		EnemySignals.explodeSignal.remove (onExplode);
		EnemySignals.checkEnemies.remove (onCheck);
		EnemySignals.destroyEnemy.remove (onDestroy);
		EnemySignals.enableSignals.remove (enableSignals);
		EnemySignals.disableSignals.remove (DisableSignals);
	}
	
	private void onEnemyRelease(){
		_enemyMediator.releaseEnemy(_enemyProxy.EnemyDO.randSpawnPoint);
	}
	
	private void onExplode(){
		_enemyMediator.explode();
	}
	
	private void onCheck(){
		_enemyMediator.checkEnemy();
	}
	
	private void onDestroy(){
		_enemyMediator.enemyHit(name);	
	}
	
	public void init(){
		initControllers();
		addSignals();
	}
	
	private void initControllers(){
		_enemyMediator = gameObject.AddComponent<EnemyMediator>();
		_enemyMediator.init();
		
		_enemyProxy = new EnemyProxy();
		_enemyProxy.init();
	}
	
	public void enableSignals(){
	Debug.Log ("Enemy Signal Enabled");
	EnemySignals.enemyRelease.add(onEnemyRelease);
	EnemySignals.explodeSignal.add(onExplode);
	EnemySignals.checkEnemies.add(onCheck);
	EnemySignals.destroyEnemy.add(onDestroy);
	}
	
	public void DisableSignals(){
	Debug.Log ("Enemy Signal Disabled");
	EnemySignals.enemyRelease.remove(onEnemyRelease);
	EnemySignals.explodeSignal.remove(onExplode);
	EnemySignals.checkEnemies.remove(onCheck);
	EnemySignals.destroyEnemy.remove(onDestroy);
	}
	
	public void destroy(){
		_enemyMediator.onDestroy();
		Destroy (_enemyMediator);
		removeSignals ();
	}
}

