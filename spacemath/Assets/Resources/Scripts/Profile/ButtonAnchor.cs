using UnityEngine;
using System.Collections;

public class ButtonAnchor : MonoBehaviour {
	
	public bool isActive;
	public bool clicked;
	
	Transform[] children;
	bool touching;
	float clickTimer;
	// Use this for initialization
	void Start () {
		children = GetComponentsInChildren<Transform>();
		CalculateColliderBounds();
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		if (clicked)
		{
			if (clickTimer <0 )
			{
				clicked = false;
				Debug.Log ("change click");
			}
			else
			{
				clickTimer -= Time.deltaTime;
			}
		}
	}
	
	public void Click()
	{
		if (!isActive)
			return;
		
		clicked = true;
		clickTimer = 1;
		foreach (Transform br in children)
		{
			if (br.gameObject.GetComponent<ButtonReaction>())
				br.gameObject.GetComponent<ButtonReaction>().Click();
			
		}
	}
	
	public void TouchDown()
	{
		if (!isActive)
			return;
		
		touching = true;
		
		foreach (Transform br in children)
		{
			if (br.gameObject.GetComponent<ButtonReaction>())
			{
				br.gameObject.GetComponent<ButtonReaction>().TouchDown();
			}
			
		}
	}
	
	public void ButtonHeld()
	{
		if (!isActive)
			return;
		
		touching = true;
		
	}
	
	public void Canceled()
	{
		if (!isActive || touching == false)
			return;
		
		touching = false;
		
		foreach (Transform br in children)
		{
			if (br.gameObject.GetComponent<ButtonReaction>())
				br.gameObject.GetComponent<ButtonReaction>().Canceled();
		}
	}
	
	public bool DetectClick()
	{
		if (clicked)
		{
			clicked = false;
			return true;
		}
		else
			return false;
	}
	void CalculateColliderBounds()
	{
		Vector3 min = Vector3.one *Mathf.Infinity;
		Vector3 max = Vector3.one *-Mathf.Infinity;
		int count = 0;
		
		foreach (Transform t in children)
		{
			BoxCollider col = t.GetComponent<BoxCollider>();
			if (col)
			{
				count ++;
				if (col.bounds.min.x < min.x)
					min.x = col.bounds.min.x;
				if (col.bounds.min.y < min.y)
					min.y = col.bounds.min.y;
				if (col.bounds.min.z < min.z)
					min.z = col.bounds.min.z;
				
				if (col.bounds.max.x > max.x)
					max.x = col.bounds.max.x;
				if (col.bounds.max.y > max.y)
					max.y = col.bounds.max.y;
				if (col.bounds.max.z > max.z)
					max.z = col.bounds.max.z;
			}
		}
		if (count ==0)
			return;
		
		BoxCollider box;
		
		box = GetComponent<BoxCollider>();
		
		if (box == null)
			box = gameObject.AddComponent<BoxCollider>();
		
		box.size = max-min;
		box.center = (max + min)/2 - transform.position;;

		
	}
}
