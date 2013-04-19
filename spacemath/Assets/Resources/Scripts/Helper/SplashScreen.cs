using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	
	Texture2D t_Splash;
	float timer;
	bool load;
	// Use this for initialization
	void Start () {
        timer = 2;
        t_Splash = (Texture2D)Resources.Load("Textures/Icon/SplashScreen");
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <=0 && !load)
		{
			CallLevelLoad.SetLevelLoad(Application.loadedLevel+1,1);
			load = true;
		}
		timer -= Time.deltaTime;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),t_Splash);
	}
	
}

