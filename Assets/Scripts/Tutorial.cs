using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    //private GameObject roverscript = GameObject.Find("Rover").GetComponent(RoverAIDay).enabled = false;
    private Rect windowRect = new Rect(150, 100, 400, 250);
    private Rect textRec = new Rect(250, 180, 200, 200);
    private Rect imageRec1 = new Rect(260, 220, 200, 200);
    private Rect imageRec2 = new Rect(280, 200, 150, 150);
    private bool showIntro = true;
    private string[] stringToEdit = { "Welcome to Mars\nIt is now your fate to\nprotect the peace on Mars", "Left click the rock to select it", "Then left click \nthe highlighted area to move rock", "The rover will always turn to its right", "Guide the rover to the portal", "During the daytime,\ndon't move the obstacle in front of\nthe rover", "During the night,\nyou are safe to move any obstacle\non the board", "Be careful!\nThe rover cannot go through\nthe board edge", "Collect stars to gain points", "Good Luck!" };
    private int fontsize = 34;
    private int index = 0;
    public Texture2D tex1;
    public Texture2D tex2;
    public Texture2D tex3;
    public Texture2D tex4;
	private GameObject abstarct;
    void OnGUI()
    {
        GUIStyle textstyle = new GUIStyle();
        textstyle.fontSize = fontsize;
        textstyle.normal.textColor = Color.black;
        textstyle.alignment = TextAnchor.UpperCenter;
        if (showIntro)
        {
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "");
            GUI.Label(textRec, stringToEdit[index], textstyle);
            if (GUI.Button(new Rect(20, 20, 100, 40), "Skip Tutorial"))
            {
				GameObject.Find ("abstract").SendMessage("ShowGUI");
                Application.LoadLevel("Level1");
            }

            if (index == 1)
            {
                GUI.Label(imageRec1, tex1);
            }
            else if (index == 3)
            {
                GUI.Label(imageRec1, tex2);
            }
            else if (index == 4)
            {
                GUI.Label(imageRec1, tex3);
            }
            else if (index == 8)
            {
                GUI.Label(imageRec2, tex4);
            }
        }
    }

    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(20, 20, 100, 40), "Previous"))
        {
            if(index > 1)
            {
                index = index - 1;
            }
        }
        else if (GUI.Button(new Rect(280, 20, 100, 40), "Next"))
        {
            fontsize = 20;
            index = index + 1;
            if (index > stringToEdit.Length - 1)
            {
                showIntro = false;
                Application.LoadLevel("Level1");
            }
                
        }
    }

}
