using UnityEngine;
using System.Collections;

public class ChangeMaterialOffset : MonoBehaviour {
	
	public float[] x;
	public float[] y;
	public float[] startX;
	public float[] startY;
	
	Material[] mats;
	float[] xoffset;
	float[] yoffset;
	// Use this for initialization
	void Start () {
		mats = new Material[renderer.materials.Length];
		for (int i=0; i < renderer.materials.Length;i++)
		{
			mats[i] = renderer.materials[i];
		}
		xoffset = new float[x.Length];
		yoffset = new float[x.Length];
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i < x.Length;i++)
		{
			xoffset[i] += x[i]/Time.deltaTime;
			yoffset[i] += y[i]/Time.deltaTime;
		
			mats[i].mainTextureOffset = new Vector2(startX[i]+ xoffset[i],startY[i] + yoffset[i]);	
		}
		
	}
}
