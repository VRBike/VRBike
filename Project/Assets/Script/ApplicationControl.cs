using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ApplicationControl : MonoBehaviour {

	bool paused = false;
	public GameObject canvas;
	private bool isShowing;

	// Use this for initialization
	void Start () {
		canvas.SetActive (false);;
	}
	
	// Update is called once per frame
	void Update () {
		ApplicationDOA.getInstance ().startTimer ();
		if (Cardboard.SDK.CardboardTriggered) {
			if(paused == false){
				OnPauseGame();
				paused = true;
				//OnApplicationPause(false);
			}
		}
	}

	public void OnPauseGame(){
		print ("paused");
		isShowing = !isShowing;
		canvas.SetActive(true);
	}

	public void OnResumeGame(){
		print ("resume");
		isShowing = !isShowing;
		canvas.SetActive(false);
		paused = false;
	}

	public void OnFinishGame(){

	}
	
	/*void OnApplicationPause(bool pauseStatus) {
		paused = pauseStatus;
	}*/
}