using UnityEngine;
using System.Collections;

public class EnemyProxy {

private EnemyDO _enemyDO;

	public EnemyDO EnemyDO {
		get {
			return this._enemyDO;
		}
		set {
			_enemyDO = value;
		}
	}		
	
	public void init(){
		_enemyDO = new EnemyDO();
	}
}
