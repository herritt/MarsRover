using UnityEngine;
using System.Collections;

public class obstaclemovement : MonoBehaviour
{
    private int i;
    private bool Playerturn = true;
    private float speed = 5;
    private float offset;
    private float temp;
    private Vector3 targetpoint;//the point which the obstacle want to move
    private bool Ismoveableplane = true;
    private GameObject square;
    private GameObject[] Squares;
    private int orintation = 0; // the direction of obstacle movement 1:left 2:up 3:right 4:down
    private GameObject selected; //Selected obstacle 
    private GameObject[] planes;
    private GameObject controller; // find abstract controller

    void Start()
    {
        planes = GameObject.FindGameObjectsWithTag("plane");
        selected = null;
        controller = GameObject.Find("Abstract");
    }

    void Update()
    {
        if (Playerturn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 1000))
                {
                    if (hitInfo.transform.tag == "rockobstacle")
                    {
                        selected = GameObject.Find(hitInfo.collider.gameObject.name); // selected the rock 
                        ShowSquarescolor();//display the color around the rock
                    }
                    else if (hitInfo.transform.tag == "plane")
                    {
                        square = hitInfo.transform.gameObject;
                        Canmovetothatsquare(square);//if the rock can move to that square
                        if (Ismoveableplane == true)
                        {
                            targetpoint = hitInfo.point;
                            targetpoint.y = selected.transform.position.y;
                            SelectMoveDirection(targetpoint);//judge the rock move to which square 
                        }
                    }
                }
            }

            switch (orintation)
            {
                //0-----no square 1-----left square 2----up square 3----right square 4---down square
                case 0:
                    break;

                case 1:
                    targetpoint.x = temp;
                    if (targetpoint.x < selected.transform.position.x)
                    {
                        selected.transform.Translate(Vector3.left * speed * Time.deltaTime);//if the rock have not arrived to target position,move forward
                    }
                    else if (targetpoint.x >= selected.transform.position.x)
                    {
                        GameObject.Find("Abstract").SendMessage("SetPlayerState");//if the rock have arrived to targer position,end player turn
                        ShowSquarescolor();
                        Playerturn = false;
                    }
                    break;

                case 2:
                    targetpoint.z = temp;
                    if (targetpoint.z < selected.transform.position.z)
                    {
                        selected.transform.Translate(Vector3.up * speed * Time.deltaTime);
                    }
                    else if (targetpoint.z >= selected.transform.position.z)
                    {
                        GameObject.Find("Abstract").SendMessage("SetPlayerState");
                        ShowSquarescolor();
                        ShowSquarescolor();
                        Playerturn = false;
                    }
                    break;

                case 3:
                    targetpoint.x = temp;
                    if (targetpoint.x > selected.transform.position.x)
                    {
                        selected.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    }
                    else if (targetpoint.x <= selected.transform.position.x)
                    {
                        GameObject.Find("Abstract").SendMessage("SetPlayerState");
                        ShowSquarescolor();
                        Playerturn = false;
                    }
                    break;

                case 4:
                    targetpoint.z = temp;
                    if (targetpoint.z > selected.transform.position.z)
                    {
                        selected.transform.Translate(Vector3.down * speed * Time.deltaTime);
                    }
                    else if (targetpoint.z <= selected.transform.position.z)
                    {
                        GameObject.Find("Abstract").SendMessage("SetPlayerState");
                        ShowSquarescolor();
                        Playerturn = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    void SelectMoveDirection(Vector3 targetpoint)
    {
        //detect mouse position to judge which square is the goal,update target position of rock
        if (targetpoint.x < selected.transform.position.x - 2.5)
        {
            targetpoint.x = selected.transform.position.x - 5;
            temp = selected.transform.position.x - 5;
            targetpoint.z = selected.transform.position.z;
            orintation = 1;
        }
        else if (targetpoint.x > selected.transform.position.x + 2.5)
        {
            targetpoint.x = selected.transform.position.x + 5;
            temp = selected.transform.position.x + 5;
            targetpoint.z = selected.transform.position.z;
            orintation = 3;
        }
        else if (targetpoint.z < selected.transform.position.z - 2.5)
        {
            targetpoint.z = selected.transform.position.z - 5;
            temp = selected.transform.position.z - 5;
            targetpoint.x = selected.transform.position.x;
            orintation = 2;
        }
        else if (targetpoint.z > selected.transform.position.z + 2.5)
        {
            targetpoint.z = selected.transform.position.z + 5;
            temp = selected.transform.position.z + 5;
            targetpoint.x = selected.transform.position.x;
            orintation = 4;
        }
    }

    void Canmovetothatsquare(GameObject square)
    {
       	//rover only can move one grid around it
        if (square.transform.position.x < selected.transform.position.x - 2.5 && square.transform.position.z > selected.transform.position.z + 2.5)
            Ismoveableplane = false;
        else if (square.transform.position.x > selected.transform.position.x + 2.5 && square.transform.position.z > selected.transform.position.z + 2.5)
            Ismoveableplane = false;
        else if (square.transform.position.x < selected.transform.position.x - 2.5 && square.transform.position.z < selected.transform.position.z - 2.5)
            Ismoveableplane = false;
        else if (square.transform.position.x > selected.transform.position.x + 2.5 && square.transform.position.z < selected.transform.position.z - 2.5)
            Ismoveableplane = false;
        else
            Ismoveableplane = true;
    }

    public void SetPlayerState()
    {
        Playerturn = true;
        Ismoveableplane = true;
        orintation = 0;

    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 100, 100, 40), "skip turn"))
        {
            skipturn();
        }
    }

    void skipturn()
    {
        //if click skip button,end player turn
        controller.SendMessage("SetPlayerState");
        Playerturn = false;
    }
    public bool GetPlayerState()
    {
        return Playerturn;
    }

    void ShowSquarescolor()
    {
        //display the color around the rock
        foreach (GameObject plane in planes)
        {
            plane.SendMessage("findobstacle");
        }
    }


}
