using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static int planet;
	
	// Use this for initialization
	void Start () {
		planet = 1;
	}
	
	void Awake()
	{
		if (ProfileManager.currentProfile == null && PlayerPrefs.GetInt("CurrentProfile")!= -1)
		{
			ProfileManager.currentProfile = new Profile();
			ProfileManager.currentProfile.LoadProfile(PlayerPrefs.GetInt("CurrentProfile"));
			SkillDataBase.SetSkills();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
