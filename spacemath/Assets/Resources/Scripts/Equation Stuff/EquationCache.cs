using UnityEngine;
using System.Collections;

public class EquationCache {
	
	static EquationCache sharedCache;
	public Equation[] equationCache;
	public int equationCount;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static EquationCache SharedCache
	{
		get
		{
			if (sharedCache == null)
				sharedCache = new EquationCache();
			
			return sharedCache;
		}
	}
	
	public void SetEquations()
	{
		equationCache = new Equation[equationCount];
		
		//Lesson currentLesson = GetLesson(ProfileManager.currentProfile.Grade,ProfileManager.currentProfile.Planet);
		Lesson currentLesson = GetLesson(1,1);
		
		for (int i=0; i < equationCount;i++)
		{
			equationCache[i] = new Equation(currentLesson, GetNumOfAnswers());
		}
	}
	
	public Equation ChangeSingleEquation(int index)
	{
		equationCache[index] = new Equation(GetLesson(1,1), GetNumOfAnswers());
		return equationCache[index];
	}
	
	Lesson GetLesson(int grade, int planet)
	{
		if (grade ==1 && planet == 1)
			return new Lesson(0,9,3,2,false);
		
		return null;
	}
	
	public Equation GetEquation(int index)
	{
		if (index >= equationCache.Length)
			return null;
		
		return equationCache[index];
	}
	
	int GetNumOfAnswers()
	{
//		int planet = ProfileManager.currentProfile.Planet;
		int planet = 1;
		
		if (planet ==1)
		{
			return Random.Range(2,7);
		}
		if (planet ==2)
		{
			return Random.Range(2,3);
		}
		if (planet ==3)
		{
			return Random.Range(2,4);
		}
		if (planet ==4)
		{
			return Random.Range(3,4);
		}
		if (planet ==5)
		{
			return Random.Range(3,5);
		}
		if (planet ==6)
		{
			return Random.Range(3,5);
		}
		if (planet ==7)
		{
			return Random.Range(3,6);
		}
		if (planet ==8)
		{
			return Random.Range(3,6);
		}
		if (planet ==9)
		{
			return Random.Range(4,6);
		}
		return 0;
	}
}

public class Lesson
{
	public int type;
	public int min;
	public int max;
	public int count;
	public bool shapes;
	
	public Lesson(int min, int max, int type, int count, bool shapes)
	{
		this.min = min;
		this.max = max;
		this.type = type;
		this.count = count;
		this.shapes = shapes;
	}
	
}



