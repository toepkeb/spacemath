using UnityEngine;
using System.Collections;

public class MyLaser : MonoBehaviour {

    public enum LaserType
    {
        Wave, Burst
    }
    public LaserType myType = LaserType.Wave;

    public float width;
    public Color color;
    public LineRenderer lineRen;
    public float noise = 1.0f;
    public Transform target;
    public Vector3 targetPos;
    public int length;
    public float height = 1;
    float randomOffset;
    public int frames;
    Vector3 lastPoint;
    public float burstRandomness;
    public Transform particle;
    int frameCounter;
    float randomY;
    float randomZ;
    public Color colStart;
    public Color colEnd;
    public Texture2D flashTex;

	// Use this for initialization
	void Start () {

        lineRen = this.GetComponent<LineRenderer>();
        if (myType == LaserType.Wave)
        {
            lineRen.SetVertexCount(length + 1);
            //lineRen.SetWidth(width, width);
            lastPoint = transform.position;
        }
        else if (myType == LaserType.Burst)
        {
            lineRen.SetVertexCount(2);
            lineRen.SetPosition(0, this.transform.position);
            randomY = Random.Range(-burstRandomness, burstRandomness);
            randomZ = Random.Range(-burstRandomness, burstRandomness);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
            targetPos = target.position;
        Vector3 direction = targetPos - this.transform.position;
        direction.Normalize();
        float distance = Vector3.Distance(transform.position, targetPos);

        if (myType == LaserType.Wave)
        {
            lineRen.SetPosition(0, this.transform.position);
            for (int i = 1; i < length; ++i)
            {
                // use to make it circular    Mathf.Cos((10 * ((Time.time * frames) + i * 10)) / height)
                lineRen.SetPosition(i, (transform.position + direction * ((distance / length) * i) + new Vector3(0, Mathf.Sin(10 * ((Time.time * frames) + i * 10)) / height, 0)));
            }
            lineRen.SetPosition(length, targetPos + new Vector3(0, Mathf.Sin(10 * ((Time.time * frames) + length * 10)) / height, 0));
        }
        else if (myType == LaserType.Burst)
        {
            bool emit = false;
            frameCounter++;
            if (frameCounter % frames == 0)
            {
                colStart = new Color(colStart.r, colStart.g, colStart.b, 1);
                colEnd = new Color(colEnd.r, colEnd.g, colEnd.b, 1);
                randomY = Random.Range(-burstRandomness, burstRandomness);
                randomZ = Random.Range(-burstRandomness, burstRandomness);
                emit = true;
                //particle.particleSystem.Play();
            }
            lineRen.SetPosition(1, targetPos + new Vector3(0, randomY, randomZ));
            //particle.position = targetPos + new Vector3(0, randomY, randomZ);
            lineRen.SetColors(colStart, colEnd);
            //particle.particleSystem.enableEmission = emit;
            colStart = new Color(colStart.r, colStart.g, colStart.b, 0);
            colEnd = new Color(colEnd.r, colEnd.g, colEnd.b, 0);
            emit = false;
            //particle.particleSystem.Stop();
        }
    }

    void OnGUI()
    {
        //if (myType == LaserType.Burst)
        //{
        //    GUI.depth = -1000;
        //    if (frameCounter % frames == 0)
        //    {
        //        GUI.color = new Color(1, 0, 0, 0.1f);
        //    }
        //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), flashTex);
        //    GUI.color = new Color(1, 0, 0, 0);
        //}
    }
}
