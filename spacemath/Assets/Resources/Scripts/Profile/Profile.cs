using UnityEngine;
using System.Collections;

public class Profile {
	
	
	public enum Avatar
	{
		Boy, Girl, Dog, Falcon, Monkey, Alligator, Panda, Goldfish, Null
	}
	
	int index;
	string identifier;
	string nickName;
	Avatar avatar;
	int outfit;
	Avatar[] crew;
	StatManager stats;
	int grade;
	bool linkedToJuddly;
	int juddlyUID;
	int juddlyCID;
	
	public int Index{ get{return index;}}
	public string Name{ get{return nickName;} set{ nickName = value;}}
	public Avatar AvatarType { get{return avatar;} set { avatar = value;}}
	public int Outfit { get { return outfit;} set{ outfit = value;}}
	public Avatar[] Crew { get {return crew;} set{crew = value;}}
	public int Grade { get { return grade;} set{grade = value;}}
	public float Progress { get { return CalculateGameProgress();}}
	public bool LinkedToJuddly{ get { return linkedToJuddly;}}
	public StatManager Stats {get { return stats;}}
	
	public void CreateProfile(int index)
	{
		this.index = index;
		identifier = index.ToString();
		nickName = "";
		outfit = -1;
		grade = -1;
		
		stats = new StatManager();
		
		PlayerPrefs.SetInt(identifier,1);
		
		PlayerPrefs.SetInt(identifier + "LinkedToJuddly",0);
	}
	
	public void DeleteProfile(int index)
	{
		this.index = -1;
		nickName = "";
		avatar = Avatar.Null;
		outfit = -1;
		linkedToJuddly = false;
		juddlyUID = -1;
		juddlyCID = -1;
		
		PlayerPrefs.DeleteKey(identifier);
		PlayerPrefs.DeleteKey(identifier+ "Name");
		PlayerPrefs.DeleteKey(identifier+ "Avatar");
		PlayerPrefs.DeleteKey(identifier+ "OutFit");
		PlayerPrefs.DeleteKey(identifier+ "LinkedToJuddly");
		PlayerPrefs.DeleteKey(identifier+ "JuddlyUID");
		PlayerPrefs.DeleteKey(identifier+ "JuddlyCID");
		
		stats.DeleteStats();
	}
	
	void LinkToJuddly(int uid, int cid)
	{
		linkedToJuddly = true;
		juddlyUID = uid;
		juddlyCID = cid;
	}
	
	public void LoadProfile(int index)
	{
		this.index = index;
		identifier = this.index.ToString();
		
		if (!PlayerPrefs.HasKey(identifier))
		{
			this.index = -1;
			//Debug.Log ("No Profile Found");
			return;
		}
		
		nickName = PlayerPrefs.GetString (identifier + "Name");
		avatar = (Avatar)PlayerPrefs.GetInt(identifier + "Avatar");
		outfit = PlayerPrefs.GetInt(identifier + "Outfit");
		
//		crew = new Avatar[3];
//		for (int i=0; i < crew.Length;i++)
//		{
//			crew[i] = (Avatar)PlayerPrefs.GetInt(identifier + "Crew" + i);
//		}
		
		stats = new StatManager();
		stats.LoadStats(index);
		
		if (PlayerPrefs.GetInt(identifier+"LinkedToJuddly")==1)
		{
			linkedToJuddly = true;
			juddlyUID = PlayerPrefs.GetInt(identifier + "JuddlyUID");
			juddlyCID = PlayerPrefs.GetInt(identifier + "JuddlyCID");
		}
		else
			linkedToJuddly = false;
	}
	
	public void SaveProfile()
	{
		PlayerPrefs.SetString (identifier + "Name",nickName);
		PlayerPrefs.SetInt(identifier + "Avatar",(int)avatar);
		PlayerPrefs.SetInt(identifier + "Outfit",outfit);
//		for (int i=0; i < crew.Length;i++)
//		{
//			PlayerPrefs.SetInt(identifier + "Crew" + i, (int)crew[i]);
//		}
		
		stats.SaveStats();
		
		if (linkedToJuddly)
		{
			PlayerPrefs.SetInt(identifier + "LinkedToJuddly",1);
			PlayerPrefs.SetInt(identifier+"JuddlyUID",juddlyUID);
			PlayerPrefs.SetInt(identifier+"JuddlyCID",juddlyCID);
		}
	}
	
	float CalculateGameProgress()
	{
		float progress = 0;
		return progress;
	}
	
}
