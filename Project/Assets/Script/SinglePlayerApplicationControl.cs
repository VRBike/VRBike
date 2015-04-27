using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SinglePlayerApplicationControl : MonoBehaviour {

	bool paused = false;
	private GameObject PauseCanvas;
	private GameObject AdjustCanvas;
	private GameObject FinishCanvas;
	private GameObject Interface;

	// Use this for initialization
	void Start () {
		ApplicationDOA.getInstance ().set_start_position (GameObject.Find("Player").transform.position);
		Interface = GameObject.Find ("Interface");
		PauseCanvas = GameObject.Find ("PausedMenu");
		AdjustCanvas = GameObject.Find ("AdjustCanvas");
		FinishCanvas = GameObject.Find ("FinishCanvas");
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
		FinishCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		ApplicationDOA.getInstance ().startTimer ();
		if (Cardboard.SDK.CardboardTriggered) {
			if(paused == false){
				OnPauseGame();
				paused = true;
			}
		}
	}

	public void OnPauseGame(){
		print ("paused");
		ApplicationDOA.getInstance ().SetGameState (GameState.Paused);
		Time.timeScale = 0;
		PauseCanvas.SetActive (true);
		AdjustCanvas.SetActive (true);
	}

	public void OnResumeGame(){
		print ("resume");
		ApplicationDOA.getInstance ().SetGameState (GameState.Playing);
		Time.timeScale = 1.0F;
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
		paused = false;
	}

	public void OnFinishGame(){
		print ("finish");
		ApplicationDOA.getInstance ().SetGameState (GameState.Finish);
		PauseCanvas.SetActive(false);
		AdjustCanvas.SetActive (false);
		Interface.SetActive (false);

		FinishCanvas.SetActive (true);

		ApplicationDOA.getInstance ().endTimer ();
	}
	
	/*void OnApplicationPause(bool pauseStatus) {
		paused = pauseStatus;
	}*/
}