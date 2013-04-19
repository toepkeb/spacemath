using UnityEngine;
using System.Collections;

public class ProfileManager : MonoBehaviour {
	
	GameObject[] avatars;
	GameObject avatarParent;
	public GameObject profileBoard;
	float avatarOffset = 3;
	Vector2 startPosition;
	float startTime;
	Vector2 previousPosition;
	Vector3 target;
	int selection;
	
	enum State
	{
		SelectProfile, CreateName, SelectAvatar, SelectOutfit, SelectCrew, ConnectToJuddly, Stats
	}
	State state;
	
	Profile[] profiles;
	Profile currentProfile;
	public static int currentProfileIndex;
	
	public ButtonAnchor[] newButtons;
	public ButtonAnchor[] deleteButtons;
	public ButtonAnchor[] playButtons;
	
	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll();
		profiles = new Profile[4];
		for (int i=0; i < profiles.Length;i++)
		{
			profiles[i] = new Profile();
			profiles[i].LoadProfile(i);
			SetProfileBoard(i);
		}
		
		SkillDataBase.SetSkills();
		
		SetButtonActive(true);
		
		//SetProfileBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (state == State.SelectProfile)
		{
			for (int i=0; i < newButtons.Length;i++)
			{
				if (newButtons[i].DetectClick())
				{
					Debug.Log ("clicked");
					currentProfileIndex = i;
					state = State.CreateName;
					profiles[currentProfileIndex].CreateProfile(i);
					target = Vector3.zero;
					SetButtonActive(false);
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
				}
			}
		}
		if (state == State.SelectAvatar)
		{
			if (Input.GetMouseButtonDown(0))
			{
				previousPosition = Input.mousePosition;
				startTime = Time.time;
				startPosition = Input.mousePosition;
			}
			else if (Input.GetMouseButton(0))
			{
				avatarParent.transform.position += new Vector3((-previousPosition.x+Input.mousePosition.x)/100,0,0);
				previousPosition = Input.mousePosition;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				float vel = (Input.mousePosition.x - startPosition.x)/(Time.time-startTime);
				vel /=Screen.width;
				
				if (Mathf.Abs(Input.mousePosition.x - startPosition.x) > Screen.width*.25f)
				{
					selection = (int)((avatarParent.transform.position.x-avatarOffset/2)/avatarOffset);
				}
				else if (Mathf.Abs(vel) > .4f)
				{
					//int offset = (int)((avatarParent.transform.position.x-avatarOffset/2)/avatarOffset);
					if (vel >0)
					{
						selection ++;
					}
					else
					{
						selection --;
					}
					Debug.Log ("swipe");
				}
				Debug.Log ("2 " + selection);
				selection = Mathf.Clamp(selection,-avatars.Length+1,0);
				Debug.Log ("3 " + selection);
				target = new Vector3(selection*avatarOffset,0,0);
			}
			else
			{
				avatarParent.transform.position = Vector3.Lerp (avatarParent.transform.position,target,.1f);
			}
		}	
	}
	
	void OnGUI()
	{
		switch(state)
		{
			#region Select Profile
			case State.SelectProfile:
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
			case State.CreateName:
			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Nickname");
			profiles[currentProfileIndex].Name = GUI.TextField( new Rect(Screen.width*.5f - 150,Screen.height*.5f,300,50),profiles[currentProfileIndex].Name);
			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
			{
				state = State.SelectAvatar;
				
				if (avatars == null)
				{
					avatarParent = new GameObject();
					avatars = LoadAvatars();
					
					for (int i=0; i < avatars.Length;i++)
					{
						avatars[i] = (GameObject)GameObject.Instantiate(avatars[i],new Vector3(i*avatarOffset,0,0),Quaternion.identity);
						avatars[i].transform.parent = avatarParent.transform;
					}
				}
			}
			break;
			#endregion
			#region SelectAvatar
			case State.SelectAvatar:
			
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
			
			
			
			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
			{
				profiles[currentProfileIndex].AvatarType = (Profile.Avatar)(selection*-1);
				state = State.SelectOutfit;
			}
			break;
			#endregion
			#region SelectOutfit
			case State.SelectOutfit:
			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Outfit");
			for (int i=0; i < 8;i++)
			{
				Rect imgRect = new Rect(200+(i*80),Screen.height*.5f,80,80);
				if (GUI.Button(imgRect,i.ToString()))
				{
					profiles[currentProfileIndex].Outfit = i;
				}
			}
			
			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
			{
				state = State.ConnectToJuddly;
			}
			break;
			#endregion
			#region Select Crew
			case State.SelectCrew:
			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Select your Outfit");
			for (int i=0; i < 8;i++)
			{
				Rect imgRect = new Rect(200+(i*80),Screen.height*.5f,80,80);
				if (GUI.Button(imgRect,i.ToString()))
				{
					profiles[currentProfileIndex].Outfit = i;
				}
			}
			
			if (GUI.Button (new Rect(Screen.width*.5f -30,Screen.height*.6f, 60,50),"Continue"))
			{
				state = State.ConnectToJuddly;
			}
			break;
			#endregion
			#region ConnectToJuddly
			case State.ConnectToJuddly:
			GUI.Label(new Rect(Screen.width*.5f - 150, Screen.height*.4f, 300,50),"Connect to Juddly");
			
			if (GUI.Button(new Rect(Screen.width*.4f-30,Screen.height* .5f,60,50),"Yes"))
			{
				state = State.SelectProfile;
				profiles[currentProfileIndex].SaveProfile();
				SetProfileBoard(currentProfileIndex);
				SetButtonActive(true);
			}
			if (GUI.Button(new Rect(Screen.width*.6f-30, Screen.height*.5f,60,50),"NO"))
			{
				state = State.SelectProfile;
				SetProfileBoard(currentProfileIndex);
				profiles[currentProfileIndex].SaveProfile();
				SetButtonActive(true);	
			}
		
			break;
			#endregion
			#region Stats
			case State.Stats:
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
				state = State.SelectProfile;
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
		
//		for (int i=0; i < profiles.Length;i++)
//		{
			if (profiles[i].Index == -1)
			{
				//profileBoard.transform.FindChild(i + "New").GetComponent<TextMesh>().text = "NEW";
				//profileBoard.transform.FindChild(i + "New").GetComponent<ButtonReaction>().enabled = true;
				deleteButtons[i].gameObject.SetActive(false);
				newButtons[i].gameObject.SetActive(true);
				playButtons[i].gameObject.SetActive(false);
				profileBoard.transform.FindChild(i + "Name").GetComponent<TextMesh>().text = "";
				profileBoard.transform.FindChild(i + "Avatar").GetComponent<TextMesh>().text = "";
				profileBoard.transform.FindChild(i + "Outfit").GetComponent<TextMesh>().text = "";
				profileBoard.transform.FindChild(i + "Grade").GetComponent<TextMesh>().text ="";
				profileBoard.transform.FindChild(i + "Juddly").GetComponent<TextMesh>().text = "";
				profileBoard.transform.FindChild(i + "Progress").GetComponent<TextMesh>().text = "";
			}
			else 
			{
				//profileBoard.transform.FindChild(i + "New").GetComponent<TextMesh>().text = "";
				//profileBoard.transform.FindChild(i + "New").GetComponent<ButtonReaction>().enabled = false;
				deleteButtons[i].gameObject.SetActive(true);
				newButtons[i].gameObject.SetActive(false);
				playButtons[i].gameObject.SetActive(true);
				profileBoard.transform.FindChild(i + "Name").GetComponent<TextMesh>().text = profiles[i].Name;
				profileBoard.transform.FindChild(i + "Avatar").GetComponent<TextMesh>().text = (profiles[i].AvatarType).ToString();
				profileBoard.transform.FindChild(i + "Outfit").GetComponent<TextMesh>().text = (profiles[i].Outfit).ToString();
				profileBoard.transform.FindChild(i + "Grade").GetComponent<TextMesh>().text = (profiles[i].Grade).ToString();
				string val = (profiles[i].LinkedToJuddly == true) ? ("Yes" ): ("No");
				profileBoard.transform.FindChild(i + "Juddly").GetComponent<TextMesh>().text = val;
				profileBoard.transform.FindChild(i + "Progress").GetComponent<TextMesh>().text = (profiles[i].Progress).ToString() + "%";
//				GUI.Label (new Rect(25,50,200,30),"Name: " + profiles[i].Name);
//				GUI.Label (new Rect(25,80,200,30),"Avatar: " + (profiles[i].AvatarType).ToString());
//				GUI.Label (new Rect(25,110,200,30),"Outfit: " + profiles[i].Outfit);
//				GUI.Label (new Rect(25,140,200,30),"Grade: " + profiles[i].Grade);
//				string val = (profiles[i].LinkedToJuddly == true) ? ("Yes" ): ("No");
//				GUI.Label (new Rect(25,170,200,30),"Connected to Juddly: " + val);
//			
//				if (GUI.Button (new Rect(65,200,120,30),"Delete"))
//				{
//					profiles[i].DeleteProfile(i);
//				}
//				if (GUI.Button (new Rect(10,200,50,30),"Stats"))
//				{
//					state = State.Stats;
//				}
//			}
		}
	}
	
	void SetButtonActive(bool on)
	{
		for (int i=0; i < newButtons.Length;i++)
		{
			newButtons[i].active = on;
		}
		for (int i=0; i < deleteButtons.Length;i++)
		{
			deleteButtons[i].active = on;
		}
		for (int i=0; i < playButtons.Length;i++)
		{
			playButtons[i].active = on;
		}
	}
}
