using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
	
	Vector3 posStart = new Vector3 (0, 0, 0);
	GameObject start;
	GameObject finish;
	List<Vector3> generate_points_position;

	int num_of_points = 0;
	
	// Use this for initialization
	void Start () {

		//generateStart();
		//posStart = start.transform.position;
		posStart = GameObject.Find ("Player").transform.position;;
		posStart.y = 0.1F;
		generate_points_position = new List<Vector3>();
		generate_points_position.Add (posStart);
		num_of_points = Random.Range (4, 8);
		//generateFinish ();
		generatePointBetween (num_of_points);
	}

	void Update(){

		if (ApplicationDOA.getInstance ().passedCheckpoint ()) {
			num_of_points = Random.Range (10, 20);
			generatePointBetween(num_of_points);
			ApplicationDOA.getInstance().set_passedCheckpoint(false);
		}
	}

	/*public void generateStart(){
		start = GameObject.CreatePrimitive(PrimitiveType.Cube);
		start.renderer.material.color = Color.red;
		//cube.AddComponent<Rigidbody>();
		start.transform.position = new Vector3(pos.x, pos.y, pos.z);
		start.transform.localScale += new Vector3 (5, 5, 5);
	}*/

	public void generatePointBetween(int num_of_points){
		//Vector3 posFinish = finish.transform.position;

		for (int i = 0; i < num_of_points; i++) {

			/*float randx = Random.Range(posStart.x - 50, posStart.x + 50);
			float randz = Random.Range(oldPos.z + 50, oldPos.z + 100);

			newPos.x = randx;
			newPos.z = randz;*/

			if(i == 0) generate_points_position.Add(new Vector3(10 * posStart.x, 0, i));
			else generate_points_position.Add(10 * new Vector3(UnityEngine.Random.value, 0, i));

		}
		ApplicationDOA.getInstance ().set_generate_points_position (generate_points_position);
		renderRoad ();
	}

	/*public void generateFinish(){
		finish = GameObject.CreatePrimitive(PrimitiveType.Cube);
		finish.renderer.material.color = Color.green;
		//cube.AddComponent<Rigidbody>();
		finish.transform.position = new Vector3(Random.Range(pos.x - 100, pos.x + 100), pos.y, Random.Range(pos.z + 100, pos.z + 200));
		finish.transform.localScale += new Vector3 (5, 5, 5);
	}*/

	public void renderRoad(){

		/*var points = new List<Vector3>();
		for (int i = 0; i < 10; i++)
		{
			if(i == 0 ) points.Add(20 * new Vector3(posStart.x, 0, posStart.z));
			else points.Add(20 * new Vector3(UnityEngine.Random.value, 0, i));
		}
		
		var splineRenderer = SplineRenderer.FindInstance();
		splineRenderer.AddLine(this, new Line(points, Color.green, width: 1f, segments: 10));*/

		SplineRenderer splineRenderer = SplineRenderer.FindInstance();
		splineRenderer.AddLine(this, new Line(ApplicationDOA.getInstance().get_generate_points_position(), Color.green, width: 1f, segments: 10));
	}

	/*public void renderRoad(Vector3 oldPos, Vector3 newPos){

		//Vector3 direction = newPos - oldPos;
		//float distance = Vector3.Distance (oldPos, newPos);

		GameObject road = GameObject.CreatePrimitive (PrimitiveType.Plane);
		//Instantiate (roads);
		road.transform.rotation = Quaternion.LookRotation (direction);
		road.renderer.material.color = Color.black;
		road.transform.position = oldPos + (direction / 2);
		road.transform.localScale = new Vector3 (3, 1, direction.magnitude / 8.8F);

		var points = new List<Vector3>();
		for (int i = 0; i < 10; i++)
		{
			points.Add(20 * new Vector3(i, 0, UnityEngine.Random.value));
		}

		var splineRenderer = SplineRenderer.FindInstance();
		splineRenderer.AddLine(this, new Line(points, Color.green, width: 1f, segments: 10));
	}*/
}
