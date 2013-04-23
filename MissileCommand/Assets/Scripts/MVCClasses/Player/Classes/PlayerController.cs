using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private PlayerMediator _playerMediator;
	private PlayerProxy _playerProxy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void addSignals(){
		PlayerSignals.fireSignal.add (onPlayerFire);
		PlayerSignals.passCollider.add (onEnemyDestroyed);
		PlayerSignals.destroyBase.add (onBaseDestroyed);
	}
	
	private void removeSignals(){
		PlayerSignals.fireSignal.remove(onPlayerFire);
		PlayerSignals.passCollider.remove (onPlayerFire);
		PlayerSignals.destroyBase.remove (onBaseDestroyed);
	}
	
	private void onPlayerFire(){
		_playerMediator.shoot();
	}
	
	private void onEnemyDestroyed(){
		_playerMediator.returnCollider();	
	}
	
	private void onBaseDestroyed(){
		_playerMediator.baseDestroyed();	
	}
	
	public void init(){
		initControllers();
		addSignals();
	}
	
	private void initControllers(){
		_playerMediator = gameObject.AddComponent<PlayerMediator>();
		_playerMediator.init();
		
		_playerProxy = new PlayerProxy();
		_playerProxy.init();
	}
	
	public void destroy(){
		removeSignals();
	}
}
