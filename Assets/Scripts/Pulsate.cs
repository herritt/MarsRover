using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {
	
	private float largeSize;
	private float originalSize;
	private float animationSpeed = 1.25f;
	private float scaleAmount = 1.1f;
	private bool bigger = true;

	// Use this for initialization
	void Start () {
		originalSize = transform.localScale.x; 
		largeSize = originalSize * scaleAmount;
	}
	
	// Update is called once per frame
	void Update () {

		if (bigger) {
			if (transform.localScale.x < largeSize) {
				transform.localScale += new Vector3 (0.05F * Time.deltaTime * animationSpeed, 0.05F * Time.deltaTime * animationSpeed, 0);
			} else bigger = false;
		} else {
			if (transform.localScale.x > originalSize) {
				transform.localScale -= new Vector3 (0.05F * Time.deltaTime * animationSpeed, 0.05F * Time.deltaTime * animationSpeed, 0);
			} else bigger = true;
		}
	}
}
