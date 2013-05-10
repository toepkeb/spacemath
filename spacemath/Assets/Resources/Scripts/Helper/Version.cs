using UnityEngine;
using System.Collections;

public class Version : MonoBehaviour {
	
	static bool exists;
	public string version;
	
	// Use this for initialization
	void Start () {
		if (exists)
			Destroy(gameObject);
		else
			exists = true;
	}
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnGUI()
	{
		GUI.Label (new Rect(2,Screen.height-20,150,20),"Version: " + version);
	}
}
