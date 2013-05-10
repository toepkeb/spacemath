using UnityEngine;
using System.Collections;

//Use this script to destroy an Object when the any of the specificed gameObject tags collide with it

public class DestroyTrigger : MonoBehaviour {

	public string[] tags;
	public bool destroyHighestLevelParent;
	
	bool marked;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (marked)
			
			if (destroyHighestLevelParent)
				HasParent(transform);
			else
				Destroy (gameObject);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		for (int i=0; i < tags.Length;i++)
		{
			if (collider.tag == tags[i])
			{
				marked = true;
			}
		}
	}
	
	void HasParent(Transform go)
	{
		if (go.parent == null)
		{
			Destroy (go.gameObject);
		}
		else
		{
			HasParent(go.parent);
		}
	}
				
}
