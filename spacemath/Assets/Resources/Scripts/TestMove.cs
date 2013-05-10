using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMove : MonoBehaviour {
	
	public List<GameObject> free;
	public GameObject next;
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.P))
		{
			SplineController sc = GetComponent<SplineController>();
			sc.SplineRoot = GameObject.Find ("Spline2");
			sc.Start ();
		}
	}
	
	public GameObject GetSpline()
	{
		int rnd = Random.Range(0,free.Count);
		
		GameObject spline = free[rnd];
		free.RemoveAt(rnd);
		spline.transform.position = next.transform.GetChild(next.transform.childCount-1).position;
		
		GameObject ret = next;
		next = spline;
		return ret;
	}
	
	public void FreeSpline(GameObject spline)
	{
		free.Add(spline);
		Debug.Log (spline);
		spline.SetActive(true);
		for (int i=0; i < spline.transform.childCount;i++)
			spline.transform.GetChild(i).gameObject.SetActive(true);
	}
}
