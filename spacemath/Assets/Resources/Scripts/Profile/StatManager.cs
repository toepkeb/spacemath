using UnityEngine;
using System.Collections;

public class StatManager {
	
	int profileIndex;
	Hashtable stats;
	
	public void AddProgress(string key, int val)
	{
		if (stats == null)
			stats = new Hashtable();
		
		if (stats.ContainsKey(key))
		{
			int temp = (int)stats[key];
			temp += val;
			stats[key] = temp;
		}
		else
		{
			stats.Add(key, val);
		}
		//Debug.Log (key + " has a value of: " + stats[key]);
		
	}
	
	public void SaveStats()
	{
		//if the stats is null, then return
		if (stats == null)
			return;
		
		//Save all the stat entries
		int count = 0;
		foreach (DictionaryEntry de in stats)
		{
			PlayerPrefs.SetString(profileIndex.ToString() + "Key" + count.ToString(), de.Key.ToString());
			PlayerPrefs.SetInt(profileIndex.ToString()+ "Value"+count.ToString(), (int)de.Value);
			count ++;
		}
		PlayerPrefs.SetInt(profileIndex.ToString() + "EntryCount",count);
		
	}
	
	public void LoadStats(int index)
	{
		profileIndex = index;
		int count = PlayerPrefs.GetInt(profileIndex.ToString() + "EntryCount");
		
		//Load all the stats
		for (int i=0; i < count;i++)
		{
			string key = PlayerPrefs.GetString(profileIndex.ToString() + "Key" + i.ToString());
			int val = PlayerPrefs.GetInt(profileIndex.ToString() + "Value" + i.ToString());
			
			AddProgress(key, val);
		}
		
	}
	
	public void DeleteStats()
	{
		if (stats == null)
			return;
		
		int count = PlayerPrefs.GetInt(profileIndex.ToString() + "EntryCount");
		
		for (int i=0; i < count;i++)
		{
			PlayerPrefs.DeleteKey(profileIndex.ToString() + "Key" + count.ToString());
			PlayerPrefs.DeleteKey(profileIndex.ToString()+ "Value"+count.ToString());
		}
		PlayerPrefs.DeleteKey(profileIndex.ToString() + "EntryCount");
	}
	
	void OnGUI()
	{
	}
}


//Save in player prefs with the following convention
// <profileIndex>Key<index> = the key for object at index
// <profileIndex>Value<index> = the matching for for the key at index
// <profileIndex>EntryCount = the number of keys and value pairs