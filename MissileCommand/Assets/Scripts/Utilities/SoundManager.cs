using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public AudioClip shoot;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		}
	
	public void CannonFireAudio(){
		audio.PlayOneShot(shoot);	
	}
}

