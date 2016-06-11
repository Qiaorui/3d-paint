using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public double rate = 0.3;
	public Color color;
	public float size = 1;
	public GameObject paint;
	public Transform marker;
	public Transform camera;
	private double nextAction;

	// Use this for initialization
	void Start () {
		Debug.Log("Controller starts");
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.P) && Time.time > nextAction) {
			nextAction = Time.time + rate;
			Debug.Log("Paint!");
			//GameObject clone = 
			//Instantiate(paint, marker.position, marker.rotation);
			GameObject clone = Instantiate(paint, marker.position, marker.rotation) as GameObject;
			clone.transform.localScale = new Vector3 (clone.transform.localScale.x * size, clone.transform.localScale.y * size, clone.transform.localScale.z * size);
			clone.transform.GetComponent<Renderer>().material.color = color;
			clone.transform.parent = camera.transform;
		}
	}
}
