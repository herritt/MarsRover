using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RoverAIDay : MonoBehaviour
{
	private GameObject[] planes;//grids in the scene
    private float offsetY = 40;//the offset of gui
    private float sizeX = 100;//the size of gui
    private float sizeY = 40;
	private static int score;//record the score
    private static int turn;//record the step
    private bool Roverturn = false;
	private bool MovingStation = false;//if the rover have not get into next grid, it should move without detect anything
    private float raylenth = 6;
	private float speed =5;
	private bool ForwardOb = false;//if detect the movement of forward obstacle ,restart this level
    private bool trap;//judge if the rover in the trap
    private int trapcount;//record the round which rover can not move
    Vector3 OriginalPoint;//record the position of rover before move,
	private GameObject controller; //game controller to sent and recieve message
	RaycastHit forwardhit_CD;// ray that detects all the obstacle in the line every time
    Ray forwardRay_CD;
	RaycastHit forwardhit_DAR;// ray that detects the obstacle after rotate
    Ray forwardRay_DAR;
	RaycastHit forwardhit;
	Ray forwardRay; // ray that detects one grid ahead of the mars rover
	private Vector3 obstacle_position;
	private Vector3 current_obstacle_position;  // update obstacle position everytime,judge if any obstacle has moved

    void Start () 
	{
		trap = false;
		trapcount = 0;
		score = 0;//initialize score and turn with 0
		turn = 0;
		obstacle_position =new Vector3 (0, 0, 0);//initialize start position of rover
        current_obstacle_position =new Vector3 (0, 0, 0);

        OriginalPoint = transform.position;//initialize start position of rover
        controller = GameObject.Find("Abstract"); //find the game controller to sent and recieve message
		planes = GameObject.FindGameObjectsWithTag ("plane");
		forwardRay = new Ray (transform.position, transform.forward);
		forwardRay_CD = new Ray (transform.position, transform.forward);
		forwardRay_DAR = new Ray (transform.position, transform.forward);
		detect_after_rotation ();//initialise position of obstacle
        detectforward ();
		cameraDetection();

	}
	
	void Update()
	{

        if (MovingStation)  //if the rover have not get into next grid, it should move without detect anything
            moveforward(OriginalPoint);
		else if (Roverturn) 
		{
			cameraDetection();// detects all the obstacle in the line every time
            if (trap==true)
				trapturn();//if rover in trap,do nothing
            else
			{
				detectforward();
				// if there is an obstacle one gird in front of the rover, turn to its right
				if (ForwardOb) 
				{
					transform.Rotate(new Vector3 (0, 90, 0));
					forwardRay = new Ray (transform.position, transform.forward);// update ray direction after rotation
                    forwardRay_CD = new Ray (transform.position, transform.forward);
					forwardRay_DAR = new Ray (transform.position, transform.forward);
					detect_after_rotation();//update the obstacle in front of the rover
					controller.SendMessage("SetRoverState");//end rover turn after rotation
				}
                else // if there is no obstacle ,move forword
                {	
					OriginalPoint = transform.position; //update the position after rover's move
                    moveforward(OriginalPoint);//move forword
				}
				cameraDetection();//detect again because rover has changed the rotation
                Roverturn = false;
			}
		}
	}
    //the GUI of score and step
    void OnGUI()
	{
        GUI.Box(new Rect(Screen.width - 4 * sizeX / 3, offsetY / 2, sizeX, sizeY), "Score：" + score);
        GUI.Box(new Rect(Screen.width - 4 * sizeX / 3, 5 * offsetY / 2, sizeX, sizeY), "Turn：" + turn);
        if (GUI.Button(new Rect(20, 20, 100, 40), "restart"))
            Application.LoadLevel(Application.loadedLevel);
	}

	void detect_after_rotation()
	{
		obstacle_position = new Vector3(0, 0, 0);
		if (Physics.Raycast (forwardRay_DAR, out forwardhit_DAR, 500))
        {
            if (forwardhit_DAR.collider.tag == "rockobstacle")
                obstacle_position = forwardhit_DAR.point;
            else
                obstacle_position = new Vector3(0, 0, 0);
		} 
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
		{
			if (forwardhit_CD.collider.tag == "rockobstacle") 
			{
				current_obstacle_position = forwardhit_CD.point;//record the position of obstacle in front of rover,update every time
			}
		} 

		if (current_obstacle_position!=obstacle_position)
		{
			Application.LoadLevel(Application.loadedLevel);//if the current position of obstacle is not equal to recorded position,Game over.
		}
	}
	
	// function that determines if there is an obstacle one grid ahead that forces the rover to turn
	void detectforward()
	{
		forwardRay = new Ray (transform.position, transform.forward);
		if (Physics.Raycast(forwardRay, out forwardhit, raylenth)) 
		{
			if (forwardhit.collider.tag == "rockobstacle") 
				ForwardOb = true;//if there is a obstacle on the next square of rover route,turn right.
            else
                ForwardOb = false;
        } 
		else
			ForwardOb = false;
	}

	void trapturn()
    {
		//force rover stay at its position 3 turn
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
	
	public void SetRoverState()//receive message from TurnGameManager,to active rover in rover turn
	{
		Roverturn = true;
		MovingStation = false;
        turn++;
    }
	
	public bool GetRoverState()
	{
		return Roverturn;
	}
	
	void OnTriggerEnter(Collider other)
	{
		// if run out of board, player lose, restart game
		if (other.tag == "outboard_trigger") 
		{
			Application.LoadLevel(Application.loadedLevel);
		}
        // if hit a star, collect 100 points
		else if (other.tag == "red_heart") 
		{
			Destroy(other.gameObject);
			score = score + 100;
		}
		else if (other.tag == "trap") 
		{
			trap=true;
		}
		//if win, jump to the next level
		else if (other.tag == "exit") 
		{
            //get current level index
            int index = Application.loadedLevel;
            // if player finishes all levels, go back to title page
            if (index == 26)
            {
                Application.LoadLevel("TitleScreen");
            }
            //load next level	
            else
            {
                string next_level = "Level" + index.ToString();
                Application.LoadLevel(next_level);
            }
		}
	}
	
	
}
