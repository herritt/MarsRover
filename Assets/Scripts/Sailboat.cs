using UnityEngine;
using System.Collections;

public class Sailboat : MonoBehaviour {

	//the animation speed
	public float animationSpeed = 2.0f;

	//distance traveled
	private float distance = 0.0f;

	//max distance
	private float maxDistance = 40.0f;

	//keep track of direction travelling
	private bool forward = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//add to the distance travelled
		distance += Time.deltaTime;


		//check if we've reached max distance
		if (distance > maxDistance) {
			//we're travelled the max distance, so we want to turnaround

			//set distance travelled to zero
			distance = 0;

			//change direcction
			forward = !forward;

            //flip the sailboat
            //transform.RotateAround(Vector3.up, Mathf.PI);
            transform.Rotate(Vector3.up, Mathf.PI);
        }

		//check which direction we're moving
		if (forward) {
			//move the sailboat forward
			transform.position += Vector3.forward * Time.deltaTime * animationSpeed;
		} else {
			//move the sailboat back
			transform.position += Vector3.back * Time.deltaTime * animationSpeed;

		}


	}
}
