using UnityEngine;
using System.Collections;
//This script is written for change grid color.
public class test : MonoBehaviour
{
	public Color colorStart = Color.white;
	public Color colorEnd = Color.green;
	public Color RedcolorStart = Color.white;
	public Color RedcolorEnd = Color.red;
	private bool ColorDisplay=true;
	private float duration = 5;
	private float Redduration = 1;
	private Renderer rend;
	private Renderer groundrend;
	private Vector3 rockposition;
	private GameObject rock;

	void Start()
    {
		rend = GetComponent<Renderer>();
		groundrend = GameObject.Find("ground").GetComponent<Renderer>();
		rend.enabled = false;
	}

	void Update()
    {
		if (rock != null)
        {
            if ((transform.position.x < rockposition.x + 2.5 && transform.position.x > rockposition.x - 2.5 && transform.position.z < rockposition.z + 6.5 && transform.position.z > rockposition.z - 6.5) || (transform.position.x < rockposition.x + 6.5 && transform.position.x > rockposition.x - 6.5 && transform.position.z < rockposition.z + 2.5 && transform.position.z > rockposition.z - 2.5))
            {
                //if the grid is one of the eight nearest grids around the rock,change the color into green
                float lerp = Mathf.PingPong (Time.time, duration) / duration;
                rend.material.color = Color.Lerp (colorStart, colorEnd, lerp);
			}
            else
            {
                //for the other grid change color into red
                rend.material.color = Color.red;
			}
		}
	}
    //called by turn manager.If it is rover turn ,turn on the coler.
    void SetColorOn()
	{
		ColorDisplay = true;
		rend.enabled = true;
	}
    //called by turn manager.If it is player turn ,turn down the coler.
    void SetColorDown()
	{
		ColorDisplay = false;
		rend.enabled = false;
	}
    //called by obstacle,when the player click obstacle show the grid it can move
    void findobstacle()
    {
		RaycastHit hitInfo;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hitInfo, 100)) 
		{
			rock = GameObject.Find(hitInfo.collider.gameObject.name); // Game Object
			rockposition = rock.transform.position;
            rend.enabled =true;
		}
	}


}
