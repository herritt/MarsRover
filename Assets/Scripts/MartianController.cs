using UnityEngine;
using System.Collections;

public class MartianController : MonoBehaviour {

	//the martian's animator
	public Animator anim;

	// Use this for initialization
	void Start () {

		//get the reference to the martian's animator
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		//set speed to 1 to trigger walk animation
		anim.SetFloat ("speed", 1);

		//make the martian move
		transform.position += Vector3.right * Time.deltaTime;

	}
}
