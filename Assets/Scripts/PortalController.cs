using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour {

	//the rotation speed of the portal
	public float rotationSpeed = 20.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//rotate the portal
		transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
	}
}
