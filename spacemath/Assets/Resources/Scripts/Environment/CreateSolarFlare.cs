using UnityEngine;
using System.Collections;

public class CreateSolarFlare : MonoBehaviour {
	
	public ParticleSystem flare;
	
	Mesh mesh;
	float timer;
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timer <=0)
		{
			SetFlare();
			timer = 3;
		}
		else 
		{
			timer -= Time.deltaTime;
		}
	}
	
	void SetFlare()
	{
		int rnd = Random.Range(0,mesh.vertices.Length);
		flare.transform.up = mesh.normals[rnd];
		flare.transform.position = mesh.vertices[rnd]*transform.localScale.x + transform.position;
	}
}
