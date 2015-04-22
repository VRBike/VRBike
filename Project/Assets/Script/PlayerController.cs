using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed= 0;
	public float angle = 0;
	NetworkScript MC;
	
	void Start(){
		MC = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkScript>();
		
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

		speed = float.Parse(MC.getSpeed ());
		angle += float.Parse (MC.getAxis())*Time.deltaTime*speed;
		Debug.Log (speed);
		transform.position += transform.forward*Time.deltaTime*speed;
		
		transform.eulerAngles = new Vector3(0,angle,0);
	}
}