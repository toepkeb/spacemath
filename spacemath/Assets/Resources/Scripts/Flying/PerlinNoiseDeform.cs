// This script is placed in public domain. The author takes no responsibility for any possible harm.
using UnityEngine;

public class PerlinNoiseDeform : MonoBehaviour {
	public float scale = .03f;
	public float speed = .5f;
	bool recalculateNormals = false;
	
	private Vector3[] baseVertices;
	
	void Start ()
	{
		//noise = new Perlin ();
	}
	
	void Update () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		
		if (baseVertices == null)
			baseVertices = mesh.vertices;
			
		Vector3[] vertices = new Vector3[baseVertices.Length];
		
		float timex = Time.time * speed + 0.1365143f;
		float timey = Time.time * speed + 1.21688f;
		float timez = Time.time * speed + 2.5564f;
		for (int i=0;i<vertices.Length;i++)
		{
			Vector3 vertex = baseVertices[i];
					
			vertex.x += Mathf.PerlinNoise(timex + vertex.x, timex + vertex.y) * scale;
			vertex.y += Mathf.PerlinNoise(timey + vertex.x, timey + vertex.y) * scale;
			vertex.z += Mathf.PerlinNoise(timez + vertex.x, timez + vertex.y) * scale;
			
			vertices[i] = vertex;
		}
		
		mesh.vertices = vertices;
		
		if (recalculateNormals)	
			mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		
	}
}
