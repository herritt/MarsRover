using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RoverAIDay : MonoBehaviour
{
	private GameObject[] planes;
	private float offsetY=40;
	private float sizeX=100;
	private float sizeY=40;
	private static int score;
	private static int step;
	private bool Roverturn = false;
	private bool MovingStation = false;
	private int OrintationState = 4; //the direction of obstacle movement 1:left 2:up 3:right 4:down
	private bool CanSetOriginalPoint=true;
	private float raylenth = 6;
	private float speed =5;
	private bool ForwardOb = false;
	private bool LeftOb = false;
	private bool RightOb = false;
	private bool BackOb = false;
	private bool trap;
	private int trapcount;
	private float RotateSpeed = 25; 
	Vector3 OriginalPoint;
	private GameObject controller; //game controller to sent and recieve message
	RaycastHit forwardhit_CD;
	Ray forwardRay_CD;
	RaycastHit forwardhit_DAR;
	Ray forwardRay_DAR;
	RaycastHit forwardhit;
	Ray forwardRay; // ray that detects one grid ahead of the mars rover
	// detects if there is an obstacle move in front of the camera of mars rover
	private bool hasObstacleAhead1 = false; // first camera detection result
	private bool hasObstacleAhead2 = false; // second camera detection result
	private bool FirstDetection = false; // if it is the first time for camera to detect
	//private bool SecondDetection = false; // if it is the second time for camera to detect
	private Vector3 obstacle_position;
	private Vector3 current_obstacle_position;

	private bool showGUI = false;
	// Use this for initialization
	
	void Start () 
	{
		trap = false;
		trapcount = 0;
		score = 0;
		step = 0;
		obstacle_position =new Vector3 (0, 0, 0);
		current_obstacle_position =new Vector3 (0, 0, 0);
		//showGUI = true;

		OriginalPoint = transform.position;
		controller = GameObject.Find("Abstract"); //find the game controller to sent and recieve message
		planes = GameObject.FindGameObjectsWithTag ("plane");
		forwardRay = new Ray (transform.position, transform.forward);
		forwardRay_CD = new Ray (transform.position, transform.forward);
		forwardRay_DAR = new Ray (transform.position, transform.forward);
		detect_after_rotation ();
		detectforward ();
		cameraDetection();


	}
	
	void Update()
	{

	
		if (MovingStation) {

			moveforward(OriginalPoint);
		}
		else if (Roverturn) 
		{
			cameraDetection();
			if(trap==true)
			{
				trapturn();
			}
			else
			{
				detectforward();
				//cameraDetection();
				// if there is an obstacle one gird in front of the rover, turn to its right
				if (ForwardOb) 
				{
					transform.Rotate(new Vector3 (0, 90, 0));
                	//update forwardray
					forwardRay = new Ray (transform.position, transform.forward);
					forwardRay_CD = new Ray (transform.position, transform.forward);
					forwardRay_DAR = new Ray (transform.position, transform.forward);
					detect_after_rotation();
					FirstDetection = false;
					hasObstacleAhead1 = false;
					hasObstacleAhead2 = false;
					controller.SendMessage("SetRoverState");
					//cameraDetection();
				} 
				else 
				{	
					//cameraDetection();
					OriginalPoint = transform.position; //update the position after rover's move
					moveforward(OriginalPoint);
					step++;
				}
				cameraDetection();

				//foreach (GameObject plane in planes) {
					//plane.SendMessage("SetColorOn");
				//}
				Roverturn = false;
			}
		}
	}
	
	//function that detects all grids in front of the camera of mars rover
	/*
        lose condition : first detection - obstacle exists, second detection - obstacle gone 
                         first dection - obstacle does NOT exists, second detection - obstacle exists
    */
	void OnGUI()
	{
		if (showGUI) 
		{
			GUI.Box (new Rect (Screen.width/2-sizeX/2, offsetY/3, sizeX, sizeY),"score："+ score);
			GUI.Box (new Rect (Screen.width-sizeX, offsetY/3, sizeX, sizeY),"step："+ step);
			if (GUI.Button(new Rect(20, 20, 100, 40), "restart"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void detect_after_rotation()
	{
		obstacle_position = new Vector3(0, 0, 0);
		//forwardRay_DAR = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (forwardRay_DAR, out forwardhit_DAR, 500)) {
			if (forwardhit_DAR.collider.tag == "rockobstacle") {
				obstacle_position=forwardhit_DAR.point;
			}
			else
				obstacle_position =new Vector3 (0, 0, 0);
		} 
		//Debug.Log ("obstacle_position    "+obstacle_position);
	}

	
	//function that detects all grids in front of the camera of mars rover
	/*
        lose condition : first detection - obstacle exists, second detection - obstacle gone 
                         first dection - obstacle does NOT exists, second detection - obstacle exists
    */
	void cameraDetection()
	{

	
		forwardRay_CD = new Ray (transform.position, transform.forward);
		current_obstacle_position = new Vector3(0, 0, 0);
		if (Physics.Raycast (forwardRay_CD, out forwardhit_CD, 500)) 
		{//	Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!S");
			//current_obstacle_position = new Vector3(0, 0, 0);
			if (forwardhit_CD.collider.tag == "rockobstacle") 
			{
				current_obstacle_position = forwardhit_CD.point;

			}
			//else
			//	
		} 

		if (current_obstacle_position!=obstacle_position)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		//Debug.Log("current_obstacle_position="+current_obstacle_position);
		//Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!S");
	}
	
	// function that determines if there is an obstacle one grid ahead that forces the rover to turn
	void detectforward()
	{
		//Debug.Log("detect forward");
		forwardRay = new Ray (transform.position, transform.forward);
		if (Physics.Raycast(forwardRay, out forwardhit, raylenth)) 
		{
				//Debug.Log("detect ray");
			if (forwardhit.collider.tag == "rockobstacle") 
			{
				//Debug.Log("has obstacle");
				ForwardOb = true;
			}
		} 
		else
			ForwardOb = false;
	}
	void trapturn(){
		//force rover stay at its position 2 turn
		if (trapcount == 1) {
			trapcount = 0;
			trap = false;
			controller.SendMessage("SetRoverState");
			Roverturn = false;
		} 
		else {
			trapcount++;
			controller.SendMessage("SetRoverState");
			Roverturn = false;
		}
	
	}
	void moveforward(Vector3 OriginalPoint)
	{
		// if the rover has reached the desired position, stop and turn to player's move
		if (Vector3.Distance(transform.position, OriginalPoint) >= 5) 
		{
			MovingStation = false;	
			controller.SendMessage("SetRoverState");
		} 
		// otherwise, keep moving
		else 
		{
			MovingStation = true;
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			foreach (GameObject plane in planes) {
				plane.SendMessage("SetColorDown");
			}
		}
	}
	
	public void SetRoverState()
	{
		Roverturn = true;
		CanSetOriginalPoint=true;
		MovingStation=false;

	}
	
	public bool GetRoverState()
	{
		return Roverturn;
	}
	
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("111111111111");
		// if run out of board, player lose, restart game
		if (other.tag == "outboard_trigger") 
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		else if (other.tag == "red_heart") 
		{
			Destroy(other.gameObject);
			score=score+10;
		}
		else if (other.tag == "trap") 
		{
			trap=true;
		}
		//if win, jump to the next level
		else if (other.tag == "exit") 
		{
			//load next level
			

			int index = Application.loadedLevel;
			//increaselevel();
			index = index+1;
			string next_level = "Level" + index.ToString();
			Debug.Log(next_level);
			Application.LoadLevel(next_level); //load next level
			
		}
	
	}
	
	public void ShowGUI()
	{
		
		showGUI = true;
		
	}
	

}
