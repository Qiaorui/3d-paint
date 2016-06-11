using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour {


	public Transform camera;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - camera.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = camera.position + offset;
	}
}
