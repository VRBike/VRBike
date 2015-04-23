using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangedText : MonoBehaviour {
	
	Vector3 lastPosition = new Vector3(0, 0, 0);
	float distanceTravelled = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeText ();
		speedText ();
		distanceText ();
	}

	public void timeText(){
		Text time = GameObject.Find("TimeText").GetComponent<Text>();
		float timef = ApplicationDOA.getInstance().getTime();
		time.text = timef.ToString ("####0.00");
	}

	public void speedText(){

	}

	public void distanceText(){
		Text distance = GameObject.Find ("DistanceNumber").GetComponent<Text> ();
		Vector3 currentPosition = GameObject.Find ("Player").transform.position;
		distanceTravelled += Vector3.Distance (currentPosition, lastPosition);
		lastPosition = currentPosition;
		distance.text = distanceTravelled.ToString("####0.0 " + "M");
	}

	public void caloriesText(){

	}
}
