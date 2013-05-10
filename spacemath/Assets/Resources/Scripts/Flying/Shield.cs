using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	public int health;
	Cluster cluster;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetShield(Cluster parent, int health)
	{
		this.health = health;
		this.cluster = parent;
	}
	
	public void HitShield(int dmg)
	{
		health -= dmg;
		
		if (health <=0)
			DestroyShield();
	}
	
	void DestroyShield()
	{
		transform.FindChild("Target").GetComponent<Target>().ChangeActive(false);
		
		foreach (OneShotParticleSystem ps in GetComponentsInChildren<OneShotParticleSystem>())
			ps.PlayOneShot();
		
		cluster.hasShield = false;
		
		Destroy(gameObject);
	}
}
