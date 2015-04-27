using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplayerController : MonoBehaviour {

	private const string typeName ="VRBike_Network_Test";
	private string gameName = "Room_Name";
	public GameObject stage;
	public GameObject player;
	public GameObject finish;

	public GameObject road;
	
	private HostData[] hostList;
	
	public GameObject serverListPanel;
	public GameObject serverNameButton;
	
	public Material mat1;
	public Material mat2;

	public GameObject PauseCanvas;
	public GameObject AdjustCanvas;
	public GameObject FinishCanvas;
	public GameObject Interface;


	void Start () {
		stage.SetActive (false);
		road.SetActive (false);
		PauseCanvas.SetActive (false);
		AdjustCanvas.SetActive (false);
		FinishCanvas.SetActive (false);
		Interface.SetActive (false);
	}	

	// Update is called once per frame
	void Update () {
		
	}

	public void StartServer(Text serverName){
		//gameName = serverName.text;
		gameName = "VRBike";
		Network.InitializeServer (4, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (typeName,gameName);
		GameObject.Find("Canvas").SetActive(false);
		//stage.SetActive (true);
		
	}
	void OnServerInitialized()
	{
		Debug.Log ("Create Server Completed");
		//statusText.text = "Create Server Completed";
		CreateStage ();
		Network.Instantiate (finish,new Vector3(-75.5f,1f,90f),Quaternion.identity, 0);
		SpawnPlayer ();
	}

	void CreateStage(){

		//Network.Instantiate (stage,new Vector3(0f,0.5f,0f),Quaternion.identity, 0);
		ApplicationDOA.getInstance ().SetGameState (GameState.Playing);
		RenderSettings.skybox = mat2;
		stage.SetActive (true);
		road.SetActive (true);
		PauseCanvas.SetActive (true);
		AdjustCanvas.SetActive(true);
		ApplicationDOA.getInstance ().SetGameState (GameState.Ready);

		Interface.SetActive (true);
	}
	public void RefreshHostList(){
		MasterServer.RequestHostList (typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent){

		if (msEvent == MasterServerEvent.HostListReceived) {
				Debug.Log ("Found Server!!");
				hostList = MasterServer.PollHostList ();
				//statusText.text = "Found Server!!  " + hostList.Length;
				ListServer ();
			} else {
				Debug.Log ("Server Not Found !!");
				//statusText.text = "Server Not Found !!";
		}

	}
	private void ListServer(){
		foreach(GameObject child in GameObject.FindGameObjectsWithTag("ServerName")){
			Destroy(child);
		}
		
		foreach (HostData hd in hostList) {
			Object tmp = Instantiate(serverNameButton);
			GameObject go = (GameObject)tmp;
			go.GetComponentInChildren<Text>().text = hd.gameName;
			go.transform.parent = serverListPanel.transform;
			go.transform.localScale = new Vector3(1,1,1);
			go.transform.localPosition = new Vector3(0,0,0);
		}
	}
	public void JoinServer(Text serverName){
		Debug.Log (serverName.text);
		foreach (HostData hd in hostList) {
			if(hd.gameName == serverName.text){
				Debug.Log (hd.gameName);
				Network.Connect(hd);
				GameObject.Find("Canvas").SetActive(false);

			}
		}
		
	}
	void OnConnectedToServer(){
		//Debug.Log ("Join Server Complete!!!");
		//statusText.text = "Join Server Complete!!!";
		//stage.SetActive (true);
		//road.SetActive (true);

		CreateStage ();
		SpawnPlayer ();
		
	}
	void SpawnPlayer(){
		int num = Network.connections.Length;
		if (num == 0) {
			Network.Instantiate(player,new Vector3(-3,2f,0f),Quaternion.identity, 0);	
		}
		else if(num == 1){
			Network.Instantiate(player,new Vector3(-1,2f,0f),Quaternion.identity, 0);
		}
		else if(num == 2){
			Network.Instantiate(player,new Vector3(1,2f,0f),Quaternion.identity, 0);
		}
		else if(num == 3){
			Network.Instantiate(player,new Vector3(3,2f,0f),Quaternion.identity, 0);
		}
		//Network.Instantiate(player,new Vector3((float)(0+(num*5)),2f,0f),Quaternion.identity, 0);
		GameObject.Find ("CardboardMain").GetComponent<MultiplayerCameraController>().GameStart(num);
		GameObject.Find ("ApplicationMaster").GetComponent<MultiPlayerApplicationControl>().SetInGame(true);
	}


}
