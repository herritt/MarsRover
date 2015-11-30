using UnityEngine;
using System.Collections;

public class Visibile : MonoBehaviour {

	private bool isVisible = false;
	private MeshRenderer r;

	// Use this for initialization
	void Start () {
		r = GetComponent<MeshRenderer> ();
		r.enabled = false;
	}
	
	public void MakeActive() {
		r.enabled = true;
	}
}
