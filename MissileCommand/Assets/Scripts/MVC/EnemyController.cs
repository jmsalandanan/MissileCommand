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
	
	private void addSignals(){
		EnemySignals.enemyRelease.add (onEnemyRelease);
		EnemySignals.explodeSignal.add (onExplode);
	}
	
	private void removeSignals(){
		EnemySignals.enemyRelease.remove (onEnemyRelease);
		EnemySignals.explodeSignal.remove (onExplode);
	}
	
	private void onEnemyRelease(){
		_enemyMediator.releaseEnemy();
	}
	
	private void onExplode(){
		_enemyMediator.explode();
	}
	
	public void init(){
		initControllers();
		addSignals();
	}
	
	private void initControllers(){
		_enemyMediator = gameObject.AddComponent<EnemyMediator>();
		_enemyMediator.init();
		
		//_enemyProxy = new EnemyProxy();
		//_enemyProxy.init();
	}
	
	public void destroy(){
		removeSignals();
	}
}

