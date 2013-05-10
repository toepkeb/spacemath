using UnityEngine;
using System.Collections;

public class RollInPipe : MonoBehaviour {
	
	Transform _transform;
	Vector3 center;
	Vector3 targetForward;
	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		center = GetCenter();
		
		
		//_transform.forward = Vector3.Lerp (_transform.forward,targetForward,.1f);
		_transform.position += _transform.forward *.1f;
		
		if (Input.GetKey(KeyCode.A))
		{
			_transform.RotateAround(center,_transform.forward, -1f);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			_transform.RotateAround(center,_transform.forward, 1f);
		}
	}
	
	Vector3 GetCenter()
	{
		RaycastHit[] hits;
		Ray ray;
		Vector3 center = Vector3.zero;
		
		Vector3 p2 = Vector3.zero;
		ray = new Ray(_transform.position,_transform.up);
		Debug.DrawRay(_transform.position,ray.direction*100,Color.blue);
		hits = Physics.RaycastAll(ray,100);
		for (int i=0; i < hits.Length;i++)
		{
			if (hits[i].collider.tag == "Tube")
			{
				p2 = hits[i].point;
			}
		}
		Vector3 p1 = Vector3.zero;
		ray = new Ray(_transform.position,-_transform.up);
		hits = Physics.RaycastAll(ray,100);
		Debug.DrawRay(_transform.position,ray.direction*100,Color.white);
		for (int i=0; i < hits.Length;i++)
		{
			if (hits[i].collider.tag == "Tube")
			{
				p1 = hits[i].point;
				_transform.position = p1+_transform.up;
				targetForward = Vector3.Cross(_transform.right, hits[i].normal);
				//_transform.up = Vector3.Lerp (_transform.up,hits[i].normal,.1f);
				center = hits[i].point + hits[i].normal*Vector3.Magnitude(p1=p2);
			}
		}
		
		
		return center;
	}
}
