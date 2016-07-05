using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour {


	public Transform camera;
	//private Vector3 offset;
	public Vector3 offset,offsetR;
	public bool inspect,rotate;

	// Use this for initialization
	void Start () {
	//	offset = transform.position - camera.position;
		offset=new Vector3(0,0,0);
		inspect = false;
		offsetR=new Vector3(0,0,0);
		rotate = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (!inspect) {
			transform.rotation = camera.rotation;
			transform.position = camera.position;
		} else {
			//transform.rotation = camera.rotation;
			transform.localPosition =offset;
			transform.Rotate (offsetR);
		}
	}
		void Update () {
		if (!WiimoteDemoButtons.menu) {
			inspect = WiimoteDemoButtons.inspect;
			rotate = WiimoteDemoButtons.rotate;
			if (inspect)
			if (!rotate) {
				if (WiimoteDemoButtons.clicked.d_left)
					offset.x += 0.01f;
				if (WiimoteDemoButtons.clicked.d_right)
					offset.x -= 0.01f;
				if (WiimoteDemoButtons.clicked.d_down)
					offset.y -= 0.01f;
				if (WiimoteDemoButtons.clicked.d_up)
					offset.y += 0.01f;
				if (WiimoteDemoButtons.clicked.plus)
					offset.z -= 0.01f;
				if (WiimoteDemoButtons.clicked.minus)
					offset.z += 0.01f;
			
			} else {
				if (WiimoteDemoButtons.clicked.d_left)
					offsetR.x += 1f;
				if (WiimoteDemoButtons.clicked.d_right)
					offsetR.x -= 1f;
				if (WiimoteDemoButtons.clicked.d_down)
					offsetR.y -= 1f;
				if (WiimoteDemoButtons.clicked.d_up)
					offsetR.y += 1f;
				if (WiimoteDemoButtons.clicked.plus)
					offsetR.z -= 1f;
				if (WiimoteDemoButtons.clicked.minus)
					offsetR.z += 1f;

			}
		}
	}

}
