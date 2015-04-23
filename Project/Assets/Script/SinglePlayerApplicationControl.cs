using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SinglePlayerApplicationControl : MonoBehaviour {

	bool paused = false;
	public GameObject PauseCanvas;
	public GameObject AdjustCanvas;
	private bool isShowing;

	// Use this for initialization
	void Start () {
		ApplicationDOA.getInstance ().set_start_position (GameObject.Find("Player").transform.position);
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
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
		Time.timeScale = 0;
		PauseCanvas.SetActive (true);
		AdjustCanvas.SetActive (true);
	}

	public void OnResumeGame(){
		print ("resume");
		isShowing = !isShowing;
		Time.timeScale = 1.0F;
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
		paused = false;
	}

	public void OnFinishGame(){

	}
	
	/*void OnApplicationPause(bool pauseStatus) {
		paused = pauseStatus;
	}*/
}