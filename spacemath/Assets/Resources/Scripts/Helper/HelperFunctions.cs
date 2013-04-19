using UnityEngine;
using System.Collections;

public class HelperFunctions : MonoBehaviour
{

    public Texture2D black;
    public GUISkin skin;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Rect RectangleCenter(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;
        return new Rect(Screen.width * x - w * .5f * wOffset, Screen.height * y - h * .5f * hOffset, w * wOffset, h * hOffset);
    }

    public static Rect RectangleRight(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x - w * wOffset, Screen.height * y - h * .5f * hOffset, w * wOffset, h * hOffset);
    }

    public static Rect RectangleLeft(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x, Screen.height * y - h * .5f * hOffset, w * wOffset, h * hOffset);
    }

    public static Rect RectangleBottom(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x - w * .5f * wOffset, Screen.height * y - h * hOffset, w * wOffset, h * hOffset);
    }

    public static Rect RectangleTop(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x - w * .5f * wOffset, Screen.height * y, w * wOffset, h * hOffset);
    }

    public static Rect RectangleTopLeft(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x, Screen.height * y, w * wOffset, h * hOffset);
    }

    public static Rect RectangleTopRight(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x - w * wOffset, Screen.height * y, w * wOffset, h * hOffset);
    }

    public static Rect RectangleBottomLeft(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x, Screen.height * y - h*hOffset, w * wOffset, h * hOffset);
    }

    public static Rect RectangleBottomRight(float x, float y, int w, int h)
    {
        float wOffset = Screen.width / 1024f;
        float hOffset = Screen.height / 768f;

        return new Rect(Screen.width * x - w * wOffset, Screen.height * y - h*hOffset, w * wOffset, h * hOffset);
    }

    void OnGUI()
    {
        //GUI.skin = skin;
        //GUI.Button(RectangleCenter(.5f, .5f, 256, 256), "RectangleCenter");
        //GUI.Button(RectangleRight(1, .5f, 256, 256), "RectangleRight");
        //GUI.Button(RectangleLeft(0, .5f, 256, 256), "RectangleLeft");
        //GUI.Box(RectangleTop(.5f, 0, 256, 256), "RectangleTop");
        //GUI.Box(RectangleBottom(.5f, 1, 256, 256), "Rectanglebottom");
        //GUI.Box(RectangleTopLeft(0, 0, 256, 256), "Top Left");
        //GUI.Box(RectangleTopRight(1, 0, 256, 256), "Top Right");
        //GUI.Box(RectangleBottomLeft(0, 1, 256, 256), "Bottom Left");
        //GUI.Box(RectangleBottomRight(1, 1, 256, 256), "Bottom Right");

        //if (GUI.Button(RectangleBottom(.5f, 1, 256, 256), "My Button"))
        //{
        //}
        //if (GUI.Button(RectangleTop(.5f, 0, 256, 256), "", skin.customStyles[0]))
        //{
        //}

    }


    /// <summary>
    /// Used to load all the assets in a folder as GameObjects
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject[] LoadAllGameObjects(string path)
    {
        Object[] obj = Resources.LoadAll(path);
        GameObject[] go = new GameObject[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            go[i] = (GameObject)obj[i];
        }

        return go;
    }

    /// <summary>
    /// Used to load all the assets in the folder as Textures
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture2D[] LoadAllTextures(string path)
    {
        Object[] obj = Resources.LoadAll(path);

        Texture2D[] go = new Texture2D[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            go[i] = (Texture2D)obj[i];
        }

        return go;
    }

    /// <summary>
    /// Smoothly turns a transform towards a target where the up vector is Vector3.up
    /// Should be called every frame you want to turn
    /// Speed should be a small number around .01f 
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public static Quaternion TurnTowardsTarget(Transform trans, Vector3 target, float speed)
    {
        Quaternion currentFacing = trans.rotation;
        trans.LookAt(target);
        return Quaternion.Lerp(currentFacing, trans.rotation, speed);

    }

    public static bool ISIPAD()
    {
        if (iPhone.generation == iPhoneGeneration.iPad1Gen
            || iPhone.generation == iPhoneGeneration.iPad2Gen
            || iPhone.generation == iPhoneGeneration.iPad3Gen
			|| iPhone.generation == iPhoneGeneration.iPad4Gen)
        {
            return true;
        }
        else
            return false;

    }

    /// <summary>
    /// Returns true if the device is iPad 2 or 3 or iPhone 4,4s, or 5
    /// </summary>
    /// <returns></returns>
    public static bool ISNEXTGEN()
    {
        if (iPhone.generation == iPhoneGeneration.iPad2Gen
            || iPhone.generation == iPhoneGeneration.iPad3Gen
			|| iPhone.generation == iPhoneGeneration.iPad4Gen
            || iPhone.generation == iPhoneGeneration.iPhone4
            || iPhone.generation == iPhoneGeneration.iPhone4S
            || iPhone.generation == iPhoneGeneration.iPhone5)
        {
            return true;
        }
        else
            return false;
    }
	
	public static bool ContainsPoint (Vector2[] polyPoints,Vector2 p)
	{ 
		int j = polyPoints.Length-1; 
	    bool inside = false; 
	    for (int i = 0; i < polyPoints.Length; j = i++) 
		{ 
	    	if ( ((polyPoints[i].y <= p.y && p.y < polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y < polyPoints[i].y)) 
			&& (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x)) 
	     		inside = !inside; 
	   	} 
	   return inside; 
	}
}
