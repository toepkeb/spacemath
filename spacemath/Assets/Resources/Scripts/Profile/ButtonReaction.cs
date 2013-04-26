using UnityEngine;
using System.Collections;

public class ButtonReaction : MonoBehaviour {
	
	public float sizeChange;
	public float changeTime;
	public string activeTexturePath;
	public AnimationClip animationClip;
	
	public bool useForBounds;
	bool touching;
	Vector3 startSize;
	Texture startingTexture;
	Texture activeTexture;
	Transform parent;
	
	// Use this for initialization
	void Awake () {
		
		if (useForBounds && !GetComponent<BoxCollider>())
		{
			gameObject.AddComponent<BoxCollider>();
		}
		
		if (animationClip && !GetComponent<Animation>())
		{
			gameObject.AddComponent<Animation>();
			gameObject.animation.AddClip(animationClip,animationClip.name);
			gameObject.animation.clip = animationClip;
			gameObject.animation.playAutomatically = false;
			gameObject.animation.wrapMode = WrapMode.Once;
		}
		
		if (transform.parent != null)
		{
			parent = transform.parent;
			startSize = parent.localScale;
		}
		
		if (activeTexturePath != "")
		{
			startingTexture = gameObject.renderer.material.mainTexture;
		}
	}
	
	void OnEnabled()
	{
		parent.localScale = startSize;
		gameObject.renderer.material.mainTexture = startingTexture;
		touching = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (touching)
		{
			Resize();
		}
		else
			ReturnToOriginalSize();
		
	}
	
	public void TouchDown()
	{
		touching = true;
		
		if (activeTexturePath != "")
		{
			activeTexture = (Texture) Resources.Load (activeTexturePath);
			gameObject.renderer.material.mainTexture = activeTexture;
		}
	}
	
	public void Click()
	{
		touching = false;
		
		if (activeTexturePath != "")
		{
			gameObject.renderer.material.mainTexture = startingTexture;
		}
		
		if (animationClip)
			gameObject.animation.Play();
	}
	
	public void Canceled()
	{
		touching = false;
		
		if (activeTexturePath != "")
		{
			gameObject.renderer.material.mainTexture = startingTexture;
		}
	}
	
	void Resize()
	{
		if (parent.localScale == startSize - Vector3.one*sizeChange || parent == null || changeTime == 0)
		{
			return;
		}
		float delta = sizeChange *Time.deltaTime/ changeTime;
		
		parent.localScale -= new Vector3(delta,delta,delta);	
		if (Mathf.Abs (startSize.x-parent.localScale.x - sizeChange)  < Mathf.Abs(delta))
		{
			parent.localScale = startSize - Vector3.one*sizeChange;
		}
	}
	
	void ReturnToOriginalSize()
	{
		if (parent.localScale == startSize || parent == null || changeTime == 0)
			return;
		
		float delta = sizeChange *Time.deltaTime/ changeTime;
		parent.localScale += new Vector3(delta,delta,delta);	
		if (Mathf.Abs(parent.localScale.x - startSize.x) < Mathf.Abs(delta))
		{
			parent.localScale = startSize;
		}
	}
}
