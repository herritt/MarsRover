using UnityEngine;
using System.Collections;

public class TurnGameManager : MonoBehaviour {

	//public Transform mPlayer;  
	//public Transform mRover;  
	
	//定义玩家及敌人脚本类  
	//private obstaclemovement playerScript;  
	//private RoverAI roverScript;  

	private OperatorState mState=OperatorState.Player;  
	private bool PlayerState=true;
	private bool RoverState=true;
	public enum OperatorState  
	{  
		Quit,  
		RoverAI, 
		Player 
	}  
	
	void Start ()   
	{  
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
					GameObject.Find("Rover").SendMessage("SetRoverState");
					PlayerState=true;
					mState=OperatorState.RoverAI;  
				}  
				break;  
			case OperatorState.RoverAI:  
			   if(RoverState==false)  
				{  
					GameObject.Find("rock1").SendMessage("SetPlayerState");
				    GameObject.Find("rock2").SendMessage("SetPlayerState");
					RoverState=true;
					mState=OperatorState.Player;  
				}  
				break;  
			}  
		  
	}  
	public void SetPlayerState()
	{
		PlayerState = false;
	}
	public void SetRoverState()
	{
		RoverState = false;
	}
}
