using UnityEngine;
using System.Collections;

public class TestMVCController : MonoBehaviour {
	
	private TestMVCMediator _testMVCMediator;
	private TestMVCProxy _testMVCProxy;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void addSignals(){
		TestSignals.normalScaleSignal.add(onNormalScale);
		TestSignals.changeScaleSignal.add(onChangeScale);
	}
	
	private void removeSignals(){
		TestSignals.normalScaleSignal.remove(onNormalScale);
		TestSignals.changeScaleSignal.remove(onChangeScale);
	}
	
	private void onNormalScale(){
		_testMVCMediator.updateScale(_testMVCProxy.TestDO.initialScale);
	}
	
	private void onChangeScale(){
		_testMVCMediator.updateScale(_testMVCProxy.TestDO.changedScale);
	}
	
	public void init(){
		initControllers();
		addSignals();
	}
	
	private void initControllers(){
		_testMVCMediator = gameObject.AddComponent<TestMVCMediator>();
		_testMVCMediator.init();
		
		_testMVCProxy = new TestMVCProxy();
		_testMVCProxy.init();
	}
	
	public void destroy(){
		removeSignals();
	}
}
