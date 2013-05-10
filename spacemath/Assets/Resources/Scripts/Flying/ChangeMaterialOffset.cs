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
		SetArrays();
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
//			if (Mathf.Abs(xoffset[i]) >1)
//				xoffset[i] =0;
			yoffset[i] += y[i]/Time.deltaTime;
//			if (Mathf.Abs (yoffset[i]) > 1)
//				yoffset[i] =0;
		
			mats[i].mainTextureOffset = new Vector2(startX[i]+ xoffset[i],startY[i] + yoffset[i]);	
		}
		
	}
	
	void SetArrays()
	{
		int max =0;
		if (x.Length >max)
			max = x.Length;
		if (y.Length > max)
			max = y.Length;
		if (startX.Length > max)
			max = startX.Length;
		if (startY.Length > max)
			max = startY.Length;
		
		float[] temp = x;
		x = new float[max];
		for (int i=0; i < temp.Length;i++)
		{
			x[i] = temp[i];
		}
		temp = y;
		y = new float[max];
		for (int i=0 ; i < temp.Length;i++)
		{
			y[i] = temp[i];
		}
		
		temp = startX;
		startX = new float[max];
		for (int i=0 ; i < temp.Length;i++)
		{
			startX[i] = temp[i];
		}
		temp = startY;
		startY = new float[max];
		for (int i=0 ; i < temp.Length;i++)
		{
			startY[i] = temp[i];
		}
		
	}
}
