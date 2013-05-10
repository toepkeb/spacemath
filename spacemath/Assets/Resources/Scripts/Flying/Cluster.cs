using UnityEngine;
using System.Collections;

public class Cluster : MonoBehaviour {
	
	GameObject[] answerRings;
	public int nodes;
	GameObject shield;
	public bool hasShield;
	int rotDirection;
	bool hit;
	bool lookAtPlayer;
	Transform player;
	
	enum Behavior
	{
		None, UpAndDown, SideToSide, Rotate, UpAndDownRotate, SideToSideRotate
	}
	Behavior behavior;
	bool move;
	
	Transform _transform;
	// Use this for initialization
	void Start () {
		_transform = transform;
		move = true;
//		behavior = Behavior.UpAndDownRotate;
//		CreateAnswerRings();
//		AddLinks();
		player = GameObject.Find("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		HandleBehavior();
//		for (int i=0; i < answerRings.Length;i++)
//		{
//			if (answerRings[i] != null)
//				answerRings[i].transform.LookAt(answerRings[i].transform.position-player.position);
//		}
	}
	
	void HandleBehavior()
	{
		if (!move) 
			return;

		switch(behavior)
		{
		case Behavior.None:
			break;
		case Behavior.UpAndDown:
			_transform.position += new Vector3(0,Mathf.Cos(Time.time)/(Mathf.PI*10),0);
			break;
		case Behavior.SideToSide:
		{
			_transform.position += new Vector3(Mathf.Cos(Time.time)/(Mathf.PI*10),0,0);
		}
			break;
		case Behavior.Rotate:
		{
			_transform.RotateAroundLocal(_transform.forward,Time.deltaTime*rotDirection);
			if (answerRings != null)
			{
				for (int i=0; i < answerRings.Length;i++)
				{
					if (answerRings[i] == null)
						continue;
					Vector3 angle = answerRings[i].transform.eulerAngles;
					answerRings[i].transform.eulerAngles = new Vector3(angle.x,angle.y,0);
				}
			}
		}
			break;
		case Behavior.UpAndDownRotate:
		{
			_transform.RotateAroundLocal(_transform.forward,Time.deltaTime*rotDirection);
			if (answerRings != null)
			{
				for (int i=0; i < answerRings.Length;i++)
				{
					if (answerRings[i] == null)
						continue;
					Vector3 angle = answerRings[i].transform.eulerAngles;
					answerRings[i].transform.eulerAngles = new Vector3(angle.x,angle.y,0);
				}
			}
			_transform.position += new Vector3(0,Mathf.Cos(Time.time)/(Mathf.PI*10),0);
		}
			break;
		case Behavior.SideToSideRotate:
		{
			_transform.RotateAroundLocal(_transform.forward,Time.deltaTime*rotDirection);
			if (answerRings != null)
			{
				for (int i=0; i < answerRings.Length;i++)
				{
					if (answerRings[i] == null)
						continue;
					Vector3 angle = answerRings[i].transform.eulerAngles;
					answerRings[i].transform.eulerAngles = new Vector3(angle.x,angle.y,0);
				}
			}
			_transform.position += new Vector3(Mathf.Cos(Time.time)/(Mathf.PI*10),0,0);
		}
			break;
		
		}
	}
	
	void CreateAnswerRings(int[] val, int answer)
	{
		GameObject ar = (GameObject)Resources.Load ("Prefabs/AnswerRing");
		answerRings = new GameObject[nodes];
		
		float angle = 2*Mathf.PI/nodes;
		float radius = 2;
		for (int i=0; i < nodes;i++)
		{
			float currentAngle = i*angle;
			float x = Mathf.Cos(currentAngle);
			float y = Mathf.Sin (currentAngle);
			
			answerRings[i] = (GameObject)GameObject.Instantiate(ar,new Vector3(x,y,0)*radius + _transform.position,Quaternion.identity);
			answerRings[i].transform.parent = _transform;
			answerRings[i].GetComponentInChildren<TextMesh>().text = val[i].ToString();
			answerRings[i].transform.localRotation = Quaternion.identity;
			AnswerRing ring = answerRings[i].GetComponent<AnswerRing>();
			ring.val= val[i];
			ring.cluster = this;
			ring.index = i;
		}
	}
	
	void AddLinks()
	{
		GameObject link = (GameObject)Resources.Load ("Prefabs/Link");
		for (int i=0; i < answerRings.Length;i++)
		{
			GameObject l1 = (GameObject)GameObject.Instantiate(link);
			l1.transform.parent = answerRings[i].transform;
			l1.transform.localPosition = Vector3.zero;
//			if (i == answerRings.Length-1)
//				l1.GetComponent<MyLaser>().targetPos = answerRings[0].transform.position;
//			else
//				l1.GetComponent<MyLaser>().targetPos = answerRings[i+1].transform.position;
			if (i == answerRings.Length-1)
				l1.GetComponent<MyLaser>().target = answerRings[0].transform;
			else
				l1.GetComponent<MyLaser>().target = answerRings[i+1].transform;
			
			
			if (nodes <3)
				continue;
			GameObject l2 = (GameObject)GameObject.Instantiate(link);
			l2.transform.parent = answerRings[i].transform;
			l2.transform.localPosition = Vector3.zero;
//			if (i == 0)
//				l2.GetComponent<MyLaser>().targetPos = answerRings[answerRings.Length-1].transform.position;
//			else
//				l2.GetComponent<MyLaser>().targetPos = answerRings[i-1].transform.position;
			if (i == 0)
				l2.GetComponent<MyLaser>().target = answerRings[answerRings.Length-1].transform;
			else
				l2.GetComponent<MyLaser>().target = answerRings[i-1].transform;
		}
	}
	
	public void SetValues(int answer, int[] incorrect)
	{
		int[] values = new int[incorrect.Length+1];
		bool setAnswer = false;
		
		for ( int i=0; i < values.Length;i++)
		{
			int rnd =-1;
			if (setAnswer)
				rnd = Random.Range(1,values.Length-i);
			else
				rnd = Random.Range(0,values.Length-i);
			
			if (rnd ==0)
			{
				rnd = answer;
				setAnswer = true;
			}
			else
			{
				if (setAnswer)
					rnd = incorrect[i-1];
				else
					rnd = incorrect[i];
			}
			
			values[i] = rnd;
		}
		
		nodes = values.Length;
		_transform = transform;
		CreateAnswerRings(values, answer);
		AddLinks();
		behavior = (Behavior)Random.Range(0,7);
		if (Random.Range(0,2) ==0)
			rotDirection = 1;
		else
			rotDirection = -1;
		
		if (Random.Range(0,2) == 1)
			CreateShield();
	}
	
	void CreateShield()
	{
		hasShield = true;
		shield = (GameObject)Resources.Load("Prefabs/Shield");
		shield = (GameObject)GameObject.Instantiate(shield,_transform.position,_transform.rotation);
		shield.transform.localScale = new Vector3(7,7,7);
		shield.transform.parent = _transform;
		shield.GetComponent<Shield>().SetShield(this,Random.Range(1,4));
		//GameObject targeter = (GameObject)GameObject.Instantiate(Resources.Load ("Prefabs/Target"),shield.transform.position,shield.transform.rotation);
		//targeter.transform.parent = shield.transform;
		//targeter.transform.localScale += Vector3.one*.5f;
		//targeter.transform.position += targeter.transform.forward *-2;
	}
	
	public void DestroyCluster(GameObject hit, int index)
	{
		if (this.hit)
			return;
		
		ParticleSystem ps = transform.FindChild("Particle System").GetComponent<ParticleSystem>();
		ps.transform.position = hit.transform.position;
		ps.Play ();
		move = false;
		
		foreach (LineRenderer lr in hit.GetComponentsInChildren<LineRenderer>())
		{
			Destroy(lr.GetComponent<MyLaser>());
			Destroy(lr);
			
		}
		
		int other = (index+1)%answerRings.Length;
		
		foreach (LineRenderer lr in answerRings[other].GetComponentsInChildren<LineRenderer>())
		{
			if (lr.GetComponent<MyLaser>().target == hit.transform)
			{
				Destroy(lr.GetComponent<MyLaser>());
				Destroy(lr);
			}
		}
		
		if (nodes >2)
		{
			other = (index-1)%answerRings.Length;
			if (other < 0)
				other+=answerRings.Length;
			foreach (LineRenderer lr in answerRings[other].GetComponentsInChildren<LineRenderer>())
			{
				if (lr.GetComponent<MyLaser>().target == hit.transform)
				{
					Destroy(lr.GetComponent<MyLaser>());
					Destroy(lr);
				}
			}
		}
		
		this.hit = true;
		Destroy(hit);
	}
}
