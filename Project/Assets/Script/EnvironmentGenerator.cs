using UnityEngine;
using System.Collections;

public class EnvironmentGenerator : MonoBehaviour {

	public GameObject tree;
	public GameObject roadPlane;

	// Use this for initialization
	void Start () {
		GameObject t =(GameObject)Instantiate (tree);
		t.transform.position = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
