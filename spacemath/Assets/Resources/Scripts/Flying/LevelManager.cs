using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public int level;
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake()
	{
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Reset()
	{
//		EquationCache.SharedCache.SetEquations(GetEquationCount(), level);
	}
			
	int GetEquationCount()
	{
		int ret =0;
		if (level ==1)
		{
			ret = 10;
		}
		else if (level ==2)
		{
			ret = 10;
		}
		else if (level ==3)
		{
			ret = 10;
		}
		else if (level ==4)
		{
			ret = 10;
		}
		else if (level ==5)
		{
			ret = 10;
		}
		else if (level ==6)
		{
			ret = 10;
		}
		else if (level ==7)
		{
			ret = 15;
		}
		else if (level ==8)
		{
			ret = 15;
		}
		return ret;
	}
}
