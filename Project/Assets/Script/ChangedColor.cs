using UnityEngine;
using System.Collections;

public class ChangedColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeColor(){
		if(GameObject.Find ("Sphere").renderer.material.color == Color.green) GameObject.Find ("Sphere").renderer.material.color = Color.red;
		else GameObject.Find ("Sphere").renderer.material.color = Color.green;
	}
}
