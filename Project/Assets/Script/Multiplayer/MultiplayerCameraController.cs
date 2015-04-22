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
	public void GameStart(){
		active = true;
		GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
		
		for(int i=0;i<p.Length;i++){
			if(p[i].networkView.isMine){
				target = p[i];
			}
		}
		positionOffset = target.transform.position - new Vector3(0,1,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = target.transform.position + positionOffset;
		}
	}
}
