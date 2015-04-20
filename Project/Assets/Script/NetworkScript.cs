using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class NetworkScript : MonoBehaviour {

	Thread receiveThread;
	UdpClient client;
	public int port;
	public bool running;
	
	public string inputText = "0:0";
	public string speed ="0";
	public string axis="0";

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	void OnApplicationQuit () {
		
		running =false;
		if (receiveThread != null)
			receiveThread.Abort();
		try{
			client.Close();
		}
		catch(Exception err){			
		}
	}
	void OnDisable() 
	{ 
		running =false;
		if ( receiveThread!= null) 
			receiveThread.Abort(); 
		try{
			client.Close(); 
		}catch(Exception err){
			
		}
	} 
	void Start () {
		running = true;
		Debug.Log ("Start Server");
		receiveThread = new Thread(
			new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
		//Application.LoadLevel ("SinglePlayer");
	}	
	// Update is called once per frame
	void Update () {
		
	}
	private  void ReceiveData()
	{
		client = new UdpClient(8000);
		while (running)
		{			
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);				
				
				string text = Encoding.UTF8.GetString(data);
				Debug.Log("Input "+text);
				string[] info = text.Split(':');
				
				inputText = text;
				
				if(info[0].Equals("speed")){
					speed= info[1];
				}
				if(info[0].Equals("turn")){
					axis= info[1];
				}
				
				
			}
			catch (Exception err)
			{
				running =false;
				print(err.ToString());
			}
		}
	}
	public string getInputText(){
		return inputText;
	}
	public string getSpeed(){
		return speed;
	}
	public string getAxis(){
		return axis;
	}
	public UdpClient getClient(){
		return client;
	}

}
