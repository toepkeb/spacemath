using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	
	public enum Axis
	{
		Forward, Up, Right
	}
	public Axis axis;
	
	public enum Simulation
	{
		Local, World
	}
	public Simulation simulation;
	
	public bool rotate = true;
	public float speed = 1;
	
	Transform _transform;
	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate)
		{
			Rotate();
		}
	}
	
	void Rotate()
	{
		Vector3 _axis = Vector3.zero;
		if (simulation == Simulation.World)
		{
			switch(axis)
			{
			case Axis.Forward:
				_axis = Vector3.forward;
				break;
			case Axis.Right:
				_axis = Vector3.right;
				break;
			case Axis.Up:
				_axis = Vector3.up;
				break;
			}
		}
		else if (simulation == Simulation.Local)
		{
			switch(axis)
			{
			case Axis.Forward:
				_axis = _transform.forward;
				break;
			case Axis.Right:
				_axis = _transform.right;
				break;
			case Axis.Up:
				_axis = _transform.up;
				break;
			}
		}
		
		_transform.RotateAround(_axis,speed);
	}
}
