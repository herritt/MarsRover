using UnityEngine;
using System.Collections;

public class RoverAINight : MonoBehaviour
{
    private GameObject[] planes;//grids in the scene
    private float offsetY = 40;//the offset of gui
    private float sizeX = 100;//the size of gui
    private float sizeY = 40;
    private static int score;//record the score
    private static int turn;//record the step
    private bool Roverturn = false;
    private bool MovingStation = false;//if the rover have not get into next grid, it should move without detect anything
    private float raylenth = 5;
    private float speed = 5;
    private bool ForwardOb = false;//if detect the movement of forward obstacle ,restart this level
    private bool trap;//judge if the rover in the trap
    private int trapcount;//record the round which rover can not move
    Vector3 OriginalPoint;//record the position of rover before move,
    private GameObject controller; //game controller to sent and recieve message
    RaycastHit forwardhit;
    Ray forwardRay; // ray that detects one grid ahead of the mars rover

    // Use this for initialization
    void Start () 
    {
        trap = false;
	    trapcount = 0;
	    score = 0;//initialize score and turn with 0
        turn = 0;
	    OriginalPoint = transform.position;
	    controller = GameObject.Find("Abstract"); //find the game controller to sent and recieve message
	    planes = GameObject.FindGameObjectsWithTag ("plane");
	    forwardRay = new Ray (transform.position, transform.forward);
	    detectforward ();
    }
		
    void Update()
    {
        if (MovingStation) //if the rover have not get into next grid, it should move without detect anything
        {
            foreach (GameObject plane in planes)
            {
                plane.SendMessage("SetColorDown");
            }
            moveforward(OriginalPoint);
        }
        else if (Roverturn) 
        {
            if(trap==true)//if rover in trap,do nothing
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
				controller.SendMessage("SetRoverState");//end rover turn after rotation
                Roverturn = false;
			}
		}
	}
		
    void OnGUI()
	{
        GUI.Box (new Rect (Screen.width-4*sizeX/3, offsetY/2, sizeX, sizeY),"Score："+ score);
        GUI.Box(new Rect(Screen.width - 4*sizeX/3, 5*offsetY/2, sizeX, sizeY), "Turn：" + turn);
        if (GUI.Button(new Rect(20, 20, 100, 40), "restart"))
            Application.LoadLevel(Application.loadedLevel);
    }
		
	// function that determines if there is an obstacle one grid ahead that forces the rover to turn
	void detectforward()
	{
		forwardRay = new Ray (transform.position, transform.forward);
		if (Physics.Raycast(forwardRay, out forwardhit, raylenth)) 
		{
		    if (forwardhit.collider.tag == "rockobstacle") 
			{
				ForwardOb = true;
			}
		} 
		else
			ForwardOb = false;
	}

	void trapturn()
    {
		//force rover stay at its position 3 turn
		if (trapcount == 1)
        {
			trapcount = 0;
			trap = false;
			controller.SendMessage("SetRoverState");
			Roverturn = false;
		} 
		else
        {
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
		else if (other.tag == "red_heart") 
		{
			Destroy(other.gameObject);
			score = score + 100;
		}
		else if (other.tag == "trap") 
		{
			trap = true;
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
