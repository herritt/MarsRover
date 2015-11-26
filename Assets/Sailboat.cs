using UnityEngine;
using System.Collections;

public class Sailboat : MonoBehaviour {

	public float animationSpeed = 2.0f;
	private float distance = 0.0f;
	private bool forward = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		distance += Time.deltaTime;

		if (distance > 40) {
			distance = 0;
			forward = !forward;
			transform.RotateAround(Vector3.up, Mathf.PI);
		}

		if (forward) {
			//move the sailboat forward
			transform.position += Vector3.forward * Time.deltaTime * animationSpeed;
		} else {
			//move the sailboat back
			transform.position += Vector3.back * Time.deltaTime * animationSpeed;

		}


	}
}
