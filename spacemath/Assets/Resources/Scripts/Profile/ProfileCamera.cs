using UnityEngine;
using System.Collections;

public class ProfileCamera : MonoBehaviour {
	
	bool move;
	Vector3 direction;
	float speed;
	Vector3[] targets;
	Vector3 currentTarget;
	Transform _transform;
	
	float avatarOffset = 3;
	Vector2 startPosition;
	float startTime;
	Vector2 previousPosition;
	Vector3 target;
	
	public GameObject frameBot;
	public GameObject frameTop;
	public int selection;
	
	// Use this for initialization
	void Start () {
		GameObject pos = GameObject.Find ("CameraPositions");
		targets = new Vector3[pos.transform.childCount];
		for (int i=0; i < targets.Length;i++)
		{
			targets[i] = pos.transform.FindChild(i.ToString()).position;
		}
		
		_transform = transform;
		transform.position = targets[0];
	}
	
	// Update is called once per frame
	void Update () {
	
		if (move)
			Move ();
		
		if (ProfileManager.state == ProfileManager.ProfileManagerState.SelectAvatar)
		{
			if (Input.GetMouseButtonDown(0))
			{
				previousPosition = Input.mousePosition;
				startTime = Time.time;
				startPosition = Input.mousePosition;
			}
			else if (Input.GetMouseButton(0))
			{
				_transform.position -= new Vector3((-previousPosition.x+Input.mousePosition.x)/100,0,0);
				previousPosition = Input.mousePosition;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				float vel = (Input.mousePosition.x - startPosition.x)/(Time.time-startTime);
				vel /=Screen.width;
				
				if (Mathf.Abs(Input.mousePosition.x - startPosition.x) > Screen.width*.25f)
				{
					selection = (int)((_transform.position.x-avatarOffset/2)/avatarOffset);
					selection++;
				}
				else if (Mathf.Abs(vel) > .4f)
				{
					//int offset = (int)((avatarParent.transform.position.x-avatarOffset/2)/avatarOffset);
					if (vel >0)
					{
						selection --;
					}
					else
					{
						selection ++;
					}
					Debug.Log ("swipe");
				}
				selection = Mathf.Clamp(selection,0,7);
				target = new Vector3(selection*avatarOffset,_transform.position.y,_transform.position.z);
			}
			else
			{
				_transform.position = Vector3.Lerp (_transform.position,target,.1f);
			}
		}
		else if (ProfileManager.state == ProfileManager.ProfileManagerState.SelectCrew)
		{
			move = false;
			_transform.position = Vector3.Lerp (_transform.position,target,.1f);
		}
	}
	
	void Move()
	{
		_transform.position += direction*speed;
		if (Vector3.SqrMagnitude(_transform.position - currentTarget)<speed*speed)
		{
			Debug.Log ("stop moving");
			move = false;
			_transform.position = currentTarget;
			ProfileManager.SetNextState();
		}
		
		if (ProfileManager.nextState == ProfileManager.ProfileManagerState.SelectCrew)
		{
			AnimateFrame(1);
		}
		else
		{
			AnimateFrame(-1);
		}
	}
	
	void AnimateFrame(int direction)
	{
		frameBot.transform.localPosition += Vector3.up * direction *.02f;
		frameTop.transform.localPosition += Vector3.down *direction *.02f;
		if (frameBot.transform.localPosition.y < -2)
		{
		Debug.Log ("stop animate1");
			frameBot.transform.localPosition = new Vector3(frameBot.transform.localPosition.x,-2,frameBot.transform.localPosition.z);
			frameTop.transform.localPosition = new Vector3(frameTop.transform.localPosition.x,4.5f,frameTop.transform.localPosition.z);
		}
		else if (frameBot.transform.localPosition.y > -1)
		{
		Debug.Log ("stop animate1");
			frameBot.transform.localPosition = new Vector3(frameBot.transform.localPosition.x,-1,frameBot.transform.localPosition.z);
			frameTop.transform.localPosition = new Vector3(frameTop.transform.localPosition.x,3.5f,frameTop.transform.localPosition.z);
		}
			
	}
	
	public void SetMove()
	{
		speed = .3f;
		move = true;
		currentTarget = targets[(int)ProfileManager.nextState];
		target = currentTarget;
		direction = Vector3.Normalize(currentTarget-_transform.position);
	}
	
	public void SetZoomIn()
	{
		speed = .1f;
		move = true;
		currentTarget = transform.FindChild("ZoomPosition").position;
		direction = Vector3.Normalize(currentTarget-_transform.position);
		
	}
	
	public void SetZoomOut()
	{
		speed = .1f;
		move = true;
		currentTarget = target;
		direction = Vector3.Normalize(currentTarget-_transform.position);
	}
}
