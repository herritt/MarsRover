using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {

	//the maximum size that the object will puslate to
	private float largeSize;

	//the original size of the object
	private float originalSize;

	//the speed to pulsate
	private float animationSpeed = 1.25f;

	//the amount to scale the object
	private float scaleAmount = 1.1f;

	//start with getting bigger set to true
	private bool bigger = true;

	// Use this for initialization
	void Start () {

		//get the orignal size of the object
		originalSize = transform.localScale.x; 

		//set the maximum size to be the orginal size scaled by the scale amount
		largeSize = originalSize * scaleAmount;
	}
	
	// Update is called once per frame
	void Update () {

		//are we getting bigger
		if (bigger) {
			//check if we are at maximum size
			if (transform.localScale.x < largeSize) {
				//we're not at maximum size, so get larger
				transform.localScale += new Vector3 (0.05F * Time.deltaTime * animationSpeed, 0.05F * Time.deltaTime * animationSpeed, 0);
			} 
			else
				//we're at maximum size, so we need to start getting smaller
				bigger = false;
		} else {
			//we are getting smaller

			//check if object is back to original size
			if (transform.localScale.x > originalSize) {
				//we're not at original size, so get smaller
				transform.localScale -= new Vector3 (0.05F * Time.deltaTime * animationSpeed, 0.05F * Time.deltaTime * animationSpeed, 0);
			} else
				//we're at minimum size, so we need to start getting bigger
				bigger = true;
		}
	}
}
