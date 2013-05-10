using UnityEngine;
using System.Collections;

public class TestRaycastAnswerRing : MonoBehaviour {
	
	EquationGenerator eg;
	// Use this for initialization
	void Start () {
		eg = GetComponent<EquationGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
	
//		if (Input.GetMouseButtonDown(0))
//		{
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit[] hits = Physics.RaycastAll(ray);
//			
//			for (int i=0; i < hits.Length;i++)
//			{
//				AnswerRing ring = hits[i].collider.gameObject.GetComponent<AnswerRing>();
//				if (ring == null)
//					continue;
//				
//				eg.CheckEquationAnswer(ring.val);
//				ring.cluster.DestroyCluster(ring.gameObject, ring.index);
//			}
//		}
	}
}
