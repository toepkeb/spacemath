using UnityEngine;
using System.Collections;

public class AddDustTrail : MonoBehaviour {
	
	ParticleSystem ps;
	ParticleSystem.Particle[] particles;
	public ParticleSystem dust;
	ParticleSystem[] dps;
	int count;
	int length;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		length = ps.particleCount;
		
		particles = new ParticleSystem.Particle[length];
		dps = new ParticleSystem[length];
		for (int i=0; i < length;i++)
		{
			dps[i] = (ParticleSystem)GameObject.Instantiate(dust);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		count = ps.GetParticles(particles);
		
		for (int i=0; i < count;i++)
		{
			Debug.Log (dps[i]);
			dps[i].transform.position = particles[i].position + ps.transform.position;
		}
	}
}
