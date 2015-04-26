using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadScene(){
		Application.LoadLevel ("SinglePlayerScene");
	}
	public void loadSceneMultiplayer(){
		Application.LoadLevel ("Multiplayer");
	}
}
