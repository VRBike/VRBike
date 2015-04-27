using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadSinglePlayerScene(){
		Time.timeScale = 1.0F;
		ApplicationDOA.getInstance ().SetGameState (GameState.Playing);
		ApplicationDOA.getInstance ().SetGameMode (GameMode.Single);
		Application.LoadLevel ("SinglePlayerScene");
	}
	public void loadSceneMultiplayer(){
		Time.timeScale = 1.0F;
		ApplicationDOA.getInstance ().SetGameState (GameState.Playing);
		ApplicationDOA.getInstance ().SetGameMode (GameMode.Multi);
		Application.LoadLevel ("Multiplayer");
	}

	public void loadMainMenu(){
		Time.timeScale = 1.0F;
		ApplicationDOA.getInstance ().SetGameState (GameState.MainMenu);
		ApplicationDOA.getInstance ().SetGameMode (GameMode.MainMenu);
		Application.LoadLevel("MainMenu");
	}
}
