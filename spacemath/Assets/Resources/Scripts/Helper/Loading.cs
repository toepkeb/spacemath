using UnityEngine;
using System.Collections;

//Put this script on a GameObject that is the parent of everything in the loading Screen
//It will destroy itself once the next scene is loadined
//By Defaul it is on the Main Camera of Scene_Loading
public class Loading : MonoBehaviour {

    Texture2D t_black;
    public Texture2D t_loading;
	
	// Use this for initialization
	void Start () {
        t_black = (Texture2D)Resources.Load("Textures/UI/black");
        Application.LoadLevelAdditive(CallLevelLoad.levelToLoad);
        
	}
	
	// Update is called once per frame
	void Update () {

        if (!Application.isLoadingLevel)
            Destroy(gameObject);
	}

    void OnGUI()
    {
       
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), t_black, ScaleMode.StretchToFill);
        if (t_loading)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), t_loading, ScaleMode.StretchToFill);
        else
            GUI.Label(HelperFunctions.RectangleCenter(.5f, .5f, 200, 64), "LOADING LEVEL...." + CallLevelLoad.levelToLoad);
    }
}
