using UnityEngine;
using System.Collections;

public class RoverRaycasting : MonoBehaviour {

	public float deploymentHeight;//diatance of the ray
	// Use this for initialization
	void Start () {
		//transform.Rotate (new Vector3 (0, 90, 0));
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray landingRay = new Ray (transform.position, Vector3.forward);
		Debug.DrawRay(transform.position, Vector3.forward*deploymentHeight);
		transform.Translate (Vector3.forward * 5 * Time.deltaTime);
		if (Physics.Raycast (landingRay, out hit, deploymentHeight)) {
		
		   if(hit.collider.tag=="obstacle")
			{
				//Debug.Log("object  object");
			}
		
		}

	}
}
