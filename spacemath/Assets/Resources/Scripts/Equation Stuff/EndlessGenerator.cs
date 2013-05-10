using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessGenerator : MonoBehaviour {
	
	int currentEquationIndex;
	int spawnPointIndex;
	int maxSpawnPoints;
	Equation currentEquation;
	List<Equation> equationsAnswered;
	
	
	//Test Textures
	public Texture2D star;
	public Texture2D array;
	public Texture2D starSquare;
	public Texture2D[] stars;
	// Use this for initialization
	void Start () {
		
		maxSpawnPoints = GameObject.Find ("SpawnPoints").transform.childCount;
		
		Reset ();
	}
	
	void Reset()
	{
		currentEquationIndex = 0;
		spawnPointIndex = 0;
		
		EquationCache.SharedCache.equationCount = 3;
		EquationCache.SharedCache.SetEquations();
		currentEquation = EquationCache.SharedCache.GetEquation(currentEquationIndex);
		for ( int i=0; i < EquationCache.SharedCache.equationCache.Length;i++)
		{
			CreateCluster(spawnPointIndex, EquationCache.SharedCache.GetEquation(i));
			
			NextSpawnPoint();
		}
		
		equationsAnswered = new List<Equation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.N))
			NextEquation();
	}
	
	GameObject CreateCluster(int index, Equation eq)
	{
		Transform spawn = GameObject.Find ("SpawnPoints").transform.FindChild(index.ToString());
		
		GameObject currentCluster = (GameObject)GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/Cluster"));
		currentCluster.GetComponent<Cluster>().SetValues(eq.answer,eq.incorrectAnswers);
		currentCluster.transform.position = spawn.position;
		currentCluster.transform.rotation = spawn.rotation;
		
		return currentCluster;
	}
	
	public void NextEquation()
	{
		if (currentEquation.status == Equation.Status.Null)
			currentEquation.status = Equation.Status.Skipped;
		
		//Add current euqaiton to those answered
		equationsAnswered.Add (currentEquation);
			
		//Change the equation that was just answered o a new equation
		Equation newEq = EquationCache.SharedCache.ChangeSingleEquation(currentEquationIndex);
		
		//Create cluster from new equation
		CreateCluster(spawnPointIndex,newEq);
		//Advance spawnPoint
		NextSpawnPoint();
		
		//Advance the currentindexcurrentEquationIndex
		currentEquationIndex++;
		if (currentEquationIndex >= EquationCache.SharedCache.equationCache.Length)
			currentEquationIndex = 0;
		Debug.Log (currentEquationIndex);
		
		//Get the next equation
		currentEquation = EquationCache.SharedCache.GetEquation(currentEquationIndex);
		currentEquation.DebugValues();
		//Create cluster from new equation
		
	}
	
	void NextSpawnPoint()
	{
		spawnPointIndex ++;
		if (spawnPointIndex >= maxSpawnPoints)
			spawnPointIndex = 0;
	}
	
	
	void OnGUI()
	{
		DisplayEquation();
	}
	void DisplayEquation()
	{
		Rect equationRect = new Rect(Screen.width*.5f - 150, Screen.height*.75f,300,50);
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		
		string eq = "";
		
		if (currentEquation.type == 0)
		{
			GUI.Box(equationRect,"                         = ?");
			Rect arrayRect = new Rect(equationRect.x+30,equationRect.y+5,100,40);
			GUI.DrawTexture(arrayRect,array);
			for (int i=0; i < currentEquation.answer;i++)
			{
				float x = 30 + equationRect.x + 20*(i%5);
				float y = equationRect.y+5;
				if (i >=5)
					y += 20;
				GUI.DrawTexture(new Rect(x,y,20,20),star);
			}
		}
		else if (currentEquation.type == 1 && currentEquation.shapes)
		{
			int width = 30*currentEquation.values.Length;
			Rect arrayRect = new Rect(Screen.width*.45f-width*.5f,Screen.height*.5f,width,50);
			equationRect = new Rect(Screen.width *.5f-200,Screen.height*.5f,400,50);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(equationRect,"                         = ?");
			
			GUI.BeginGroup(arrayRect);
			
			for (int i=0; i < currentEquation.values.Length;i++)
			{
				GUI.DrawTexture(new Rect(i*30,10,30,30),stars[currentEquation.values[i]]);
			}
			
			GUI.EndGroup();
		}
		else if (currentEquation.type ==2)
		{
			int width = 20*currentEquation.values[0];
			int height = 20*currentEquation.values[1];
			Rect arrayRect = new Rect(Screen.width*.45f-width*.5f,Screen.height*.5f-height*.5f,width,height);
			equationRect = new Rect(Screen.width *.5f-200,Screen.height*.5f-height*.5f,400,height);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(equationRect,"                  = ?");
			
			GUI.BeginGroup(arrayRect);
			
			for (int x=0; x< currentEquation.values[0];x++)
			{
				for (int y = 0; y < currentEquation.values[1];y++)
				{
					Rect tempRect = new Rect(x*20,y*20,20,20);
					GUI.DrawTexture(tempRect,starSquare);
				}
			}
			GUI.EndGroup();
			
			GUI.Label (new Rect(arrayRect.x + arrayRect.width *.5f-15,arrayRect.y - 30, 30,30),currentEquation.values[0].ToString());
			GUI.Label (new Rect(arrayRect.x - 30,arrayRect.y + arrayRect.height *.5f-15, 30,30),currentEquation.values[1].ToString());
			
			
		}
		else if (currentEquation.type >=3 || (currentEquation.type ==1 && !currentEquation.shapes))
		{
			
			for (int i=0; i < currentEquation.values.Length;i++)
			{
				eq += currentEquation.values[i].ToString();
				
				if (i < currentEquation.values.Length-1)
					eq += currentEquation.GetOperation();
				else
					eq += " = " + currentEquation.answer;
			}
			
			GUI.Box (equationRect,eq);
		}
	}
	
}
