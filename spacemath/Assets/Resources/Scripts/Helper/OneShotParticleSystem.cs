using UnityEngine;
using System.Collections;

public class OneShotParticleSystem : MonoBehaviour {
	
	bool played;
	ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (played && !ps.isPlaying)
		{
			Destroy(gameObject);
		}
	}
	
	public void PlayOneShot()
	{
		transform.parent = null;
		ps.Play();
		played = true;
	}
}
