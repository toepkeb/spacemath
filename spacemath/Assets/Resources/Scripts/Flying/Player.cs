using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	bool foundDestructables;
	GameObject[] destructables;
	GameObject currentTarget;
	GameObject projectileRef;
	float cooldown;
	
	// Use this for initialization
	void Start () {
		projectileRef = (GameObject)Resources.Load ("Prefabs/Projectile");
	}
	
	// Update is called once per frame
	void Update () {
		FindDestructables();
		
		FindClosestTarget();
		
		if (Input.GetMouseButtonDown(0) && cooldown <=0)
		{
			Fire ();
		}
		
		if (cooldown >0)
			cooldown -= Time.deltaTime;
	}
	
	
	void Fire()
	{
		if (currentTarget == null)
			return;
		
		cooldown = .1f;
		
		GameObject newProj = (GameObject)GameObject.Instantiate(projectileRef,transform.position,transform.rotation);
		newProj.GetComponent<Projectile>().SetProjectile(currentTarget.transform);
	}
	
	void FindDestructables()
	{
		if (foundDestructables)
			return;
		
		destructables = GameObject.FindGameObjectsWithTag("Targetable");
		
		if (destructables.Length != 0)
			foundDestructables = true;
	}
	
	void FindClosestTarget()
	{
		GameObject target = null;
		float dist = 5000;
		
		for (int i=0; i < destructables.Length;i++)
		{
			if (destructables[i] == null)
				continue;
			
			float temp = Vector3.SqrMagnitude(destructables[i].transform.position - transform.position);
			
			if (temp < dist)
			{
				target = destructables[i];
				dist = temp;
			}
		}
		
		if (target != currentTarget && target != null)
		{
			target.GetComponent<Target>().ChangeActive(true);
			if (currentTarget != null)
				currentTarget.GetComponent<Target>().ChangeActive(false);
		}
		
		currentTarget = target;
	}
}
