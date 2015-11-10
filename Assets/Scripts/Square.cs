using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	public Color colorStart = Color.white;
	public Color colorEnd = Color.green;
	public Color RedcolorStart = Color.white;
	public Color RedcolorEnd = Color.red;
	private bool ColorDisplay=false;
	private float duration = 1;
	private float Redduration =1;
	private Renderer rend;
	private Renderer groundrend;
	private Vector3 rock1position;
	void Start() {
		rend = GetComponent<Renderer>();
		groundrend=GameObject.Find("ground").GetComponent<Renderer>();
	}
	void Update() {
		if (ColorDisplay) {
			groundrend.enabled=false;
			rock1position = GameObject.Find ("rock1").transform.position;
			if ((transform.position.x < rock1position.x + 2.5 && transform.position.x > rock1position.x - 2.5 && transform.position.z < rock1position.z + 6.5 && transform.position.z > rock1position.z - 6.5) 
			    || (transform.position.x < rock1position.x + 6.5 && transform.position.x > rock1position.x - 6.5 && transform.position.z < rock1position.z + 2.5 && transform.position.z > rock1position.z - 2.5)) {
				float lerp = Mathf.PingPong (Time.time, duration) / duration;
				rend.material.color = Color.Lerp (colorStart, colorEnd, lerp);
			} else {
				//float lerp = Mathf.PingPong (Time.time, Redduration) / Redduration;
				//rend.material.color = Color.Lerp (RedcolorStart, RedcolorEnd, lerp);
				rend.material.color = Color.red;
			}
		}
		else
			groundrend.enabled=true;
	}
	void SetColorOn()
	{
		ColorDisplay=true;
	}
	void SetColorDown()
	{
		ColorDisplay=false;
	}
}
