using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour {


	public Transform camera;
	//private Vector3 offset;
	public Vector3 offset;
	public bool inspect;

	// Use this for initialization
	void Start () {
	//	offset = transform.position - camera.position;
		offset=new Vector3(0,0,0);
		inspect = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (!inspect) {
			transform.rotation = camera.rotation;
			transform.position = camera.position;
		} else {
			//transform.rotation = camera.rotation;
			transform.localPosition =offset;
		}
	}
		void Update () {
		inspect = WiimoteDemoButtons.inspect;
		if (inspect) {
			if (WiimoteDemoButtons.clicked.d_left)
				offset.x += 0.01f;
			if (WiimoteDemoButtons.clicked.d_right)
				offset.x -= 0.01f;
			if (WiimoteDemoButtons.clicked.d_down)
				offset.y -= 0.01f;
			if (WiimoteDemoButtons.clicked.d_up)
				offset.y += 0.01f;
			
		}
		}

}
