﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerMultiplayer : MonoBehaviour {
	
	Vector3 position = Vector3.zero;	
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
		
	private Quaternion originalRotation;
	public string status = null;
	private Vector3 bornLocation = Vector3.zero;

	public float speed = 0;
	public float angle = 0;
	NetworkScript MC;

	int count=0;
	int count2=0;
	float tmpspeed =0;
	
	void Awake(){
		networkView.observed = this;
		lastSynchronizationTime = Time.time;
		syncStartPosition = transform.position;
		syncEndPosition = transform.position;
		
		bornLocation = transform.position;
		status = "stop";
		
		int num = Network.connections.Length;
		if (num == 0) {
			ChangeColorTo (new Vector3(1f, 0, 0));		
		}
		else if(num == 1){
			ChangeColorTo (new Vector3(0, 1f, 0));
		}
		else if(num == 2){
			ChangeColorTo (new Vector3(0, 0, 1f));
		}
		else if(num == 3){
			ChangeColorTo (new Vector3(0.5f, 0.5f, 0.5f));
		}
		
	}
	void Start () {
		originalRotation = transform.rotation;
		MC = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkScript>();
		speed = 0;
		count = 0;
	}
	 
	
	Vector3 rotateAngles = Vector3.zero;
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 positionn = transform.position;
		
		
		Vector3 syncPosition = transform.position;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			rotateAngles = transform.eulerAngles;
			
			stream.Serialize(ref rotateAngles);
			
			stream.Serialize(ref positionn);
			
			syncPosition = transform.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref rotateAngles);
			
			stream.Serialize(ref positionn);
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			position = positionn;
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = transform.position;
			
		}
	}
	
	// Update is called once per frame
	void Update(){
		
		if (networkView.isMine) {
			if(status == "run"){
				Movement();
			}
		}
		else
		{
			SyncedMovement();
		}
		
		if (Input.GetKey (KeyCode.E)){
			if(Network.isServer && status !="run"){
				networkView.RPC("SendStatus", RPCMode.AllBuffered, "run");	
			}
		}
		
		
	}
	void Movement(){
		if (Input.GetKey (KeyCode.D)) {
			
			angle += 5*Time.deltaTime*speed;
			//transform.position += transform.right*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.A)) {
			
			angle -= 5*Time.deltaTime*speed;
			//transform.position -= transform.right*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.W)) {
			
			transform.position += transform.forward*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.S)) {
			
			transform.position -= transform.forward*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.R)) {
			ReBorn();
		}
		
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
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		transform.eulerAngles = rotateAngles;
		rigidbody.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}	
	void ReBorn(){
		angle = 0;
		transform.position = bornLocation;
		syncStartPosition = bornLocation;
		syncEndPosition = bornLocation;		
	}	
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag =="Finish"){
			status = "finish";
			//Debug.Log("Number !!! = "+ other.gameObject.GetComponent<FinishScript>().GetNumber());
		}
	}
	public void SetStatus(string text){
		status = text;
	}
	
	[RPC] void SendStatus(string s){
		SetStatus (s);
	}
	
	[RPC] void ChangeColorTo(Vector3 color)
	{

		Renderer[] mats = gameObject.GetComponentsInChildren<Renderer> ();
		foreach(Renderer m in mats){
			m.material.color = new Color(color.x, color.y, color.z, 1f);
		}
		//renderer.material.color = new Color(color.x, color.y, color.z, 1f);
		
		
		if (networkView.isMine)
			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
	}
	
	
	
	
}
