using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	
	public int health;
	// Use this for initialization
	void Start () {
		ChangeActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.localScale.x > .5f)
			transform.localScale -= Vector3.one *.1f;
	}
	
	public void HitTarget(int dmg)
	{
		health -= dmg;
		
		if (health <=0)
			DestroyTarget();
	}
	
	public void ChangeActive(bool on)
	{
		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
			mr.enabled = on;
		
		transform.localScale = new Vector3(2,2,2);
	}
	
	public void DestroyTarget()
	{
		foreach (OneShotParticleSystem ps in GetComponentsInChildren<OneShotParticleSystem>())
			ps.PlayOneShot();
		
		//cluster.hasShield = false;
		
		Destroy(transform.parent.gameObject);
	}
}
