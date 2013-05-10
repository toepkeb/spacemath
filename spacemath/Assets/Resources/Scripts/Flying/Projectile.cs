using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	float ttl;
	Vector3 target;
	Vector3 direction;
	bool passed;
	
	Transform _transform;
	// Use this for initialization
	void Start () {
		ttl = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Vector3.SqrMagnitude(target-transform.position) >=100 && !passed)
			direction = Vector3.Lerp (direction, Vector3.Normalize(target - _transform.position),.1f);
		else
			passed = true;
		
		_transform.position += direction;
		_transform.LookAt(_transform.position+direction);
		
		ttl -= Time.deltaTime;
		if (ttl <=0)
			DestroyProjectile(false);
	}
	
	public void SetProjectile(Transform tar)
	{
		_transform = gameObject.transform;
		direction = _transform.forward;
		target = tar.position;
	}
	
	void DestroyProjectile(bool hit)
	{
		if (hit)
			foreach (OneShotParticleSystem ps in GetComponentsInChildren<OneShotParticleSystem>())
				ps.PlayOneShot();
		
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Targetable")
		{
			DestroyProjectile(true);
			col.GetComponent<Target>().HitTarget(1);
		}
		if (col.gameObject.tag == "Tube")
		{
			DestroyProjectile(true);
		}
	}
}
