using UnityEngine;
using System.Collections;

public class CreateSolarFlare : MonoBehaviour {
	
	public ParticleSystem[] flare;
	
	Mesh mesh;
	float[] timer;
	Vector3[] direction;
	float speed;
	Vector3[] up;
	
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		
		timer = new float[flare.Length];
		direction = new Vector3[flare.Length];
		up = new Vector3[flare.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
		for (int i=0; i < flare.Length;i++)
		{
			if (timer[i] <=0)
			{
				SetFlare(i);
				timer[i] = Random.Range (1f,1.5f);
			}
			else 
			{
				timer[i] -= Time.deltaTime;
				flare[i].transform.position += direction[i]*speed;
				//flare.transform.position -= up*.001f;
				direction[i] -= up[i]*.05f;
			}
		}
	}
	
	void SetFlare(int i)
	{
		int rnd = Random.Range(0,mesh.vertices.Length);
		flare[i].transform.up = mesh.normals[rnd];
		direction[i] = flare[i].transform.up;
		up[i] = direction[i];
		
		float x = Random.Range(-1f,1f);
		float z = Random.Range (-1f,1f);
		
		direction[i] += flare[i].transform.right*x + flare[i].transform.forward*z;
		speed = .5f;
		
		
		flare[i].transform.position = mesh.vertices[rnd]*transform.localScale.x + transform.position;
	}
}
