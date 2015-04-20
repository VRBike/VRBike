using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangedText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeText ();
	}

	public void timeText(){
		Text time = gameObject.GetComponent<Text>();
		float timef = ApplicationDOA.getInstance().getTime();
		time.text = timef.ToString ("####0.00");
	}

	public void caloriesText(){

	}
}
