using UnityEngine;
using System.Collections;

public class ColliderScript : MonoBehaviour {

	private Color originalColor;

	void OnTriggerEnter(Collider other)
	{
		originalColor = transform.GetComponent<Renderer>().material.color;
		transform.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnTriggerExit(Collider other)
	{
		transform.GetComponent<Renderer>().material.color = originalColor;
	}


}
