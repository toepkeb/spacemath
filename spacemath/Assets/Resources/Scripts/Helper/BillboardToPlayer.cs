using UnityEngine;
using System.Collections;

public class BillboardToPlayer : MonoBehaviour {
	
	public bool flip = false;
	public bool useParent;
	public float dist;
	Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (useParent && transform.parent != null)
		{
			Vector3 dir = Vector3.Normalize(player.position - transform.parent.position);
			transform.position = transform.parent.position + dir *dist;
		}
		
		if (flip)
			transform.LookAt(-player.position);
		else
			transform.LookAt(player.position);
	}
}
