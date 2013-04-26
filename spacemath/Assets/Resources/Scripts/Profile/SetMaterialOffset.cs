using UnityEngine;
using System.Collections;

public class SetMaterialOffset : MonoBehaviour {
	
	public Vector2 offset;
	public Vector2 tiling;
	// Use this for initialization
	void Start () {
		renderer.material.mainTextureOffset = offset;
		renderer.material.mainTextureScale = tiling;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
