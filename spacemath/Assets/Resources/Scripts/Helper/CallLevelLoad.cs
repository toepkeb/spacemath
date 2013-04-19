using UnityEngine;
using System.Collections;

public class CallLevelLoad : MonoBehaviour {

    public static int levelToLoad;
    static bool load;

	// Use this for initialization
	void Start () {
        load = false;
	}

    void OnLevelWasLoaded(int level)
    {
        load = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (load)
        {
            if (FadeInOut.GetAlpha == 1)
            {
                Application.LoadLevel("Scene_Loading");
            }
        }
	
	}

    public static void SetLevelLoad(int levelToLoad, float fadeTime)
    {
        if (!load)
        {
            load = true;
            CallLevelLoad.levelToLoad = levelToLoad;
            FadeInOut.fadeTime = fadeTime;
            FadeInOut.fade = true;
            FadeInOut.fadeIn = false;
        }

    }
}
