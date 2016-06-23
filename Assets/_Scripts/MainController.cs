using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class MainController : MonoBehaviour {

	public double rate = 0.3;
	public Color color;
	public float size = 1;

	public GameObject player;
	public Transform camera;
	public Transform canvas;
	private double nextAction;

	public GameObject point;
	public GameObject line;
	public GameObject cube;

	private List<GameObject> buffer = new List<GameObject>();

	private Brushes brush;
    private bool isPainting;


    enum Brushes { Sphere, Line, Paint, Neon, Cube };

	// Use this for initialization
	void Start () {
		Debug.Log("Controller starts");
		player.transform.localScale =new Vector3 (size, size, size);
		player.transform.GetComponent<Renderer> ().material.color = color;
        brush = Brushes.Sphere;

	}

    void FixedUpdate()
    {
		if (isPainting && brush == Brushes.Sphere && Time.time > nextAction)
		{
			nextAction = Time.time + rate;
			GameObject clone = Instantiate(point, player.transform.position, player.transform.rotation) as GameObject;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x * size, clone.transform.localScale.y * size, clone.transform.localScale.z * size);
            clone.transform.GetComponent<Renderer>().material.color = color;
            clone.transform.parent = canvas.transform;
        }
		if (isPainting && brush == Brushes.Cube && Time.time > nextAction)
		{
			nextAction = Time.time + rate;
			GameObject clone = Instantiate(cube, player.transform.position, player.transform.rotation) as GameObject;
			clone.transform.localScale = new Vector3(clone.transform.localScale.x * size, clone.transform.localScale.y * size, clone.transform.localScale.z * size);
			clone.transform.GetComponent<Renderer>().material.color = color;
			clone.transform.parent = canvas.transform;
		}
		
		if (isPainting && brush == Brushes.Line && Time.time > nextAction)
		{
			nextAction = Time.time + rate;
			GameObject clone = Instantiate(point, player.transform.position, player.transform.rotation) as GameObject;
			clone.transform.localScale = new Vector3(clone.transform.localScale.x * size, clone.transform.localScale.y * size, clone.transform.localScale.z * size);
			clone.transform.GetComponent<Renderer>().material.color = color;
			clone.transform.parent = canvas.transform;
			buffer.Add(clone);
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.GetKeyDown(KeyCode.P)) {
			//GameObject clone = Instantiate(line, marker.position, marker.rotation) as GameObject;
			//clone.transform.parent = camera.transform;
			isPainting = true;
		}
		if (Input.GetKeyUp(KeyCode.P))
		{
			isPainting = false;
			if (buffer.Count > 0 && brush == Brushes.Line) {
				GameObject newLine = Instantiate(line, player.transform.position, player.transform.rotation) as GameObject;
				LineRenderer lr = newLine.GetComponent<LineRenderer>();
				lr.useWorldSpace = false;
				lr.material = new Material(Shader.Find("Particles/Additive"));
				lr.SetColors(color, color);
				lr.SetWidth(size/2,size/2	);
				lr.SetVertexCount(buffer.Count);
				for (int i = 0; i < buffer.Count ; ++i) {
					lr.SetPosition(i,buffer[i].transform.position);
				}
				newLine.transform.parent = canvas.transform;

				foreach (GameObject obj in buffer) {
					Destroy(obj);
				}
				buffer.Clear();
				
			}
		}
		if (Input.GetKeyDown(KeyCode.B) && Time.time > nextAction)
		{
			nextAction = Time.time + rate;
			brush = (Brushes)(((int)brush + 1) % Enum.GetNames(typeof(Brushes)).Length);
		}
	}
	
	void OnGUI()
	{
		GUILayout.Label("Press B to change Brush");
		GUILayout.Label("Current brush : " + brush);
	}
	
}
