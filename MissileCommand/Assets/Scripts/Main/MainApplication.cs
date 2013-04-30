using UnityEngine;
using System.Collections;

public class MainApplication : MonoBehaviour {
	private EnemyController _enemyController;
	private PlayerController _playerController;
	private bool _isGameView = false;
	private bool _isHowTo = false;
	public bool _isMainMenu = true;
	public GameObject MainPanel;  
	public GameObject HowToPanel;
	public GameObject pausePanel;
	public GameObject gameOverPanel;
    public GameObject buttonStart;
	public GameObject buttonHowTo;
	public GameObject buttonBack;
	public GameObject buttonResume;
	public GameObject buttonReturntoMain;
	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	// Use this for initialization
	void Start () {
		NGUITools.SetActive (HowToPanel,false);
		NGUITools.SetActive (pausePanel,false);
		NGUITools.SetActive (gameOverPanel,false);
		addSignals();
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
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
		NGUITools.SetActive(MainPanel,false);
		NGUITools.SetActive (HowToPanel.gameObject,true);
		
		_isHowTo = false;
		}
		
		if(_isMainMenu)
		{
		NGUITools.SetActive(MainPanel,true);
		NGUITools.SetActive (HowToPanel.gameObject,false);
		_isMainMenu = false;
		}
	}
	
	private void startApplication(){	
	Debug.Log ("Game Enters");
	NGUITools.SetActive (MainPanel,false);
	NGUITools.SetActive (HowToPanel,false);
	NGUITools.SetActive (gameOverPanel,false);
	NGUITools.SetActive (pausePanel,false);
	buttonBack.SetActive(false);
	buttonHowTo.SetActive (false);
	buttonStart.SetActive(false);
		
	camera1.enabled = false;
	camera2.enabled = true;
	camera3.enabled = false;
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
		PlayerSignals.onGameOver.add (showGameOverScreen);
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
	}
	
	void OnMainMenuClick(GameObject go){
		Debug.Log ("Main Menu Clicked");
		_playerController.onMainMenuClicked();
		_isMainMenu = true;
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		buttonHowTo.SetActive (true);
		buttonStart.SetActive(true);
		_playerController.destroy();
		_enemyController.destroy();
		DestroyImmediate (_enemyController,true);
		GameObject.Find ("Player").GetComponent<MouseLook>().enabled = false;
		NGUITools.SetActive(pausePanel,false);
		NGUITools.SetActive(gameOverPanel,false);
		
	}
	void onPause(){
		_playerController.onResumeClicked();	
	}
	
	
	public void togglePauseMenu(){
		NGUITools.SetActive(pausePanel,!pausePanel.activeSelf);
		buttonResume.SetActive(true);
		buttonReturntoMain.SetActive(true);
	}
	
	public void showGameOverScreen(){
		NGUITools.SetActive (gameOverPanel,true);
		//camera2.enabled = false;
		GameObject.Find ("Player").GetComponent<MouseLook>().enabled = false;
		camera3.enabled = true;
	}
}
