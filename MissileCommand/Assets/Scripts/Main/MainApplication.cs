using UnityEngine;
using System.Collections;

public class MainApplication : MonoBehaviour {
	private EnemyController _enemyController;
	// Use this for initialization
	void Start () {
		startApplication();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void startApplication(){
	_enemyController = gameObject.AddComponent<EnemyController>();	
	_enemyController.init();
	}
}
