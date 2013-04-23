using UnityEngine;
using System.Collections;

public class PlayerProxy {

private PlayerDO _playerDO;

	public PlayerDO PlayerDO {
		get {
			return this._playerDO;
		}
		set {
			_playerDO = value;
		}
	}		
	
	public void init(){
		_playerDO = new PlayerDO();
	}
}
