using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
	
public enum GameState{MainMenu, Ready, Playing, Paused, Finish};
public delegate void OnStateChangeHandler();

public enum GameMode{MainMenu, Single, Multi};

public class ApplicationDOA{
		
	private static ApplicationDOA instance = null;
	private List<Vector3> generate_points_position = new List<Vector3>();
	private Boolean passed = false;
	private float Timer;
	private Vector3 startPosition;
	private float age;
	private float weight;
	private float distance;
	private Boolean finish_game = false;
	private float calories;
	
	public event OnStateChangeHandler OnStateChange;

	public ApplicationDOA (){

	}

	public static ApplicationDOA getInstance(){
		if(instance == null) instance = new ApplicationDOA ();
		return instance;
	}

	public GameMode gameMode{ get; private set;}

	public void SetGameMode(GameMode gameMode){
		this.gameMode = gameMode;
	}

	public GameState gameState{ get; private set;}

	public void SetGameState(GameState gameState){
		this.gameState = gameState;
		if(OnStateChange != null) {
			OnStateChange();
		}
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

	public void endTimer(){
		Timer = 0;
	}

	public float getTime (){
		return Timer;
	}

	public void set_start_position(Vector3 position){
		this.startPosition = position;
	}

	public Vector3 get_start_position(){
		return startPosition;
	}

	public void set_age(float age){
		this.age = age;
	}

	public float get_age(){
		return age;
	}

	public void set_weight(float weight){
		this.weight = weight;
	}

	public float get_weight(){
		return weight;
	}

	public void set_distance(float distance){
		this.distance = distance;
	}

	public float get_distance(){
		return distance;
	}

	public void set_calories(float calories){
		this.calories = calories;
	}

	public float get_calories(){
		return calories;
	}
}

