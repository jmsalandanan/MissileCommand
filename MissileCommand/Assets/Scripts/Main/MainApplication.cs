using UnityEngine;
using System.Collections;

public class MainApplication : MonoBehaviour {
	private EnemyController _enemyController;
	private PlayerController _playerController;
	private bool _isGameView = false;
	private bool _isHowTo = false;
	public bool _isMainMenu = true;
	public UIPanel MainPanel;  
	public UIPanel HowToPanel;
	public UIPanel pausePanel;
    public GameObject buttonStart;
	public GameObject buttonHowTo;
	public GameObject buttonBack;
	public GameObject buttonResume;
	public GameObject buttonReturntoMain;
	public Camera camera1;
	public Camera camera2;

	// Use this for initialization
	void Start () {
		HowToPanel.enabled = false;
		pausePanel.enabled = false;
		addSignals();
		camera1.enabled = true;
		camera2.enabled = false;
		buttonResume.SetActive(false);
		buttonReturntoMain.SetActive(false);
		GameObject.Find ("Player").GetComponent<MouseLook>().enabled =false;
		UIEventListener.Get(buttonStart).onClick += OnButtonStartClicked;
		UIEventListener.Get (buttonHowTo).onClick += OnButtonHowToClicked;
		UIEventListener.Get (buttonBack).onClick += OnBackClicked;
		UIEventListener.Get (buttonResume).onClick += OnResumeClick;
		Debug.Log ("Event Started");
	}
	
	// Update is called once per frame
	void Update () {
		if(_isGameView){
		startApplication();
		}	
		
		if(_isHowTo){
		HowToPanel.enabled = true;
		MainPanel.enabled = false;
		_isHowTo = false;
		}
		
		if(_isMainMenu)
		{
		HowToPanel.enabled = false;
		MainPanel.enabled = true;
		_isMainMenu = false;
		}
	}
	
	private void startApplication(){	
	Debug.Log ("Game Enters");
	MainPanel.enabled = false;
	HowToPanel.enabled = false;
	buttonBack.SetActive(false);
	buttonHowTo.SetActive (false);
	buttonStart.SetActive(false);
	camera1.enabled = !camera1.enabled;
	camera2.enabled = !camera2.enabled;
	GameObject.Find ("Player").GetComponent<MouseLook>().enabled = true;
	_enemyController = gameObject.AddComponent<EnemyController>();	
	_enemyController.init();
	_playerController = gameObject.AddComponent<PlayerController>();
	_playerController.init();	
	_isGameView=false;	
	}
	
		private void addSignals(){
		PlayerSignals.showPauseMenu.add (togglePauseMenu);
		PlayerSignals.onPause.add (onPause);
	}
	
	void OnButtonStartClicked(GameObject go){
		Debug.Log ("start button clicked");
		_isGameView = true;
	}
	
	void OnButtonHowToClicked(GameObject go){
		Debug.Log ("How to button clicked");
		_isHowTo = true;
	}
	
	void OnBackClicked(GameObject go){
		Debug.Log ("back clicked");
		_isMainMenu = true;
	}
	
	void OnResumeClick(GameObject go){
		Debug.Log ("resume clicked");
		_playerController.onResumeClicked();
		//pausePanel.enabled = false;
	}
	void onPause(){
		_playerController.onResumeClicked();	
	}
	
//Change	
	
	public void togglePauseMenu(){
		pausePanel.enabled = !pausePanel.enabled;
		buttonResume.SetActive(!buttonResume.activeSelf);
		buttonReturntoMain.SetActive(!buttonReturntoMain.activeSelf);
	}
	

}
