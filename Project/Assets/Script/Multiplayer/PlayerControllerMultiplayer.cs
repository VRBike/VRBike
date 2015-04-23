﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerMultiplayer : MonoBehaviour {

	Vector3 position = Vector3.zero;
	public float speed= 10;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;

	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private bool onStart;


	void Awake(){
		networkView.observed = this;
		lastSynchronizationTime = Time.time;
		syncStartPosition = transform.position;
		syncEndPosition = transform.position;
		onStart = true;
	}
	void Start () {
	
	}
	 
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 positionn = transform.position;

		Vector3 syncPosition = transform.position;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			stream.Serialize(ref positionn);

			syncPosition = transform.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
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
			Movement();		
		}
		else
		{
			SyncedMovement();
		}
	}
	void Movement(){
		if (Input.GetKey (KeyCode.D)) {
			
			transform.position += transform.right*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.A)) {
			
			transform.position -= transform.right*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.W)) {
			
			transform.position += transform.forward*Time.deltaTime*speed;
		}
		if (Input.GetKey (KeyCode.S)) {
			
			transform.position -= transform.forward*Time.deltaTime*speed;
		}
	}
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag =="PickUp"){

		}
		if (other.gameObject.name == "Event1") {
			Debug.Log("Trigger Event!!!");		
		}
	}

    


}
