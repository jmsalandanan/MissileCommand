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
		PlayerSignals.destroyBase.add (onBaseDestroyed);
		PlayerSignals.enableSignals.add(enableSignals);
		PlayerSignals.disableSignals.add(disableSignals);
	}
	
	private void removeSignals(){
		PlayerSignals.fireSignal.remove(onPlayerFire);
		PlayerSignals.passCollider.remove (onPlayerFire);
		PlayerSignals.destroyBase.remove (onBaseDestroyed);
		PlayerSignals.enableSignals.remove (enableSignals);
		PlayerSignals.disableSignals.remove (disableSignals);
	}
	
	private void onPlayerFire(){
		_playerMediator.shoot();
	}
	
	
	
	private void onBaseDestroyed(){
		_playerMediator.baseDestroyed();	
	}
	
	public void init(){
		initControllers();
		addSignals();
	}
	
	public void enableSignals(){
	Debug.Log ("Player Signal Enabled");
	PlayerSignals.fireSignal.add (onPlayerFire);

	PlayerSignals.destroyBase.add (onBaseDestroyed);	
	}
	
	public void disableSignals(){
	Debug.Log ("Player Signal Disabled");
	PlayerSignals.fireSignal.remove(onPlayerFire);
	PlayerSignals.passCollider.remove (onPlayerFire);
	PlayerSignals.destroyBase.remove (onBaseDestroyed);
	}
	
	private void initControllers(){
		_playerMediator = gameObject.AddComponent<PlayerMediator>();
		_playerMediator.init();
		
		_playerProxy = new PlayerProxy();
		_playerProxy.init();
	}
	
	public void onResumeClicked(){
		_playerMediator.onResumeClicked();	
	}
	
	public void onMainMenuClicked(){
		_playerMediator.onMainMenuClicked();	
	}
	
	public void fireButtonPressed(){
		_playerMediator.fireButtonPressed();
	}
	
	public void destroy(){
		removeSignals();
		_playerMediator.onDestroy();
		Destroy (_playerMediator);
	}
}
