using UnityEngine;
using System.Collections;

public class StarFoxPlayer : MonoBehaviour {

    public CharacterController controller;
    public float xSpeedCap = 2.5f;
    public float ySpeedCap = 2.5f;
    public float zSpeedCap = 2.5f;
    public float xVelocity = 0.5f;
    public float yVelocity = 0.5f;
    public float zVelocity = 0.5f;
    public float yRotCap;
    public float xRotCap;
    public float zRotCap;
    public float xRotSpeed;
    public float yRotSpeed;
    public float zRotSpeed;
    float xSpeed = 0;
    float ySpeed = 0;
    float zSpeed = 0;
    float xRot = 0f;
    float yRot = 180f;
    float zRot = 0f;
    public SendingCamera camScript;
    Vector3 mousePos;
    Vector3 previousMousePos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (camScript.inGame)
        {
            mousePos = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
            if (Input.GetMouseButtonDown(0))
                previousMousePos = mousePos;
            if (camScript.hasGyro && camScript.gyro.enabled)
            {
                //zRot = (camScript.gyro.rotationRate.z + camScript.gyro.rotationRate.y) / 2f;
//                if (Mathf.Abs(camScript.gyro.rotationRate.z) > Mathf.Abs(camScript.gyro.rotationRate.y))
//                {
                    zRot -= camScript.gyro.rotationRate.z;
                    xSpeed -= camScript.gyro.rotationRate.z;
//                }
//                else if (Mathf.Abs(camScript.gyro.rotationRate.y) > Mathf.Abs(camScript.gyro.rotationRate.z))
//                {
//                    zRot -= camScript.gyro.rotationRate.y;
//                    xSpeed -= camScript.gyro.rotationRate.y;
				
//                }
                xRot += camScript.gyro.rotationRate.x * 2f;
                ySpeed += camScript.gyro.rotationRate.x * 2f;
            }
            else
            {
                zRot -= (previousMousePos.x - mousePos.x) / 2f;
                xRot += (previousMousePos.y - mousePos.y) / 2f;
                ySpeed += (previousMousePos.y - mousePos.y) / 20f;
                xSpeed -= (previousMousePos.x - mousePos.x) / 20f;
            }

            if (Input.GetKey(KeyCode.D))
                zRot += zRotSpeed;
            if (Input.GetKey(KeyCode.A))
                zRot -= zRotSpeed;
            if (Input.GetKey(KeyCode.W))
                xRot += xRotSpeed;
            if (Input.GetKey(KeyCode.S))
                xRot -= xRotSpeed;
            if (Mathf.Abs(yRot) > yRotCap + 180)
            {
                if (yRot < 0)
                    yRot = -yRotCap + 180;
                else
                    yRot = yRotCap + 180;
            }
            if (Mathf.Abs(xRot) > xRotCap)
            {
                if (xRot < 0)
                    xRot = -xRotCap;
                else
                    xRot = xRotCap;
            }
            if (Mathf.Abs(zRot) > zRotCap)
            {
                if (zRot < 0)
                    zRot = -zRotCap;
                else
                    zRot = zRotCap;
            }
            yRot = Mathf.Lerp(yRot, 180, 0.05f);
            zRot = Mathf.Lerp(zRot, 0, 0.05f);
            xRot = Mathf.Lerp(xRot, 0, 0.05f);
            //if (Mathf.Abs(xRot) <= 0.01f)
            //    zRot = 0;
            if (Mathf.Abs(yRot) <= 0.01f)
                yRot = 0;
            //if (Mathf.Abs(xRot) <= 0.01f)
            //    xRot = 0;
            transform.rotation = Quaternion.Euler(new Vector3(xRot, yRot, zRot));

            if (Input.GetKey(KeyCode.W))
            {
                ySpeed += yVelocity;
            }
            if (Input.GetKey(KeyCode.S))
            {
                ySpeed -= yVelocity;
            }
            if (Input.GetKey(KeyCode.A))
            {
                xSpeed -= xVelocity;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xSpeed += xVelocity;
            }
            if (Mathf.Abs(ySpeed) > ySpeedCap)
            {
                if (ySpeed < 0)
                    ySpeed = -ySpeedCap;
                else
                    ySpeed = ySpeedCap;
            }
            if (Mathf.Abs(xSpeed) > xSpeedCap)
            {
                if (xSpeed < 0)
                    xSpeed = -xSpeedCap;
                else
                    xSpeed = xSpeedCap;
            }
            xSpeed = Mathf.Lerp(xSpeed, 0, 0.1f);
            ySpeed = Mathf.Lerp(ySpeed, 0, 0.1f);
            controller.Move(new Vector3(xSpeed, ySpeed, zSpeed));
            previousMousePos = mousePos;
        }
        else
        {
            xSpeed = 0;
            ySpeed = 0;
            xRot = Mathf.Lerp(xRot, 0, 0.1f);
            yRot = Mathf.Lerp(yRot, 180, 0.1f);
            zRot = Mathf.Lerp(zRot, 0, 0.1f);
            transform.rotation = Quaternion.Euler(new Vector3(xRot, yRot, zRot));
        }
	}
}
