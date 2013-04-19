using UnityEngine;
using System.Collections;

public class SendingCamera : MonoBehaviour {

    public Transform target;
    public float distance;
    public float height;
    public float lerpSpeed;
    public bool follow;
    public bool inGame;

//    StarFoxPlayer playerScript;
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

    public Gyroscope gyro;
    public bool hasGyro;
    Quaternion rotation;
    bool motionControlled;
    Quaternion camRotation;
    public Vector3 mousePos;
    public Vector3 previousMousePos;

	// Use this for initialization
	void Start () {

//        playerScript = target.GetComponent<StarFoxPlayer>();
        xPos = target.position.x;
        yPos = target.position.y;

        hasGyro = SystemInfo.supportsGyroscope;// Input.isGyroAvailable;
        if (hasGyro)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (follow)
        {
            transform.LookAt(target);
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y + height, target.position.z - distance), lerpSpeed);
        }
        if (!follow && inGame)
        {
            if (hasGyro)
            {
                //if (Input.GetMouseButton(0))
                //    gyro.enabled = false;
                //if (Input.touchCount == 0 && !gyro.enabled)
                //    gyro.enabled = true;
            }

            mousePos = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
            if (Input.GetMouseButtonDown(0))
                previousMousePos = mousePos;

            if (hasGyro && gyro.enabled)
            {
                Debug.Log("HIT");
                //ySpeed -= (gyro.rotationRate.y + gyro.rotationRate.z) / 2f;
                //if (Mathf.Abs(gyro.rotationRate.z) > Mathf.Abs(gyro.rotationRate.y))
                    ySpeed -= gyro.rotationRate.z;
//                else if (Mathf.Abs(gyro.rotationRate.y) > Mathf.Abs(gyro.rotationRate.z))
//                    ySpeed -= gyro.rotationRate.y;
                xSpeed -= gyro.rotationRate.x;
            }
            else
            {
                ySpeed -= (previousMousePos.x - mousePos.x) / 20f;
                xSpeed -= (previousMousePos.y - mousePos.y) / 20f;
            }
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
            transform.position = new Vector3(xPos, yPos, Mathf.Lerp(transform.position.z, target.position.z - distance, 0.2f));
            previousMousePos = mousePos;
        }
	}

    void OnGUI()
    {
        //GUI.Label(new Rect(0, 200, 500, 50), gyro.attitude + " GYRO ATT");
        //GUI.Label(new Rect(0, 400, 500, 50), gyro.rotationRate + " GYRO ROT RATE");
    }

    void SetPosition()
    {
        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - distance);
        //GetComponent<Bloom>().enabled = true;
    }
}
