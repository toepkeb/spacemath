using UnityEngine;
using System.Collections;

public class ButtonDetection : MonoBehaviour {
	
	ButtonAnchor lastButton;
	
	bool canClick = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
		{
			
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray);
			
			bool touchingButton = false;
			for (int i=0; i < hits.Length;i++)
			{
				ButtonAnchor button = hits[i].transform.gameObject.GetComponent<ButtonAnchor>();
				
				if ((lastButton != null && lastButton != button) || !canClick)
				{
					continue;
				}
				if (button != null && button.isActive)
				{
					touchingButton = true;
					if (Input.GetMouseButtonDown(0))
					{
						button.TouchDown();
						lastButton = button;
					}
					else if (Input.GetMouseButtonUp(0))
					{
						button.Click();
						canClick = true;
						lastButton = null;
					}
					else
					{
						button.ButtonHeld();
					}
				}
			}
			if (!touchingButton)
			{
				canClick = false;
				CancelButton();
			}
		
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			CancelButton();
			canClick = true;
		}
		
	}
	
	void CancelButton()
	{
		if (lastButton != null)
		{
			lastButton.Canceled();
			lastButton = null;
		}
	}
}
