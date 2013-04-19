using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour {

	// all of our active trackers
	public static List<TouchTracker> trackers = new List<TouchTracker>();
	
	// index is finger id, value is a TouchTracker
	Hashtable trackerLookup = new Hashtable();
		
	Vector2 offset;
	/**
	* Use widescreen orientation
	*/
	void Awake()
	{
	}
	
	/**
	* Update all of our trackers
	*/
	public void Update()
	{
		// clean all touches (so they know if they aren't updated after we pull info)
		foreach(TouchTracker tracker in trackers)
			tracker.Clean();
		
		// process our touches
		foreach(Touch touch in Input.touches)
		{
			// try to get our tracker for this finger id
			TouchTracker tracker = (TouchTracker)trackerLookup[touch.fingerId];
			
			if(tracker != null)
				tracker.UpdateTouch(touch, 1);
			else
				tracker = BeginTracking(touch);
				
			// iPhoneTouchPhase.Ended isn't very reliable (especially with the remote)
			/*
			if(touch.phase == iPhoneTouchPhase.Ended)
				EndTracking(tracker);
			*/
		}
		
		// track which events vanished (without using iPhoneTouchPhase.Ended)
		List<TouchTracker> ended = new List<TouchTracker>();
		
		// use an intermediate list because EndTracking removes from trackers arraylist
		foreach (TouchTracker tracker in trackers)
			if(!tracker.isDirty)
				ended.Add(tracker);
		
		foreach(TouchTracker tracker in ended)
			EndTracking(tracker);
		
	}
	
	/**
	* Start up a tracker for a fingerid
	*/
	public TouchTracker BeginTracking(Touch touch)
	{
		TouchTracker tracker = new TouchTracker(touch);
		
		// remember our tracker
		trackers.Add(tracker);
		trackerLookup[touch.fingerId] = tracker;
		return tracker;
	}
	
	/**
	* Clear a tracker from being updated, tell it to stop
	*/
	public void EndTracking(TouchTracker tracker)
	{
		tracker.End();
		
		trackers.Remove(tracker);
		trackerLookup[tracker.fingerId] = null;
	}

}
