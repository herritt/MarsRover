using UnityEngine;
using System.Collections;

public class TurnGameManager : MonoBehaviour
{
    //create a state machine to implement player round and rover round
    private GameObject obstacleController;//control all the rock
    private GameObject[] planes;//every grid in the scene
    private GameObject marsrover;
	private OperatorState mState=OperatorState.Player; //state machine which has two state:Player state and Rover state 
    private bool PlayerState;
	private bool RoverState;

    private int round;

	public enum OperatorState   //state machine which has three state:Player state and Rover state and quit
    {  
		RoverAI, 
		Player 
	}  
	
	void Start()   
	{ 
		planes = GameObject.FindGameObjectsWithTag("plane");//find every grid in the scene
        PlayerState =true;
		RoverState=true;
        marsrover = GameObject.Find("Rover");// find obstacle and rover in the scene
        obstacleController = GameObject.FindGameObjectWithTag("obstacleController");
        round = 1;
	}  

	void Update()   
	{  
        switch(mState)  
		{
            case OperatorState.Player:
                if (PlayerState==false)  
			    {
                    //if receive the messeage which player round is end
                    marsrover.SendMessage("SetRoverState");//active rover round
				    foreach (GameObject plane in planes) 
					    plane.SendMessage("SetColorDown");
					PlayerState = true;
					mState = OperatorState.RoverAI;  
				}  
			    break;
                  
			case OperatorState.RoverAI: 
		        if(RoverState==false)
                {
                    //if receive the messeage which rover round is end
                    obstacleController.SendMessage("SetPlayerState");//active player round
                    RoverState =true;
			        mState=OperatorState.Player;  
		        }  
				break;  
			}  
	}  

	public void SetPlayerState()
	{
        //if receive the messeage that player round is end,set rover state into true,get into rover round
        PlayerState = false;
		RoverState = true;
	}

	public void SetRoverState()
	{
        //if receive the messeage that rover round is end ,set player state into true,get into player round
        foreach (GameObject plane in planes) 
			plane.SendMessage("SetColorDown");
		RoverState = false;
		PlayerState = true;
        round++;
	}
}
