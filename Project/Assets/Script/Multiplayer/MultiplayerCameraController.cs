using UnityEngine;
using System.Collections;

public class MultiplayerCameraController : MonoBehaviour {
	private bool active = false;
	private GameObject target = null;

	private Vector3 positionOffset = Vector3.zero;
	// Use this for initialization
	void Start () {
		active = false;
	}
	public void GameStart(int num){
		active = true;
		GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
		
		for(int i=0;i<p.Length;i++){
			if(p[i].networkView.isMine){
				target = p[i];
			}
		}
		Vector3 offset = new Vector3(0,0,0);
		
		if (num == 0) {
			offset = new Vector3(-3,1,0);
		} 
		else if (num == 1) {
			offset = new Vector3(-1,1,0);
		}
		else if (num == 2) {
			offset = new Vector3(1,1,0);
		}
		else if (num == 3) {
			offset = new Vector3(3,1,0);
		}
		positionOffset = target.transform.position - offset;

	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = target.transform.position + positionOffset;
			transform.rotation = target.transform.rotation;
		}
	}
}
