using UnityEngine;
using System.Collections;

public class RoverAINight : MonoBehaviour {
		private GameObject[] planes;
		private float offsetY=40;
		private float sizeX=100;
		private float sizeY=40;
		private static int score;
		private bool Roverturn = false;
		private bool MovingStation = false;
		private int OrintationState = 4; //the direction of obstacle movement 1:left 2:up 3:right 4:down
		private bool CanSetOriginalPoint=true;
		private float raylenth = 5;
		private float speed = 5;
		private bool ForwardOb = false;
		private bool LeftOb = false;
		private bool RightOb = false;
		private bool BackOb = false;
		private bool trap;
		private int trapcount;
		private float RotateSpeed = 25; 
		Vector3 OriginalPoint;
		private GameObject controller; //game controller to sent and recieve message
		RaycastHit forwardhit;
		Ray forwardRay; // ray that detects one grid ahead of the mars rover
		// Use this for initialization
		void Start () 
		{
			trap = false;
			trapcount = 0;
			score = 0;
			OriginalPoint = transform.position;
			controller = GameObject.Find("Abstract"); //find the game controller to sent and recieve message
			planes = GameObject.FindGameObjectsWithTag ("plane");
			forwardRay = new Ray (transform.position, transform.forward);
			detectforward ();
		}
		
		void Update()
		{
			
			if (MovingStation) {
				foreach (GameObject plane in planes) {
					plane.SendMessage("SetColorDown");
				}
				moveforward(OriginalPoint);
			}
			else if (Roverturn) 
			{
				if(trap==true)
				{
					trapturn();
				}
				else
				{
					detectforward();
					// if there is an obstacle one gird in front of the rover, turn to its right
					if (ForwardOb) 
					{
						transform.Rotate(new Vector3 (0, 90, 0));
						forwardRay = new Ray (transform.position, transform.forward);
					} 
					else 
					{	
						OriginalPoint = transform.position; //update the position after rover's move
						moveforward(OriginalPoint);
					}
					controller.SendMessage("SetRoverState");
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
			GUI.Box (new Rect (Screen.width/2-sizeX/2, offsetY/3, sizeX, sizeY),"score："+ score);
		}
		
		// function that determines if there is an obstacle one grid ahead that forces the rover to turn
		void detectforward()
		{
		Debug.Log("detect");
			forwardRay = new Ray (transform.position, transform.forward);
			if (Physics.Raycast(forwardRay, out forwardhit, raylenth)) 
			{
				if (forwardhit.collider.tag == "rockobstacle") 
				{
				Debug.Log("wofaxianle");
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
			} 
			// otherwise, keep moving
			else 
			{
				MovingStation = true;
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
		
		
}
