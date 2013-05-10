using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[ExecuteInEditMode]
public class Lightning : MonoBehaviour {

    public GameObject target;
    public LineRenderer LR;
    public float arcLength = 0.1f;
    public float arcVariation = 5f;
    public float inaccuracy = 1f;
    int test = 0;
    public float randomMax;
    public float randomMin;
    public float speed;
    public List<Vector3> verts;
    public int amount;

	// Use this for initialization
	void Start () {

        LR.SetPosition(0, transform.position);
    }
	
	// Update is called once per frame
	void Update () {

//        float y = transform.position.y;
        Vector3 lastPoint = transform.position;
        int i = 0;
        int vertCount = 0;
        int totalVerts = 0;
        test++;
        
        if (test % speed == 0)
        {
            for (int z = 0; z < amount; ++z)
            {
                Vector3 targetPos = transform.position + new Vector3(Random.Range(randomMin, randomMax), Random.Range(randomMin, randomMax), Random.Range(randomMin, randomMax));
                if (target != null)
                    targetPos = target.transform.position;
                while (Vector3.Distance(lastPoint, targetPos) > 1f)
                {
                    LR.SetVertexCount(i + 1);
                    Vector3 fwd = targetPos - lastPoint;
                    fwd.Normalize();
                    fwd = Randomize(fwd, inaccuracy);
                    fwd *= Random.Range(arcLength * arcVariation, arcLength);
                    fwd += lastPoint;
                    LR.SetPosition(i, fwd);
                    verts.Add(fwd);
                    ++i;
                    lastPoint = fwd;
                    vertCount++;
                }
				totalVerts += vertCount;
                LR.SetVertexCount(totalVerts);
//                if (verts.Count < 1)
//                    return;
//                if (z == 0)
//                    totalVerts += i + vertCount;
//                else
//                    totalVerts += vertCount * 2;
//                i = totalVerts;
//                LR.SetVertexCount(totalVerts);
//                lastPoint = verts[0];
//                int offset = 0;
//                for (int v = totalVerts - vertCount; v < totalVerts; ++v)
//                {
//                    LR.SetPosition(v, verts[verts.Count - 1 - offset]);
//                    offset++;
//                }
                verts.Clear();
                vertCount = 0;
            }
        }
	}

    Vector3 Randomize (Vector3 v3, float inaccuracy2)
    {
       v3 += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * inaccuracy2;
       v3.Normalize();
       return v3;
    }
}
