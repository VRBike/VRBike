using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
		
public class ApplicationDOA{
		
	private static ApplicationDOA instance = null;
	private List<Vector3> generate_points_position = new List<Vector3>();
	private Boolean passed = false;
	private float Timer;

	public ApplicationDOA (){

	}

	public static ApplicationDOA getInstance(){
		if(instance == null) instance = new ApplicationDOA ();
		return instance;
	}

	public void set_generate_points_position(List<Vector3> position){
		this.generate_points_position = position;
	}

	public List<Vector3> get_generate_points_position(){
		return generate_points_position;
	}

	public void set_passedCheckpoint (Boolean passed){
		this.passed = passed; 
	}

	public Boolean get_passedCheckpoint(){
		return passed;
	}

	public void startTimer(){
		Timer += Time.deltaTime;
	}

	public float getTime (){
		return Timer;
	}
		
	public Boolean passedCheckpoint(){

		List<Vector3> list = ApplicationDOA.getInstance ().get_generate_points_position ();
		if (GameObject.Find ("Player").transform.position.z >= list [list.Count - 2].z)
			ApplicationDOA.getInstance().set_passedCheckpoint(true);

		return ApplicationDOA.getInstance().get_passedCheckpoint();
	}
}

