using UnityEngine;
using System.Collections;

public class PlayerMediator : MonoBehaviour {
	private PlayerView _playerView;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void init(){
		_playerView = gameObject.AddComponent<PlayerView>();
		_playerView.init();
	}
	public void shoot(){
		_playerView.fireWeapon();
	}
	
	public void returnCollider(){
		_playerView.returnCollider();	
	}
	
	public void baseDestroyed(){
		_playerView.deactivateBase();	
	}
	
		public void onResumeClicked(){
		PlayerView.paused = _playerView.togglePause();
	}
	

}
