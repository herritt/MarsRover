using UnityEngine;
using System.Collections;

public class MartianController : MonoBehaviour {

	public Animator anim;
	private float speed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("speed", 1);
		transform.position += Vector3.right * Time.deltaTime;

	}
}
