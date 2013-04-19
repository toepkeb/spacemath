using UnityEngine;
using System.Collections;

public class StarFoxCamera : MonoBehaviour {

    public Transform target;
    //StarFoxPlayer playerScript;
    public float xCap;
    public float yCap;
    float xSpeed;
    float ySpeed;
    public float xDist;
    public float yDist;
    public float yTurnSpeed;
    public float xTurnSpeed;
    float xPos = 0;
    float yPos = 0;

	// Use this for initialization
	void Start () {

        //playerScript = target.GetComponent<StarFoxPlayer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.D))
            ySpeed += xTurnSpeed;
        if (Input.GetKey(KeyCode.A))
            ySpeed -= xTurnSpeed;
        if (Input.GetKey(KeyCode.W))
            xSpeed -= yTurnSpeed;
        if (Input.GetKey(KeyCode.S))
            xSpeed += yTurnSpeed;
        if (Mathf.Abs(ySpeed) > yCap)
        {
            if (ySpeed < 0)
                ySpeed = -yCap;
            else
                ySpeed = yCap;
        }
        if (Mathf.Abs(xSpeed) > xCap)
        {
            if (xSpeed < 0)
                xSpeed = -xCap;
            else
                xSpeed = xCap;
        }
        ySpeed = Mathf.Lerp(ySpeed, 0, 0.05f);
        xSpeed = Mathf.Lerp(xSpeed, 0, 0.05f);
        if (Mathf.Abs(xSpeed) <= 0.01f)
            xSpeed = 0;
        if (Mathf.Abs(ySpeed) <= 0.01f)
            ySpeed = 0;
        transform.rotation = Quaternion.Euler(new Vector3(xSpeed, ySpeed, 0));

        Vector2 camPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(target.position.x, target.position.y);
        Vector2 direction = playerPos - camPos;
        direction.Normalize();

        if (Mathf.Abs(target.position.x - transform.position.x) > xDist)
        {
            if (target.position.x > transform.position.x)
                xPos = Mathf.Lerp(xPos, target.position.x - xDist, Mathf.SmoothStep(0.0f, 1.0f, 0.2f));
            if (target.position.x < transform.position.x)
                xPos = Mathf.Lerp(xPos, target.position.x + xDist, Mathf.SmoothStep(0.0f, 1.0f, 0.2f));
        }
        if (Mathf.Abs(target.position.y - transform.position.y) > yDist)
        {
            if (target.position.y > transform.position.y)
                yPos = Mathf.Lerp(yPos, target.position.y - yDist, Mathf.SmoothStep(0.0f, 1.0f, 0.2f));
            if (target.position.y < transform.position.y)
                yPos = Mathf.Lerp(yPos, target.position.y + yDist, Mathf.SmoothStep(0.0f, 1.0f, 0.2f));
        }
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
