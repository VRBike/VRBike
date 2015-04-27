using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ApplicationControl : MonoBehaviour {

	bool paused = false;
	public GameObject PauseCanvas;
	public GameObject AdjustCanvas;
	public GameObject FinishCanvas;
	public ChangedText changedtext;

	// Use this for initialization
	void Start () {
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
		PauseCanvas.SetActive(true);
		AdjustCanvas.SetActive (true);
	}

	public void OnResumeGame(){
		print ("resume");
		ApplicationDOA.getInstance ().SetGameState (GameState.Playing);
		PauseCanvas.SetActive(false);
		AdjustCanvas.SetActive (false);
	}

	public void OnFinishGame(){
		print ("finish");
		ApplicationDOA.getInstance ().SetGameState (GameState.Finish);
		PauseCanvas.SetActive(false);
		AdjustCanvas.SetActive (false);
		FinishCanvas.SetActive (false);

		changedtext.result_time ();

		ApplicationDOA.getInstance().endTimer();
	}
}