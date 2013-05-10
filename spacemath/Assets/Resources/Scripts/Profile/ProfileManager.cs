using UnityEngine;
using System.Collections;
//Change 1
//Added changed2 for test
public class ProfileManager : MonoBehaviour {
	//blah blah blah KAY
	public GameObject profileBoard;
	
	ProfileCamera profileCamera;
	GameObject[] avatars;
	GameObject avatarParent;
	float avatarOffset = 3;
	bool startedAnimation;
	int crewMembersSelected;
	Profile.Avatar[] crew;
	
	public enum ProfileManagerState
	{
		SelectProfile, CreateName, SelectAvatar, SelectOutfit, SelectCrew, ConnectToJuddly, Stats, Moving, AvatarComplete
	}
	public static ProfileManagerState state;
	public static ProfileManagerState nextState;
	
	Profile[] profiles;
	public static Profile currentProfile;
	public static int currentProfileIndex;
	
	public ButtonAnchor[] newButtons;
	public ButtonAnchor[] deleteButtons;
	public ButtonAnchor[] playButtons;
	public ButtonAnchor[] backButtons;
	public ButtonAnchor[] crewButtons;
//	public ButtonAnchor acceptNameButton;
	public ButtonAnchor acceptButton;
	
	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll();
		if (!PlayerPrefs.HasKey("CurrentProfile"))
			PlayerPrefs.SetInt("CurrentProfile",-1);
		profileCamera = Camera.main.GetComponent<ProfileCamera>();
		crew = new Profile.Avatar[3];
		
		profiles = new Profile[4];
		for (int i=0; i < profiles.Length;i++)
		{
			profiles[i] = new Profile();
			profiles[i].LoadProfile(i);
//			float avatarOffset = 3;
			SetProfileBoard(i);
		}
		
		
		SkillDataBase.SetSkills();
		
		SetProfileBoardButtonActive(true);
		
		acceptButton.gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		#region Select Profile
		if (state == ProfileManagerState.SelectProfile)
		{
			for (int i=0; i < newButtons.Length;i++)
			{
				if (newButtons[i].DetectClick())
				{
					currentProfileIndex = i;
					state = ProfileManagerState.Moving;
					nextState = ProfileManagerState.CreateName;
					profiles[currentProfileIndex].CreateProfile(i);
					SetProfileBoardButtonActive(false);
					profileCamera.SetMove();
				}
			}
			for (int i=0; i< deleteButtons.Length;i++)
			{
				if (deleteButtons[i].DetectClick())
				{
					profiles[i].DeleteProfile(i);
					SetProfileBoard(i);
				}
			}
			for (int i=0; i < playButtons.Length;i++)
			{
				if (playButtons[i].DetectClick())
				{
					currentProfile = profiles[i];
					currentProfileIndex = i;
					PlayerPrefs.SetInt("CurrentProfile",i);
					CallLevelLoad.SetLevelLoad(3,.5f);
				}
			}
		}
		#endregion
		#region Create Name
		else if (state == ProfileManagerState.CreateName)
		{
			acceptButton.gameObject.SetActive(true);
			acceptButton.isActive = true;
			if (acceptButton.DetectClick())
			{
				
				if (avatars == null)
				{
//					avatarParent = new GameObject();
					avatars = LoadAvatars();
					GameObject platforms = GameObject.Find ("AvatarPlatforms");
					
					for (int i=0; i < avatars.Length;i++)
					{
						avatars[i] = (GameObject)GameObject.Instantiate(avatars[i],new Vector3(i*avatarOffset,0,0),Quaternion.identity);
						avatars[i].transform.parent = platforms.transform.FindChild(i.ToString());
					}
				}
				
				state = ProfileManagerState.Moving;
				nextState = ProfileManagerState.SelectAvatar;
				profileCamera.SetMove();
				acceptButton.gameObject.SetActive(false);
			}
			
			for (int i=0; i < backButtons.Length;i++)
			{
				if (backButtons[i].DetectClick())
				{
					profiles[i].DeleteProfile(i);
					SetProfileBoard(i);
					state = ProfileManagerState.Moving;
					nextState = ProfileManagerState.SelectProfile;
					profileCamera.SetMove();
					SetProfileBoardButtonActive(true);
					acceptButton.gameObject.SetActive(false);
				}
			}
		}
		#endregion
		
		#region Select Avatar
		else if (state == ProfileManagerState.SelectAvatar)
		{
			acceptButton.gameObject.SetActive(true);
			
			if (acceptButton.DetectClick())
			{
				profileCamera.SetZoomIn();
				state = ProfileManagerState.Moving;
				nextState = ProfileManagerState.SelectOutfit;
				acceptButton.gameObject.SetActive(false);
				profiles[currentProfileIndex].AvatarType = (Profile.Avatar)profileCamera.selection;
			}
		}
		#endregion
		
		#region Select Outfit
		else if (state == ProfileManagerState.SelectOutfit)
		{
			acceptButton.gameObject.SetActive(true);
			
			if (acceptButton.DetectClick())
			{
				profileCamera.SetZoomOut();
				state = ProfileManagerState.Moving;
				nextState = ProfileManagerState.AvatarComplete;
				acceptButton.gameObject.SetActive(false);
				//profiles[currentProfileIndex].Outfit = 
			}
		}
		#endregion
		#region Animation
		else if (state == ProfileManagerState.AvatarComplete)
		{
			if (!startedAnimation)
			{
				avatars[profileCamera.selection].transform.parent.animation.Play ();
				startedAnimation = true;
			}
			else 
			{
				if (!avatars[profileCamera.selection].transform.parent.animation.isPlaying)
				{
					avatars[profileCamera.selection].transform.parent.gameObject.SetActive(false);
					state = ProfileManagerState.Moving;
					nextState = ProfileManagerState.SelectCrew;
					acceptButton.gameObject.SetActive(false);
					SetCrewButtonActive(true);
					crewButtons[profileCamera.selection].isActive = false;
					profileCamera.SetMove();
				}
			}
		}
		#endregion
		#region Select Crew
		else if (state == ProfileManagerState.SelectCrew)
		{
			for (int i=0; i < crewButtons.Length;i++)
			{
				if (crewButtons[i].DetectClick())
				{
					if (avatars[i].transform.parent.position.y == .5f)
					{
						avatars[i].transform.parent.position = new Vector3(avatars[i].transform.parent.position.x,-.5f,avatars[i].transform.parent.position.z);
						RemoveCrewMember(i);
						crewMembersSelected--;
					}
					else if (crewMembersSelected <3 && avatars[i].transform.parent.position.y == -.5f)
					{
						avatars[i].transform.parent.position = new Vector3(avatars[i].transform.parent.position.x,.5f,avatars[i].transform.parent.position.z);
						AddCrewMember(i, crewMembersSelected);
						crewMembersSelected++;
					}
				}
			}
			
			if (crewMembersSelected == 3)
				acceptButton.gameObject.SetActive(true);
			else
				acceptButton.gameObject.SetActive(false);
			
			if (acceptButton.DetectClick())
			{
				SetCrewButtonActive(false);
				state = ProfileManagerState.Moving;
				nextState = ProfileManagerState.SelectProfile;
				profileCamera.SetMove();
				acceptButton.gameObject.SetActive(false);
				profiles[currentProfileIndex].Crew = crew;
				profiles[currentProfileIndex].SaveProfile();
				SetProfileBoard(currentProfileIndex);
				SetProfileBoardButtonActive(true);
				
				for (int i=0; i < crew.Length;i++)
				{
					Debug.Log (crew[i]);
				}
			}
		}
		#endregion
		
	}
	
	void OnGUI()
	{
		switch(state)
		{
			#region Select Profile
			case ProfileManagerState.SelectProfile:
//			for (int i=0; i < 4;i++)
//			{
//				Rect box = new Rect(50 + 250*(i%2), 50 + 250 *(i/2),250,250);
//				GUI.Box (box,i.ToString());
//				
//				GUI.BeginGroup(box);
//				
//				
//				if (profiles[i].Index == -1)
//				{
//					if (GUI.Button (new Rect(65,150,120,50),"NEW"))
//					{
//						currentProfileIndex = i;
//						state = State.CreateName;
//						profiles[currentProfileIndex].CreateProfile(i);
//						target = Vector3.zero;
//					}
//				}
//				else 
//				{
//					GUI.Label (new Rect(25,50,200,30),"Name: " + profiles[i].Name);
//					GUI.Label (new Rect(25,80,200,30),"Avatar: " + (profiles[i].AvatarType).ToString());
//					GUI.Label (new Rect(25,110,200,30),"Outfit: " + profiles[i].Outfit);
//					GUI.Label (new Rect(25,140,200,30),"Grade: " + profiles[i].Grade);
//					string val = (profiles[i].LinkedToJuddly == true) ? ("Yes" ): ("No");
//					GUI.Label (new Rect(25,170,200,30),"Connected to Juddly: " + val);
//				
//					if (GUI.Button (new Rect(65,200,120,30),"Delete"))
//					{
//						profiles[i].DeleteProfile(i);
//					}
//					if (GUI.Button (new Rect(10,200,50,30),"Stats"))
//					{
//						state = State.Stats;
//					}
//				}
//				
//				GUI.EndGroup();
//			}
			
			break;
			#endregion
			#region CreateName
			case ProfileManagerState.CreateName:			
			profiles[currentProfileIndex].Name = GUI.TextField( new Rect(Screen.width*.5f - 150,Screen.height*.5f,300,50),profiles[currentProfileIndex].Name);
//			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Nickname");
//			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
//			{
//				state = ProfileManagerState.SelectAvatar;
//				
//				if (avatars == null)
//				{
//					avatarParent = new GameObject();
//					avatars = LoadAvatars();
//					
//					for (int i=0; i < avatars.Length;i++)
//					{
//						avatars[i] = (GameObject)GameObject.Instantiate(avatars[i],new Vector3(i*avatarOffset,0,0),Quaternion.identity);
//						avatars[i].transform.parent = avatarParent.transform;
//					}
//				}
//			}
			break;
			#endregion
			#region SelectAvatar
			case ProfileManagerState.SelectAvatar:
			
//			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Avatar");
//			for (int i=0; i < 8;i++)
//			{
//				Rect imgRect = new Rect(200+(i*80),Screen.height*.5f,80,80);
//				if (GUI.Button(imgRect,((Profile.Avatar)i).ToString()))
//				{
//					profiles[currentProfileIndex].AvatarType = (Profile.Avatar)i;
//					//Debug.Log (profiles[currentProfileIndex].AvatarType);
//				}
//			}
			
			
			
//			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
//			{
//				profiles[currentProfileIndex].AvatarType = (Profile.Avatar)(selection*-1);
//				state = ProfileManagerState.SelectOutfit;
//			}
			break;
			#endregion
			#region SelectOutfit
			case ProfileManagerState.SelectOutfit:
//			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Outfit");
//			for (int i=0; i < 8;i++)
//			{
//				Rect imgRect = new Rect(200+(i*80),Screen.height*.5f,80,80);
//				if (GUI.Button(imgRect,i.ToString()))
//				{
//					profiles[currentProfileIndex].Outfit = i;
//				}
//			}
//			
//			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
//			{
//				state = ProfileManagerState.ConnectToJuddly;
//			}
			break;
			#endregion
			#region Select Crew
			case ProfileManagerState.SelectCrew:
			//GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Outfit");
//			for (int i=0; i < 8;i++)
//			{
//				Rect imgRect = new Rect(200+(i*80),Screen.height*.5f,80,80);
//				if (GUI.Button(imgRect,i.ToString()))
//				{
//					profiles[currentProfileIndex].Outfit = i;
//				}
//			}
//			
//			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
//			{
//				state = ProfileManagerState.ConnectToJuddly;
//			}
			break;
			#endregion
			#region ConnectToJuddly
			case ProfileManagerState.ConnectToJuddly:
			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Connect to Juddly");
			
			if (GUI.Button(new Rect(Screen.width*.4f-30,Screen.height* .5f,60,50),"Yes"))
			{
				state = ProfileManagerState.SelectProfile;
				profiles[currentProfileIndex].SaveProfile();
				SetProfileBoard(currentProfileIndex);
				SetProfileBoardButtonActive(true);
			}
			if (GUI.Button(new Rect(Screen.width*.6f-30, Screen.height*.5f,60,50),"NO"))
			{
				state = ProfileManagerState.SelectProfile;
				SetProfileBoard(currentProfileIndex);
				profiles[currentProfileIndex].SaveProfile();
				SetProfileBoardButtonActive(true);	
			}
		
			break;
			#endregion	
			#region Stats
			case ProfileManagerState.Stats:
			if (GUI.Button (new Rect(150,150,100,100),"1"))
			{
				profiles[currentProfileIndex].Stats.AddProgress("One",1);
			}
			if (GUI.Button (new Rect(250,150,100,100),"2"))
			{
				profiles[currentProfileIndex].Stats.AddProgress("Two",1);
			}
			if (GUI.Button (new Rect(350,150,100,100),"3"))
			{
				profiles[currentProfileIndex].Stats.AddProgress("Three",1);
			}
			if (GUI.Button (new Rect(450,150,100,100),"4"))
			{
				profiles[currentProfileIndex].Stats.AddProgress("Four",1);
			}
			if (GUI.Button (new Rect(550,150,100,100),"5"))
			{
				profiles[currentProfileIndex].Stats.AddProgress("Five",1);
			}
			
			if (GUI.Button (new Rect(500,50,100,100),"Back"))
			{
				state = ProfileManagerState.SelectProfile;
				profiles[currentProfileIndex].Stats.SaveStats();
			}
			if (GUI.Button( new Rect(610,50,100,100),"Skills"))
			{
				showSkills = !showSkills;
			}
			
			if (showSkills)
				DisplaySkills();
			
			break;
			#endregion
		}
		
	
	}
	
	bool showSkills;
	Vector2 scollPosition;
	void DisplaySkills()
	{
		Rect box = new Rect(Screen.width*.5f-120,Screen.height*.5f,240,200);
		scollPosition = GUI.BeginScrollView(box,scollPosition,new Rect(0,0,240,20*(SkillDataBase.skills.Count+1)));
		
		int i=0;
		foreach (DictionaryEntry de in SkillDataBase.skills)
		{
			Rect content = new Rect (5,5+i*20,230,20);
			Skill skill = (Skill)de.Value;
			
			GUI.Label(content,"Type: " + skill.Type + "  Value: " +skill.Value + "  Progress: " +skill.Progress);
			i++;
		}
		
		GUI.EndScrollView();
	}
	
	GameObject[] LoadAvatars()
	{
		return HelperFunctions.LoadAllGameObjects("Prefabs/Temps");
	}
	
	void SetProfileBoard(int i)
	{
		if (profileBoard == null)
			return;
		
		if (profiles[i].Index == -1)
		{
			deleteButtons[i].gameObject.SetActive(false);
			newButtons[i].gameObject.SetActive(true);
			playButtons[i].gameObject.SetActive(false);
			profileBoard.transform.FindChild(i + "Name").GetComponent<TextMesh>().text = "";
			profileBoard.transform.FindChild(i + "Avatar").GetComponent<TextMesh>().text = "";
			profileBoard.transform.FindChild(i + "Outfit").GetComponent<TextMesh>().text = "";
			profileBoard.transform.FindChild(i + "Grade").GetComponent<TextMesh>().text ="";
			profileBoard.transform.FindChild(i + "Juddly").GetComponent<TextMesh>().text = "";
			profileBoard.transform.FindChild(i + "Progress").GetComponent<TextMesh>().text = "";
			
			for (int j=0; j< crew.Length;j++)
			{
				profileBoard.transform.FindChild(i+"Crew" +j).GetComponent<TextMesh>().text = "";
			}
		}
		else 
		{
			deleteButtons[i].gameObject.SetActive(true);
			newButtons[i].gameObject.SetActive(false);
			playButtons[i].gameObject.SetActive(true);
			profileBoard.transform.FindChild(i + "Name").GetComponent<TextMesh>().text = profiles[i].Name;
			profileBoard.transform.FindChild(i + "Avatar").GetComponent<TextMesh>().text = (profiles[i].AvatarType).ToString();
			profileBoard.transform.FindChild(i + "Outfit").GetComponent<TextMesh>().text = (profiles[i].Outfit).ToString();
			profileBoard.transform.FindChild(i + "Grade").GetComponent<TextMesh>().text = (profiles[i].Grade).ToString();
			string val = (profiles[i].LinkedToJuddly == true) ? ("Yes" ): ("No");
			profileBoard.transform.FindChild(i + "Juddly").GetComponent<TextMesh>().text = val;
			profileBoard.transform.FindChild(i + "Progress").GetComponent<TextMesh>().text = (profiles[i].Planet).ToString() + "%";
		
			for (int j=0; j< crew.Length;j++)
			{
				profileBoard.transform.FindChild(i+"Crew" +j).GetComponent<TextMesh>().text = profiles[i].Crew[j].ToString();
			}
		}
	}
	
	void SetProfileBoardButtonActive(bool on)
	{
		for (int i=0; i < newButtons.Length;i++)
		{
			newButtons[i].isActive = on;
		}
		for (int i=0; i < deleteButtons.Length;i++)
		{
			deleteButtons[i].isActive = on;
		}
		for (int i=0; i < playButtons.Length;i++)
		{
			playButtons[i].isActive = on;
		}
	}
	
	void SetCrewButtonActive(bool on)
	{
		for (int i=0; i < crewButtons.Length;i++)
		{
			crewButtons[i].isActive = on;
		}
	}
	
	public static void SetNextState()
	{
		state = nextState;
	}
	
	void AddCrewMember(int selection, int index)
	{
		crew[index] = (Profile.Avatar)selection;
	}
	
	void RemoveCrewMember(int selection)
	{
		for (int i=0; i < crew.Length;i++)
		{
			if (crew[i] == (Profile.Avatar)selection)
			{
				crew[i] = Profile.Avatar.Null;
			}
		}
	}
}
