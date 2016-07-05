using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{



	private List <GameObject> currentCollisions  = new List <GameObject> ();

	void FixedUpdate()
	{
		//if(WiimoteDemoButtons.wiiButtons.a==true)

		if (Input.GetKeyDown(KeyCode.R)||WiimoteDemoButtons.clicked.a) {
			foreach (GameObject gObject in currentCollisions) {
				//currentCollisions.Remove(gObject);
				Destroy(gObject);
			}
			currentCollisions.Clear();
		}

	}

	void OnTriggerEnter(Collider other)
	{
		// Add the GameObject collided with to the list.
		if (other.gameObject.CompareTag ("Paint")) {

			currentCollisions.Add (other.gameObject);
		}
		/*foreach (GameObject gObject in currentCollisions) {
             print (gObject.name);
         }*/
	}



	void OnTriggerExit (Collider other) {
		if (other.gameObject.CompareTag ("Paint")) {
			currentCollisions.Remove (other.gameObject);
		}
	}




}
