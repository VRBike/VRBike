using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangedText : MonoBehaviour {
	
	private Vector3 lastPosition = new Vector3(0, 0, 0);
	private float distanceTravelled = 0;
	private NetworkScript MC;
	private double calories_burned = 0;

	// Use this for initialization
	void Start () {
		MC = GameObject.Find("Network").GetComponent<NetworkScript>();
	}
	
	// Update is called once per frame
	void Update () {
		timeText ();
		speedText ();
		distanceText ();
		if(ApplicationDOA.getInstance().gameState == GameState.Finish && ApplicationDOA.getInstance().gameMode == GameMode.Single){
			result_time();
		}
		caloriesText ();
	}

	public void timeText(){
		Text time = GameObject.Find("TimeText").GetComponent<Text>();
		float timef = ApplicationDOA.getInstance().getTime();
		time.text = timef.ToString ("####0.00");
	}

	public void speedText(){
		Text speed = GameObject.Find ("SpeedNumber").GetComponent<Text> ();
		speed.text = MC.getSpeed ();
	}

	public void distanceText(){
		Text distance = GameObject.Find ("DistanceNumber").GetComponent<Text> ();
		Vector3 currentPosition = GameObject.Find ("Player").transform.position;
		distanceTravelled += Vector3.Distance (currentPosition, lastPosition) / 100;
		ApplicationDOA.getInstance ().set_distance (distanceTravelled);
		lastPosition = currentPosition;
		distance.text = distanceTravelled.ToString("####0.0 " + "M");
	}
	
	public void caloriesText(){
		Text calories = GameObject.Find ("CaloriesNumber").GetComponent<Text> ();
		float age = ApplicationDOA.getInstance ().get_age ();
		float weight = ApplicationDOA.getInstance ().get_weight ();
		float timef = ApplicationDOA.getInstance().getTime();
		float distance = ApplicationDOA.getInstance ().get_distance ();
		float k1 = 3.509F;
		float k2 = 0.2581F;
		float speed = float.Parse(MC.getSpeed());
	
		// double calories_burned = ((weight * 0.3) * distance) / 10;
		calories_burned += (((k1 * speed) + (k2 * Mathf.Pow(speed, 3))) / 69.78) / 60;
		ApplicationDOA.getInstance ().set_calories ((float)calories_burned);
		calories.text = calories_burned.ToString ("####0.0");
	}

	public void result_time(){
		Text time = GameObject.Find ("ResultTime").GetComponent<Text> ();
		float resultime = ApplicationDOA.getInstance().getTime();

		time.text = resultime.ToString ("####0.0");
	}

	public void result_calories(){
		Text calories = GameObject.Find ("ResultCalories").GetComponent<Text> ();
		float result_calories = ApplicationDOA.getInstance ().get_calories ();
		calories.text = result_calories.ToString ();
	}
}
