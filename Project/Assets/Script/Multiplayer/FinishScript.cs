using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour {
	private int number =0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int GetNumber(){
		return number;
	}
	public void ResetGame(){
		number = 0;
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag =="Player"){
			number++;
		}
	}


}
