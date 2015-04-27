using UnityEngine;
using System.Collections;

public class ShowResult : MonoBehaviour {

	public ChangedText changedText;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		changedText.result_time ();
		changedText.result_calories ();
	}
}
