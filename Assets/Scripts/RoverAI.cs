using UnityEngine;
using System.Collections;

public class RoverAI : MonoBehaviour {
	private bool Roverturn=false;
	private bool MovingStation=false;
	private int OrintationState=4;
	private bool CanSetOriginalPoint=true;
	public float raylenth=10;
	public float speed=5;
	private bool ForwardOb=false;
	private bool LeftOb=false;
	private bool RightOb=false;
	private bool BackOb=false;
	private float RotateSpeed=25;
	Vector3 OriginalPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (MovingStation) {
			moveforward (OriginalPoint);
		} else if (Roverturn == true) {
			//Debug.DrawRay(transform.position, Vector3.forward*raylenth);
			RaycastHit forwardhit;
			Ray forwardlandingRay = new Ray (transform.position, Vector3.forward);
			if (Physics.Raycast (forwardlandingRay, out forwardhit, raylenth)) {				
				if (forwardhit.collider.tag == "obstacle") {
					ForwardOb = true;
				}	
			}
			RaycastHit lefthit;
			Ray leftlandingRay = new Ray (transform.position, Vector3.left);
			if (Physics.Raycast (leftlandingRay, out lefthit, raylenth)) {				
				if (lefthit.collider.tag == "obstacle") {
					LeftOb = true;
				}	
			}
			RaycastHit righthit;
			Ray rightlandingRay = new Ray (transform.position, Vector3.right);
			if (Physics.Raycast (rightlandingRay, out righthit, raylenth)) {				
				if (righthit.collider.tag == "obstacle") {
					RightOb = true;
				}	
			}
			RaycastHit backhit;
			Ray backlandingRay = new Ray (transform.position, Vector3.back);
			if (Physics.Raycast (backlandingRay, out backhit, raylenth)) {				
				if (backhit.collider.tag == "obstacle") {
					BackOb = true;
				}	
			}

				switch (OrintationState) {

				case 1:
					if (!ForwardOb) {
						MoveAfterRotate ();
					} else if (!LeftOb) {
						OrintationState = 2;
						Turnleft ();
						MoveAfterRotate ();
					} else if (!RightOb) {
						OrintationState = 4;
						Turnright ();
						MoveAfterRotate ();
					} else if (!BackOb) {
						OrintationState = 3;
						Turnback ();
						MoveAfterRotate ();
					}
					break;
				case 2:
					if (!LeftOb) {
						MoveAfterRotate ();
					} else if (!BackOb) {
						OrintationState = 3;
						Turnleft ();
						MoveAfterRotate ();
					} else if (!ForwardOb) {
						OrintationState = 1;
						Turnright ();
						MoveAfterRotate ();
					} else if (!RightOb) {
						OrintationState = 4;
						Turnback ();
						MoveAfterRotate ();
					}
					break;
				case 3:
					if (!BackOb) {
						MoveAfterRotate ();
					} else if (!RightOb) {
						OrintationState = 4;
						Turnleft ();
						MoveAfterRotate ();
					} else if (!LeftOb) {
						OrintationState = 2;
						Turnright ();
						MoveAfterRotate ();
					} else if (!ForwardOb) {
						OrintationState = 1;
						Turnback ();
						MoveAfterRotate ();
					}
					break;
				case 4:
					if (!RightOb) {
						Debug.Log ("front");
						MoveAfterRotate ();
					} else if (!ForwardOb) {
						Debug.Log ("left");
						Turnleft ();
						OrintationState = 1;
						MoveAfterRotate ();
					} else if (!BackOb) {
						Debug.Log ("right");
						Turnright ();
						OrintationState = 3;
						MoveAfterRotate ();
					} else if (!LeftOb) {
						Debug.Log ("back");
						Turnback ();
						OrintationState = 2;
						MoveAfterRotate ();
					}
					break;
				}
			
		}
	}
    void MoveAfterRotate()
		{
			if(CanSetOriginalPoint)
			{
				OriginalPoint=transform.position;
				CanSetOriginalPoint=false;
			}
			
			moveforward(OriginalPoint);
			
		}
	void moveforward(Vector3 OriginalPoint)
	{
		   if (Vector3.Distance (transform.position, OriginalPoint) >= 5) {
			MovingStation = false;
			Roverturn = false;
			GameObject.Find("Abstract").SendMessage("SetRoverState");

		} else {
			MovingStation = true;
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
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
	void Turnleft()
	{
	    transform.Rotate (new Vector3 (0, -90, 0));
	}
	void Turnright()
	{
		transform.Rotate (new Vector3 (0, 90, 0));
	}
	void Turnback()
	{
		transform.Rotate (new Vector3 (0, 180, 0));
	}

}
