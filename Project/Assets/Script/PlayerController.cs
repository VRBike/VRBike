using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
	
	public float speed = 0;
	public float angle = 0;
	NetworkScript MC;

	int count=0;
	int count2=0;
	float tmpspeed =0;

	void Start(){
		ApplicationDOA.getInstance ().set_age (24);
		ApplicationDOA.getInstance ().set_weight (65);
		MC = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkScript>();
		speed = 0;
		count = 0;
	}
	
	void FixedUpdate(){

		//if (Input.GetKey (KeyCode.D)) {
		
		//	transform.position += transform.right*Time.deltaTime*speed;
		//}
		//if (Input.GetKey (KeyCode.A)) {
		
		//	transform.position -= transform.right*Time.deltaTime*speed;
		//}
		//if (Input.GetKey (KeyCode.W)) {
		
		//	transform.position += transform.forward*Time.deltaTime*speed;
		//}
		//if (Input.GetKey (KeyCode.S)) {
		
		//	transform.position -= transform.forward*Time.deltaTime*speed;
		//}
		
		//if (Input.GetKey (KeyCode.Q)) {
		//	angle -= 50*Time.deltaTime*speed;
		//}
		//if (Input.GetKey (KeyCode.E)) {
		//	angle += 50*Time.deltaTime*speed;	
		//}
		
		//transform.eulerAngles = new Vector3(0,angle,0);
		Movement();


		
	}
	void Movement(){
		speed = 0;
		try{
			
			speed = float.Parse(MC.getSpeed ());
			angle += float.Parse(MC.getAxis())*Time.deltaTime;
		}
		catch(Exception e){			
		}
		//Debug.Log (speed);
		if (count == 15) {
			count =0;
			
			
			if(count2 == 3){
				speed =0;
				MC.setSpeed("0");
				count2=0;
			}
			else if(tmpspeed==speed){
				count2++;
			}
			else{
				tmpspeed=speed;
			}
			
		}
		transform.position += transform.forward*Time.deltaTime*speed;
		if (speed != 0) {
			transform.eulerAngles = new Vector3 (0, angle, 0);
		}
		count++;
	}
}