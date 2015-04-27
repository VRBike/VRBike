using UnityEngine;
using System.Collections;

public class MultiPlayerApplicationControl : MonoBehaviour {

	bool paused = false;
	private GameObject PauseCanvas;
	private GameObject AdjustCanvas;
	private GameObject FinishCanvas;
	private GameObject Interface;
	private bool inGame;

	// Use this for initialization
	void Start () {
		Interface = GameObject.Find ("Interface");
		PauseCanvas = GameObject.Find ("PausedMenu");
		AdjustCanvas = GameObject.Find ("AdjustCanvas");
		FinishCanvas = GameObject.Find ("FinishCanvas");
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
		FinishCanvas.SetActive (false);
		inGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inGame) {
			if(ApplicationDOA.getInstance().gameState == GameState.Playing){
				if (Cardboard.SDK.CardboardTriggered) {
					if (paused == false) {
						OnPauseGame ();
						paused = true;
					}
				}
			}
		}
	}
	public void SetInGame(bool bo){
		inGame = bo;
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
		
		popFinish ();
		
		ApplicationDOA.getInstance ().endTimer ();
	}

	public void popFinish(){
		FinishCanvas.SetActive (true);
	}

	public void HostStartGame(){
		
		GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
		
		for (int i=0; i<p.Length; i++) {
			
			if (p [i].networkView.isMine) {
				p [i].GetComponent<PlayerControllerMultiplayer> ().SendStartGame ();
				
			}
		}
	}
}
