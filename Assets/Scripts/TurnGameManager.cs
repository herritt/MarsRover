using UnityEngine;
using System.Collections;

public class TurnGameManager : MonoBehaviour {

	//public Transform mPlayer;  
	//public Transform mRover;  
	
	//定义玩家及敌人脚本类  
	//private obstaclemovement playerScript;  
	//private RoverAI roverScript;  
	private GameObject[] obstacles;
	private GameObject[] planes;
	private GameObject marsrover;
	private OperatorState mState=OperatorState.Player;  
	private bool PlayerState;
	private bool RoverState;
	public enum OperatorState  
	{  
		Quit,  
		RoverAI, 
		Player 
	}  
	
	void Start ()   
	{ 
		planes=GameObject.FindGameObjectsWithTag("plane");
		PlayerState=true;
		RoverState=true;
		if (obstacles == null) {
			obstacles = GameObject.FindGameObjectsWithTag ("rockobstacle");
			marsrover = GameObject.Find ("Rover");
		}
		//获取玩家及敌人脚本类  
		//playerScript=mPlayer.GetComponent<obstaclemovement>();  
		//roverScript=mRover.GetComponent<RoverAI>();  
	}  



	void Update ()   
	{  

			switch(mState)  
			{  
			case OperatorState.Player:  


			   if(PlayerState==false)  
			   { 	

				 // Debug.Log("transform from obstacle to rover");
					marsrover.SendMessage("SetRoverState");
				    Debug.Log ("控制台把控制权交还给rover");
				foreach (GameObject plane in planes) {
					plane.SendMessage("SetColorDown");
				}
					PlayerState=true;
					mState=OperatorState.RoverAI;  
				}  
				break;  
			case OperatorState.RoverAI:  
		
			   if(RoverState==false)  
				{  

			//	Debug.Log("transform from rover to obstacle");
					foreach (GameObject obstacle in obstacles) {
						obstacle.SendMessage("SetPlayerState");
					}
				Debug.Log ("控制台把控制权交还给player");
					RoverState=true;
					mState=OperatorState.Player;  
				}  
				break;  
			}  
		  
	}  
	public void SetPlayerState()
	{
		Debug.Log ("player把控制权交还给控制台");
		PlayerState = false;
		RoverState = true;
	}
	public void SetRoverState()
	{
		Debug.Log ("rover把控制权交还给控制台");
		foreach (GameObject plane in planes) {
			plane.SendMessage("SetColorDown");
		}
		RoverState = false;
		PlayerState = true;
	}
}
