using UnityEngine;
using System.Collections;

public class MultiPlayerApplicationControl : MonoBehaviour {

	bool paused = false;
	public GameObject PauseCanvas;
	public GameObject AdjustCanvas;
	private bool isShowing;
	private bool inGame;

	// Use this for initialization
	void Start () {
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
		inGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inGame) {
			ApplicationDOA.getInstance ().startTimer ();
			if (Cardboard.SDK.CardboardTriggered) {
				if (paused == false) {
					OnPauseGame ();
					paused = true;
					//OnApplicationPause(false);
				}
			}
		}
	}
	public void SetInGame(bool bo){
		inGame = bo;
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
