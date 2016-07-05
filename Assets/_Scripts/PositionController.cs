using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour {


	public Transform camera;
	//private Vector3 offset;
	public Vector3 offset,offsetR;
	public bool inspect,rotation;

	// Use this for initialization
	void Start () {
	//	offset = transform.position - camera.position;
		offset=new Vector3(0,0,0);
		offsetR=new Vector3(0,0,0);
		inspect = false;
		rotation = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (!inspect) {
			transform.rotation = camera.rotation;
			transform.position = camera.position;
		} else {
			//transform.rotation = camera.rotation;
			transform.localPosition =offset;
			transform.Rotate(offsetR);
			offsetR.x = offsetR.y = offsetR.z = 0;
		}
	}
		void Update () {
		inspect = WiimoteDemoButtons.inspect;
		rotation = WiimoteDemoButtons.rotate;
		if (inspect && !rotation) {
			if (WiimoteDemoButtons.clicked.d_left)
				offset.x += 0.01f;
			if (WiimoteDemoButtons.clicked.d_right)
				offset.x -= 0.01f;
			if (WiimoteDemoButtons.clicked.d_down)
				offset.y -= 0.01f;
			if (WiimoteDemoButtons.clicked.d_up)
				offset.y += 0.01f;
			if (WiimoteDemoButtons.clicked.minus)
				offset.z -= 0.01f;
			if (WiimoteDemoButtons.clicked.plus)
				offset.z += 0.01f;
			
		} else {
			if (WiimoteDemoButtons.clicked.d_left)
				offsetR.x += 0.01f;
			if (WiimoteDemoButtons.clicked.d_right)
				offsetR.x -= 0.01f;
			if (WiimoteDemoButtons.clicked.d_down)
				offsetR.y -= 0.01f;
			if (WiimoteDemoButtons.clicked.d_up)
				offsetR.y += 0.01f;
			if (WiimoteDemoButtons.clicked.minus)
				offsetR.z -= 0.01f;
			if (WiimoteDemoButtons.clicked.plus)
				offsetR.z += 0.01f;

		}
	}

}
