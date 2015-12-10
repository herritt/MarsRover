using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {

	//has the button been clicked on
	private bool clicked = false;

	//has the button finished getting big (used for animating the button after clicking on it)
	private bool big = false;

	//has the button finished its animation
	private bool finished = false;

	//speed of animation of button click
	public float animationSpeed = 10.0f;

	//the original size of the button
	private float originalSize;

	void OnMouseDown() {

		//we clicked on the button
		clicked = true;

		//play the click sound
		GetComponent<AudioSource> ().Play ();
	}

	void Start() {

		//get the orignal size of the button
		originalSize = transform.localScale.x; 
	
	}

	void Update() {
	
		//is the button clicked on
		if (clicked) {
		
			//has the button finished animating after being clicked on
			if (!finished) {
			
				//is the button finished getting bigger
				if (!big) {

					//make the button bigger
					transform.localScale += new Vector3 (0.05F * Time.deltaTime * animationSpeed, 0.05F * Time.deltaTime * animationSpeed, 0);
				
					//check to see if big enough
					if (transform.localScale.x > originalSize * 1.1) {
						big = true;
					}

				} else {
				
					//get smaller
					//make the button smaller
					transform.localScale -= new Vector3 (0.05F * Time.deltaTime * animationSpeed, 0.05F * Time.deltaTime * animationSpeed, 0);
				
					//check to see if small enough
					if (transform.localScale.x <= originalSize) {
						finished = true;
					}
				} 
			} else {

				//finsiehd animating, load the first level
				Application.LoadLevel("ProtoLevel");
			}

		
		}
	
	}

}


