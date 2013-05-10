using UnityEngine;
using System.Collections;

public class CalculateShipDirection : MonoBehaviour {
	
	Transform _transform;
	
	Vector3 up;
	Vector3 left;
	Vector3 down;
	Vector3 right;
	Vector3 forward1;
	Vector3 forward2;
	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		//Debug.DrawRay(_transform.position,-_transform.up*100,Color.red);
		//Debug.DrawRay(_transform.position,transform.right*100,Color.green);
		CalculateNormals();
		forward1 = Vector3.Cross(up,left);
		forward2 = Vector3.Cross(down, right);
		
		
//		Debug.DrawRay(_transform.position,-_transform.up*100,Color.green);
//		Debug.DrawRay(_transform.position,transform.right*100,Color.red);
//		Debug.DrawRay(_transform.position,left*100,Color.blue);
//		Debug.DrawRay(_transform.position,up*100,Color.black);
		Debug.DrawRay(_transform.position,forward1*100,Color.magenta);
		Debug.DrawRay(_transform.position,Vector3.Cross(down,right)*100,Color.blue);
		
		
//		_transform.forward = Vector3.Lerp (_transform.forward,(forward1+forward2)/2,.03f);
		_transform.forward = Vector3.RotateTowards(_transform.forward,(forward1+forward2)/2,.005f,.005f);
//		_transform.rotation = HelperFunctions.TurnTowardsTarget(_transform,_transform.position+ (forward1+forward2)/2,.01f);
		
		Vector3 dest = _transform.position + _transform.forward*.5f;
		_transform.position = Vector3.Lerp (_transform.position,dest,.2f);
//		_transform.position += Vector3.forward *.05f;
//		if (Input.GetKey(KeyCode.A))
//			_transform.position += -transform.right*1f;
//		if (Input.GetKey(KeyCode.D))
//			_transform.position += transform.right*1f;
//		if (Input.GetKey(KeyCode.W))
//			_transform.position += transform.up*1f;
//		if (Input.GetKey(KeyCode.S))
//			_transform.position += -transform.up*1f;
	}
	
	void CalculateNormals()
	{
		Ray ray = new Ray(_transform.position,_transform.up+_transform.forward*.5f);
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray,100);
		for (int i=0; i < hits.Length;i++)
		{
			if (hits[i].collider.tag == "Tube")
			{
				up = hits[i].normal;
				//Debug.DrawRay(hits[i].point,hits[i].normal*30,Color.white);
			}
		}
		
		ray = new Ray(_transform.position,-transform.up+_transform.forward*.5f);
		hits = Physics.RaycastAll(ray);	
		for (int i=0; i < hits.Length;i++)
		{
			if (hits[i].collider.tag == "Tube")
			{
				down = hits[i].normal;
				//Debug.DrawRay(hits[i].point,hits[i].normal*30,Color.white);
			}
		}
		
		
		ray = new Ray(_transform.position,transform.right+_transform.forward*.5f);
		hits = Physics.RaycastAll(ray);	
		for (int i=0; i < hits.Length;i++)
		{
			if (hits[i].collider.tag == "Tube")
			{
				right = hits[i].normal;
				//Debug.DrawRay(hits[i].point,hits[i].normal*30,Color.white);
			}
		}
		
		ray = new Ray(_transform.position,-transform.right+_transform.forward*.5f);
		hits = Physics.RaycastAll(ray);
		for (int i=0; i < hits.Length;i++)
		{
			if (hits[i].collider.tag == "Tube")
			{
				left = hits[i].normal;
				//Debug.DrawRay(hits[i].point,hits[i].normal*30,Color.white);
			}
		}
		
	}

}
