using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	private float currentRotation = 0.0f;
	public float rotationAmt = 5.0f;
	private bool right = true;
	public float animationSpeed = 2f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (right) {
			if (currentRotation < rotationAmt) {
				currentRotation += Time.deltaTime * animationSpeed;
				transform.Rotate(Vector3.up * Time.deltaTime * animationSpeed);
			} else right = false;
		} else {
			if (currentRotation > -rotationAmt) {
				currentRotation -= Time.deltaTime * animationSpeed;
				transform.Rotate(Vector3.down * Time.deltaTime * animationSpeed);
			} else right = true;
		}
	}

}
