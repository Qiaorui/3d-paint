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
	public GameObject menu;
	public Transform camera;
	public Transform canvas;
	private double nextAction;

	public GameObject point;
	public GameObject line;
	public GameObject cube;
	public GameObject gap;





	private List<GameObject> buffer = new List<GameObject>();
	private LineRenderer lineRenderer;
	private int index;



	private Brushes brush;
    private bool isPainting;
	private bool menuOpen;




    enum Brushes { Sphere, Line, Paint, Neon, Cube };

	// Use this for initialization
	void Start () {
		Debug.Log("Controller starts");
		player.transform.localScale =new Vector3 (size, size, size);
		player.transform.GetComponent<Renderer> ().material.color = color;
        brush = Brushes.Sphere;
		isPainting = false;
		menuOpen = false;


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
		
		if (isPainting && brush == Brushes.Line)
		{

			if (Time.time > nextAction) 
			{
				nextAction = Time.time + rate;
				GameObject clone = Instantiate(gap, player.transform.position, player.transform.rotation) as GameObject;
				//clone.transform.localScale = new Vector3(clone.transform.localScale.x * size, clone.transform.localScale.y * size, clone.transform.localScale.z * size);
				//clone.transform.GetComponent<Renderer>().material.color = color;
				clone.transform.parent = canvas.transform;
				buffer.Add(clone);

				lineRenderer.SetVertexCount(index+1);
				lineRenderer.SetPosition(index, buffer[index].transform.position);
				++index;
			}
			for (int i = 0; i < index ; ++i) 
			{
				lineRenderer.SetPosition(i,buffer[i].transform.position);
			}

		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//turn on the menu
		if (Input.GetKeyDown (KeyCode.M) || (WiimoteDemoButtons.wiiDetected && WiimoteDemoButtons.clicked.home)) {
			//GameObject clone = Instantiate(line, marker.position, marker.rotation) as GameObject;
			//clone.transform.parent = camera.transform;
			menuOpen = true;
		}

		if (Input.GetKeyDown (KeyCode.E) || (WiimoteDemoButtons.wiiDetected && !WiimoteDemoButtons.clicked.home && menuOpen)) {
			//GameObject clone = Instantiate(line, marker.position, marker.rotation) as GameObject;
			//clone.transform.parent = camera.transform;
			menuOpen = false;
		}

		if (menuOpen) {
			menu.SetActive (true);
		} else {
			menu.SetActive (false);
		}
		if (!menuOpen && !WiimoteDemoButtons.inspect) {
			if (Input.GetKeyDown (KeyCode.P) || (WiimoteDemoButtons.wiiDetected && WiimoteDemoButtons.clicked.b)) {
				//GameObject clone = Instantiate(line, marker.position, marker.rotation) as GameObject;
				//clone.transform.parent = camera.transform;
				isPainting = true;

				if (brush == Brushes.Line) {
					GameObject newLine = Instantiate (line, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					lineRenderer = newLine.GetComponent<LineRenderer> ();
				
					lineRenderer.material = new Material (Shader.Find ("Particles/Alpha Blended"));
					lineRenderer.SetColors (color, color);
					lineRenderer.SetWidth (size / 2, size / 2);
					lineRenderer.SetVertexCount (0);
					//lineRenderer.useWorldSpace = false;
					//lineRenderer.transform.parent = canvas.transform;
					index = 0;
				}

			}
	
			if (Input.GetKeyUp (KeyCode.P) || (WiimoteDemoButtons.wiiDetected && !WiimoteDemoButtons.clicked.b && isPainting)) {
				isPainting = false;
				if (buffer.Count > 0 && brush == Brushes.Line) {
					lineRenderer.useWorldSpace = false;
					lineRenderer.transform.parent = canvas.transform;
					/*GameObject newLine = Instantiate(line, new Vector3(0,0,0), Quaternion.identity) as GameObject;
				LineRenderer lr = newLine.GetComponent<LineRenderer>();
				
				lr.material = new Material(Shader.Find("Particles/Additive"));
				lr.SetColors(color, color);
				lr.SetWidth(size/2,size/2	);
				lr.SetVertexCount(buffer.Count);
				
				for (int i = 0; i < buffer.Count ; ++i) {
					Debug.Log("buffer "+i + " :" + buffer[i].transform.position.ToString("F4"));

					
					
					lr.SetPosition(i,buffer[i].transform.position);
				}
				newLine.transform.parent = canvas.transform;
				lr.useWorldSpace = false;
				*/
					foreach (GameObject obj in buffer) {
						Destroy (obj);
					}
					buffer.Clear ();
				
				}

			}
			//if (Input.GetKeyUp (KeyCode.S)) {

			//}
			if ((Input.GetKeyDown (KeyCode.B) || (WiimoteDemoButtons.clicked.plus)) && Time.time > nextAction) {
				nextAction = Time.time + rate;
				brush = (Brushes)(((int)brush + 1) % Enum.GetNames (typeof(Brushes)).Length);
			}
			if (WiimoteDemoButtons.clicked.minus && Time.time > nextAction) {
				nextAction = Time.time + rate;
				brush = (Brushes)(((int)brush + Enum.GetNames (typeof(Brushes)).Length - 1) % Enum.GetNames (typeof(Brushes)).Length);
			}
		}
	}
	
	void OnGUI()
	{
		GUILayout.Label("Press B to change Brush");
		GUILayout.Label("Current brush : " + brush);
	}
	
}
