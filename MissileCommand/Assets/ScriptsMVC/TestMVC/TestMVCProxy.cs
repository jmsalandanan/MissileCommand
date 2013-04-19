using UnityEngine;
using System.Collections;

public class TestMVCProxy {
	
	private TestDO _testDO;

	public TestDO TestDO {
		get {
			return this._testDO;
		}
		set {
			_testDO = value;
		}
	}		
	
	public void init(){
		_testDO = new TestDO();
	}
}
