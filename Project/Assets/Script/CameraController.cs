using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject target = null;
	
	private Vector3 positionOffset = Vector3.zero;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		positionOffset = target.transform.position + new Vector3(0,0.5f,0);
	}


	// Update is called once per frame
	void Update () {
		transform.position = target.transform.position + positionOffset;
		transform.rotation = target.transform.rotation;
	}
}
