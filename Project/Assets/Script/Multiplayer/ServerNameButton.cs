using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerNameButton : MonoBehaviour {
	public Text name;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnClick(){
		GameObject.Find ("NetworkController").GetComponent<MultiplayerController> 
			().JoinServer(name);
	}
}
