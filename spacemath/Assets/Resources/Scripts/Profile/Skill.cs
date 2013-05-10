using UnityEngine;
using System.Collections;

public class Skill{
	
	int index;
	int type;
	int val;
	float progress;
	float maxProgress;
	bool mastered;
	bool loaded;
	int user;
	
	public int Type {get {return type;}}
	public int Value{get {return val;}}
	public float Progress {get {return progress;}}
	public bool Mastered {get {return mastered;}}
	
	public Skill(int type, int val, float max)
	{
		this.type = type;
		this.val = val;
		this.maxProgress = max;
		loaded = false;
	}
	
	public void AddProgress(int user, float amt)
	{
		if (!loaded)
			LoadSkill(user);
		
		progress += amt;
		
		if (!mastered && progress > maxProgress)
		{
			mastered = true;
			Debug.Log ("Skill mastered!");
		}
		
		SaveSkill(this.user);
	}
	
	public void SaveSkill(int user)
	{
		PlayerPrefs.SetFloat(user.ToString() + type.ToString() + val.ToString(), progress);
	}
	
	public void LoadSkill(int user)
	{
		if (loaded)
			return;
		
		progress = PlayerPrefs.GetFloat(user.ToString() + type.ToString() + val.ToString());
		loaded = true;
		this.user = user;
		
		if (progress > maxProgress)
			mastered = true;
	}
	
	public void DeleteSkill(int user)
	{
		PlayerPrefs.DeleteKey(user.ToString() + type.ToString() + val.ToString());
	}
}

public class SkillDataBase
{
	public static Hashtable skills;
	public static int skill0Count;
	public static int skill1Count;
	public static int skill2Count;
	public static int skill3Count;
	public static int skill4Count;
	public static int skill5Count;
	public static int skill6Count;
	public static int skill7Count;
	
	public static void SetSkills()
	{
		skills = new Hashtable();
		
		//Number Value Skills
		skill0Count = 11;
		skills.Add("0/0",new Skill(0,0,1));
		skills.Add("0/1",new Skill(0,1,1));
		skills.Add("0/2",new Skill(0,2,1));
		skills.Add("0/3",new Skill(0,3,1));
		skills.Add("0/4",new Skill(0,4,1));
		skills.Add("0/5",new Skill(0,5,1));
		skills.Add("0/6",new Skill(0,6,1));
		skills.Add("0/7",new Skill(0,7,1));
		skills.Add("0/8",new Skill(0,8,1));
		skills.Add("0/9",new Skill(0,9,1));
		skills.Add("0/10",new Skill(0,10,1));
		
		//Pattern Skills
		skill1Count = 5;
		skills.Add("1/0",new Skill(1,0,1));
		skills.Add("1/1",new Skill(1,1,1));
		skills.Add("1/2",new Skill(1,2,1));
		skills.Add("1/3",new Skill(1,3,1));
		skills.Add("1/4",new Skill(1,4,1));
		
		//Multiplication Arrays
		skill2Count = 9;
		skills.Add("2/1",new Skill(2,1,2));
		skills.Add("2/2",new Skill(2,2,2));
		skills.Add("2/3",new Skill(2,3,2));
		skills.Add("2/4",new Skill(2,4,2));
		skills.Add("2/5",new Skill(2,5,2));
		skills.Add("2/6",new Skill(2,6,2));
		skills.Add("2/7",new Skill(2,7,2));
		skills.Add("2/8",new Skill(2,8,2));
		skills.Add("2/9",new Skill(2,9,2));
		
		//Addition
		skill3Count = 19;
		skills.Add("3/0",new Skill(3,0,1));
		skills.Add("3/1",new Skill(3,1,1));
		skills.Add("3/2",new Skill(3,2,1));
		skills.Add("3/3",new Skill(3,3,1));
		skills.Add("3/4",new Skill(3,4,1));
		skills.Add("3/5",new Skill(3,5,1));
		skills.Add("3/6",new Skill(3,6,1));
		skills.Add("3/7",new Skill(3,7,1));
		skills.Add("3/8",new Skill(3,8,1));
		skills.Add("3/9",new Skill(3,9,1));
		skills.Add("3/10",new Skill(3,10,1));
		skills.Add("3/20",new Skill(3,20,1));
		skills.Add("3/30",new Skill(3,30,1));
		skills.Add("3/40",new Skill(3,40,1));
		skills.Add("3/50",new Skill(3,50,1));
		skills.Add("3/60",new Skill(3,60,1));
		skills.Add("3/70",new Skill(3,70,1));
		skills.Add("3/80",new Skill(3,80,1));
		skills.Add("3/90",new Skill(3,90,1));
		
		//Subtraction
		skill4Count = 19;
		skills.Add("4/0",new Skill(4,0,1));
		skills.Add("4/1",new Skill(4,1,1));
		skills.Add("4/2",new Skill(4,2,1));
		skills.Add("4/3",new Skill(4,3,1));
		skills.Add("4/4",new Skill(4,4,1));
		skills.Add("4/5",new Skill(4,5,1));
		skills.Add("4/6",new Skill(4,6,1));
		skills.Add("4/7",new Skill(4,7,1));
		skills.Add("4/8",new Skill(4,8,1));
		skills.Add("4/9",new Skill(4,9,1));
		skills.Add("4/10",new Skill(4,10,1));
		skills.Add("4/20",new Skill(4,20,1));
		skills.Add("4/30",new Skill(4,30,1));
		skills.Add("4/40",new Skill(4,40,1));
		skills.Add("4/50",new Skill(4,50,1));
		skills.Add("4/60",new Skill(4,60,1));
		skills.Add("4/70",new Skill(4,70,1));
		skills.Add("4/80",new Skill(4,80,1));
		skills.Add("4/90",new Skill(4,90,1));
		
		//Multiplication
		skill5Count = 10;
		skills.Add("5/0",new Skill(5,0,2));
		skills.Add("5/1",new Skill(5,1,2));
		skills.Add("5/2",new Skill(5,2,2));
		skills.Add("5/3",new Skill(5,3,2));
		skills.Add("5/4",new Skill(5,4,2));
		skills.Add("5/5",new Skill(5,5,2));
		skills.Add("5/6",new Skill(5,6,2));
		skills.Add("5/7",new Skill(5,7,2));
		skills.Add("5/8",new Skill(5,8,2));
		skills.Add("5/9",new Skill(5,9,2));
		
		//Division
		skill6Count = 9;
		skills.Add("6/1",new Skill(6,1,2));
		skills.Add("6/2",new Skill(6,2,2));
		skills.Add("6/3",new Skill(6,3,2));
		skills.Add("6/4",new Skill(6,4,2));
		skills.Add("6/5",new Skill(6,5,2));
		skills.Add("6/6",new Skill(6,6,2));
		skills.Add("6/7",new Skill(6,7,2));
		skills.Add("6/8",new Skill(6,8,2));
		skills.Add("6/9",new Skill(6,9,2));
		
	}
	
	public static Skill GetSkill(int type, int val)
	{
		Skill ret = (Skill)skills[type.ToString()+"/"+val.ToString()];
		
		ret.LoadSkill(ProfileManager.currentProfileIndex);
		
		return ret;
	}
}
