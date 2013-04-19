using UnityEngine;
using System.Collections;

public class TouchTracker {
	// expose our finger id for convenience
	public int fingerId;
	
	// our last data from Unity
	public Touch touch;
	
	// have we been updated this frame?
	public bool isDirty;
	
	// track how long we've been active
	public float totalTime;
	
	// where we started
	public Touch firstTouch;
	
	
	/**
	* Constructor
	*/
	public TouchTracker(Touch firstTouch)
	{
		this.fingerId = firstTouch.fingerId;
		this.firstTouch = firstTouch;
		
		Begin();
		UpdateTouch(firstTouch, 0);
	}
	
	/**
	* We have new touch data
	*/
	public void UpdateTouch(Touch touch, int i)
	{
		this.touch = touch;
		isDirty = true;
		
		totalTime += Time.deltaTime;
		
	}
	
	/**
	* Called before our manager updates everything
	*/
	public void Clean()
	{
		isDirty = false;
	}
	
	/**
	* Callback when we start
	*/
	public void Begin()
	{
		//Debug.Log("started tracking finger " + fingerId);
		
	}
	
	/**
	* Clean things up
	*/
	public void End()
	{
		//Debug.Log("ended tracking finger " + fingerId);
				
		// how far did we move?
		Vector2 movement = touch.position - firstTouch.position;
		movement /= totalTime;
		//Debug.Log (totalTime);
		//Debug.Log("moved " + movement);
		
		// test for *very* simple swipe detection
//		if(movement.x > 200 && Mathf.Abs(movement.y) < 200)
//			Debug.Log("swipe right");
//		if(movement.x < 200 && Mathf.Abs(movement.y) < 200)
//			Debug.Log("swipe left");
		
	}
}

