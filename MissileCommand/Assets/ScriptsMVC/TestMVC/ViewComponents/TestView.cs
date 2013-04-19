using UnityEngine;
using System.Collections;

public class TestView : MonoBehaviour {
	
	private GameObject _testCube;
	private DirectionType _dirType = DirectionType.Left;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_testCube != null){
			moveCube();
		}
	}
	
	private void moveCube(){
		float testCubeXPos = _testCube.transform.position.x;
		if (_dirType == DirectionType.Left){
			_testCube.transform.Translate(Vector3.left * 0.5f);
		} else if (_dirType == DirectionType.Right){
			_testCube.transform.Translate(Vector3.right * 0.5f);
		}
		
		if (testCubeXPos < -3){
			TestSignals.changeScaleSignal.dispatch();
			_dirType = DirectionType.Right;
		} 
		
		if (testCubeXPos > 3) {
			TestSignals.normalScaleSignal.dispatch();
			_dirType = DirectionType.Left;
			
		}
		
		Debug.Log(testCubeXPos);
	}
	
	public void init(){
		_testCube = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/TestCube"));
	}
	
	public void updateCubeScale(Vector3 vector3){
		_testCube.transform.localScale = vector3;
	}
}
