using UnityEngine;
using System.Collections;

public class obstaclemovement : MonoBehaviour {
	private int i;
	private bool Playerturn = true;
	private float speed =5;
	private float offset;
	private float temp;
	private Vector3 targetpoint;//the point which the obstacle want to move
	private bool HasSelected=false;//if the obstacle has been clicked
	private bool HasSelectedpoint=false; //the obstacle can only move once
	private bool Ismoveableplane=true;
	private GameObject square;
	private GameObject[] Squares;
	private int orintation=0;//the direction of obstacle movement 1:left 2:up 3:right 4:down
	// Update is called once per frame
	private GameObject selected; //Selected obstacle 
	private GameObject[] planes;
	void Start()
	{
		planes = GameObject.FindGameObjectsWithTag ("plane");
		selected = null;
	}

	void Update () 
	{
		if (Playerturn == true) 
		{

			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hitInfo, 1000)) 
			{
				if(Input.GetMouseButtonDown (0))
				{
					if (hitInfo.transform.tag == "rockobstacle")
					{  
						//Debug.Log ("jinlaile hit");
						selected = GameObject.Find(hitInfo.collider.gameObject.name); // selected obejct
						HasSelected = true;//select the rock
						ShowSquarescolor();
					}
					else if (HasSelected == true) 
					{
						square = hitInfo.transform.gameObject;
						Canmovetothatsquare(square);
						//Debug.Log ("jinlaile hit");
						//Debug.Log(hitInfo.transform.tag);		
						if (hitInfo.transform.tag == "plane" && Ismoveableplane == true) 
						{
							if (HasSelectedpoint == false) 
							{
								//if it is the first time to choose square
								targetpoint = hitInfo.point;
								targetpoint.y = selected.transform.position.y;
								SelectMoveDirection(targetpoint);
								//Debug.Log("zhenidoumeijinlai");
							}
						}
					}
					else 
					{
						HasSelected=false;
					}
				}
					/*

					*/
				/*
				if (hitInfo.transform.tag == "rockobstacle" && Input.GetMouseButtonDown (0))
				{  
					//Debug.Log ("jinlaile hit");
					selected = GameObject.Find(hitInfo.collider.gameObject.name); // selected obejct
					HasSelected = true;//select the rock
					ShowSquarescolor();
				}
					
				else if (HasSelected == true && Input.GetMouseButtonDown (0)) {
					square = hitInfo.transform.gameObject;
					Canmovetothatsquare(square);
					//Debug.Log ("jinlaile hit");
					//Debug.Log(hitInfo.transform.tag);		
					if (hitInfo.transform.tag == "plane" && Ismoveableplane == true) {
						if (HasSelectedpoint == false) {//if it is the first time to choose square
							targetpoint = hitInfo.point;
							targetpoint.y = selected.transform.position.y;
							SelectMoveDirection(targetpoint);
							//Debug.Log("zhenidoumeijinlai");
						}
					}
				}

				*/
				//当射线碰撞到plane并且鼠标左键按下时

			}

			if (HasSelected == true) {
				//Debug.Log ("jinlaile switch");
				switch (orintation) {
				case 0:

					break;
				case 1:
					targetpoint.x = temp;
					if (targetpoint.x < selected.transform.position.x) 
					{
					//if (targetpoint.x < selected.position.x) {
						selected.transform.Translate (Vector3.left * speed * Time.deltaTime);

					} 
					else if (targetpoint.x >= selected.transform.position.x) 
					{
						//transform.position.Set (targetpoint.x, targetpoint.y, targetpoint.z);
						//Debug.Log ("jinlailema");
						GameObject.Find("Abstract").SendMessage("SetPlayerState");
						ShowSquarescolor();
						Playerturn=false;
					}
					break;
				case 2:
					targetpoint.z = temp;
					if (targetpoint.z < selected.transform.position.z) {
						selected.transform.Translate (Vector3.up * speed * Time.deltaTime);
					}
					else if (targetpoint.z >= selected.transform.position.z) {
						//transform.position.Set (targetpoint.x, targetpoint.y, targetpoint.z);
						//Debug.Log ("obstacle end");
						GameObject.Find("Abstract").SendMessage("SetPlayerState");
						ShowSquarescolor();
						ShowSquarescolor();
						Playerturn=false;
					}
					break;
				case 3:
					targetpoint.x = temp;
					if (targetpoint.x > selected.transform.position.x) {
						selected.transform.Translate (Vector3.right * speed * Time.deltaTime);
					}
					else if (targetpoint.x <= selected.transform.position.x) {
						//transform.position.Set (targetpoint.x, targetpoint.y, targetpoint.z);
						GameObject.Find("Abstract").SendMessage("SetPlayerState");
						ShowSquarescolor();
						//GameObject.FindGameObjectWithTag("plane").SendMessage("SetColorDown");
						Playerturn=false;
					}
					break;
				case 4:
					targetpoint.z = temp;
					if (targetpoint.z > selected.transform.position.z) {
						selected.transform.Translate (Vector3.down * speed * Time.deltaTime);
					}
					else if (targetpoint.z <= selected.transform.position.z) {
						//transform.position.Set (targetpoint.x, targetpoint.y, targetpoint.z);
						GameObject.Find("Abstract").SendMessage("SetPlayerState");
						ShowSquarescolor();
						Playerturn=false;
					}
					break;
				default:

					break;
				}
			}
		}
	}
	void SelectMoveDirection(Vector3 targetpoint)
	{
		HasSelectedpoint = true;
		if (targetpoint.x < selected.transform.position.x - 2.5) {
			targetpoint.x=selected.transform.position.x - 5;
			temp=selected.transform.position.x - 5;
			targetpoint.z = selected.transform.position.z;
			orintation = 1;
		} else if (targetpoint.x > selected.transform.position.x + 2.5) {
			targetpoint.x=selected.transform.position.x + 5;
			temp=selected.transform.position.x + 5;
			targetpoint.z = selected.transform.position.z;
			orintation = 3;
		} else if (targetpoint.z < selected.transform.position.z - 2.5) {
			targetpoint.z=selected.transform.position.z - 5;
			temp=selected.transform.position.z - 5;
			targetpoint.x = selected.transform.position.x;
			orintation = 2;
		} else if (targetpoint.z >selected.transform.position.z + 2.5) {
			targetpoint.z=selected.transform.position.z + 5;
			temp=selected.transform.position.z + 5;
			targetpoint.x = selected.transform.position.x;
			orintation = 4;
		}
		//Debug.Log (orintation);
	}
	void Canmovetothatsquare(GameObject square)
	{
		//Debug.Log ("Canmovetothatsquare");
		if (square.transform.position.x < selected.transform.position.x - 2.5 && square.transform.position.z > selected.transform.position.z + 2.5)
			Ismoveableplane = false;
		else if (square.transform.position.x > selected.transform.position.x + 2.5 && square.transform.position.z > selected.transform.position.z + 2.5)
			Ismoveableplane=false;
		else if(square.transform.position.x<selected.transform.position.x-2.5&&square.transform.position.z<selected.transform.position.z-2.5)
			Ismoveableplane=false;
		else if(square.transform.position.x>selected.transform.position.x+2.5&&square.transform.position.z<selected.transform.position.z-2.5)
			Ismoveableplane=false;
		else
			Ismoveableplane=true;
	}
	public void SetPlayerState()
	{
		Playerturn = true;
		HasSelected=false;//if the obstacle has been clicked
		HasSelectedpoint=false; //the obstacle can only move once
		Ismoveableplane=true;
		orintation = 0;

	}
	void OnGUI()
	{
		if (GUI.Button (new Rect (20, 100, 100, 40), "skip turn")) {
			skipturn ();
		}
	}
	void skipturn ()
	{
		GameObject.Find("Abstract").SendMessage("SetPlayerState");
		Playerturn=false;
	}
	public bool GetPlayerState()
	{
		return Playerturn;
	}

	void ShowSquarescolor()
	{
		foreach (GameObject plane in planes) {
			plane.SendMessage("findobstacle");
		}
	}


}
