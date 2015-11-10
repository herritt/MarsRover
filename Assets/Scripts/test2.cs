using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {
	private float speed=10;
	// Use this for initialization
	void Start () {
		transform.Rotate (new Vector3 (0, 90, 0));       
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
}
