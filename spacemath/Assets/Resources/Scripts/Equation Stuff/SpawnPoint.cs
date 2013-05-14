using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	
	public int min;
	public int max;
	public int type;
	public int count;
	public bool shapes = false;
	public int minAnswers=2;
	public int maxAnswers=6;
	public Lesson lesson;
	public Lesson.Pattern pattern;
	public Lesson.PatternStyle style;
	
	void Awake()
	{
		minAnswers = Mathf.Clamp(minAnswers,2,6);
		maxAnswers = Mathf.Clamp(maxAnswers,2,6);
		
		if (pattern == Lesson.Pattern.Null)
			lesson = new Lesson(min, max+1, type, count, shapes, minAnswers, maxAnswers);
		else
			lesson = new Lesson(min, max+1, type, count, shapes, minAnswers, maxAnswers, pattern, style);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
