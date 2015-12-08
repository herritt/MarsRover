using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public Color colorStart = Color.white;
	public Color colorEnd = Color.green;
	public Color RedcolorStart = Color.white;
	public Color RedcolorEnd = Color.red;
	private bool ColorDisplay=true;
	private float duration = 5;
	private float Redduration =1;
	private Renderer rend;
	private Renderer groundrend;
	private Vector3 rockposition;
	private GameObject rock;
	void Start() {

		rend = GetComponent<Renderer>();
		groundrend=GameObject.Find("ground").GetComponent<Renderer>();
		rend.enabled = false;

	}
	void Update() {
		if (rock != null) {
				if ((transform.position.x < rockposition.x + 2.5 && transform.position.x > rockposition.x - 2.5 && transform.position.z < rockposition.z + 6.5 && transform.position.z > rockposition.z - 6.5) 
					|| (transform.position.x < rockposition.x + 6.5 && transform.position.x > rockposition.x - 6.5 && transform.position.z < rockposition.z + 2.5 && transform.position.z > rockposition.z - 2.5)) {
					float lerp = Mathf.PingPong (Time.time, duration) / duration;
					rend.material.color = Color.Lerp (colorStart, colorEnd, lerp);
				} else {
					rend.material.color = Color.red;
				}
			}

	}
	void SetColorOn()
	{
		//Debug.Log("yuanlairuci");
		ColorDisplay=true;
		rend.enabled = true;
	}
	void SetColorDown()
	{
		//Debug.Log("SetColorDown");
		ColorDisplay=false;
		rend.enabled=false;
		//Debug.Log ("square1212 end");
	}
	void findobstacle(){
		RaycastHit hitInfo;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//Debug.Log ("11111111111111111");
		if (Physics.Raycast (ray, out hitInfo, 100)) 
		{
			rock = GameObject.Find(hitInfo.collider.gameObject.name); // Game Object
			rockposition = rock.transform.position;
			rend.enabled =true;
		}
	}


}
