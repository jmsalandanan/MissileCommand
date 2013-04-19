using UnityEngine;
using System.Collections;

public class TestMVCMediator : MonoBehaviour {
	private TestView _testView;
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(){
		_testView = gameObject.AddComponent<TestView>();
		_testView.init();
	}
	
	public void updateScale(Vector3 scale){
		_testView.updateCubeScale(scale);
	}
}
