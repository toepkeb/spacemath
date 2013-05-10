using UnityEngine;
using System.Collections;

public class AnswerRing : MonoBehaviour {
	
	[HideInInspector]

	public int val;
	public Cluster cluster;
	public int index;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if (col.gameObject.name == "player")
		{
			
			GameObject.Find ("EquationGenerator").GetComponent<EquationGenerator>().CheckEquationAnswer(index, val);
			cluster.DestroyCluster(gameObject, index);
		}

	}
}
