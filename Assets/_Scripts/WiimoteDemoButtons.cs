using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using WiimoteApi;

public class WiimoteDemoButtons : MonoBehaviour {

    //public WiimoteModel model;

	public WiiButtons wiiButtons; 
	public WiiButtons buttons;
	public static WiiButtons clicked;
    public Wiimote wiimote;
	public static bool inspect,rotate,menu;
	public static bool wiiDetected;

    private Vector2 scrollPosition;

    private Vector3 wmpOffset = Vector3.zero;

    void Start() {
		wiiButtons = new WiiButtons ();
		clicked = new WiiButtons ();
		buttons= clicked;
		clicked.one = false;
		clicked.a = false;
		clicked.b = false;
		clicked.home = false;
		wiiDetected = false;
    }

	void Update () {
		if (!WiimoteManager.HasWiimote ()) { 
			WiimoteManager.FindWiimotes ();
			wiiDetected = false;
			return;
		} 
		wiiDetected = true;	


        wiimote = WiimoteManager.Wiimotes[0];
		int ret;
		do
		{
			ret = wiimote.ReadWiimoteData();

			if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS) {
				Vector3 offset = new Vector3(  -wiimote.MotionPlus.PitchSpeed,
					wiimote.MotionPlus.YawSpeed,
					wiimote.MotionPlus.RollSpeed) / 95f; // Divide by 95Hz (average updates per second from wiimote)
				wmpOffset += offset;


			}
		} while (ret > 0);
			
		wiiButtons.a = wiimote.Button.a;
		wiiButtons.b= wiimote.Button.b;
		wiiButtons.one = wiimote.Button.one;
		wiiButtons.two= wiimote.Button.two;
		wiiButtons.d_up = wiimote.Button.d_up;
		wiiButtons.d_down = wiimote.Button.d_down;
		wiiButtons.d_left= wiimote.Button.d_left;
		wiiButtons.d_right = wiimote.Button.d_right;
		wiiButtons.plus = wiimote.Button.plus;
		wiiButtons.minus = wiimote.Button.minus;
		wiiButtons.home= wiimote.Button.home;
		if (wiiButtons.one == true) {
			if (clicked.one == false) {
				if (!inspect) {
					inspect = true;
					rotate = false;
				} else if (rotate)
					rotate = false;
				else
					inspect = false;
				clicked.one = true;
			}
		} else
			clicked.one = false;

		if (wiiButtons.two == true) {
			if (clicked.two == false) {
				if (!inspect) {
					inspect = true;
					rotate = true;
				} else if (rotate)
					inspect = false;
				else
					rotate = true;
				clicked.two = true;
			}
		} else
			clicked.two = false;

		
		if (wiiButtons.a == true) {
			if (clicked.a == false) {
				clicked.a = true;
			}
		} else
			clicked.a = false;
		
		if (wiiButtons.b == true) {
			if (clicked.b == false) {
				clicked.b = true;
			}
		} else
			clicked.b = false;
		
		if (wiiButtons.plus == true) {
			if (clicked.plus == false) {
				clicked.plus = true;
			}
		} else
			clicked.plus = false;

		if (wiiButtons.minus == true) {
			if (clicked.minus == false) {
				clicked.minus = true;
			}
		} else
			clicked.minus = false;
		if (wiiButtons.home == true) {
			if (clicked.home == false) {
				menu = !menu;
				clicked.home = true;
			}
		} else
			clicked.home = false;
		clicked.d_up = wiiButtons.d_up;
		clicked.d_down = wiiButtons.d_down;
		clicked.d_left = wiiButtons.d_left;
		clicked.d_right = wiiButtons.d_right;



		wiimote.SendPlayerLED (true, inspect&&!rotate,rotate,false);


	}

    void OnGUI()
    {
		if (!WiimoteManager.HasWiimote ()) {
			
			//GUI.Label (new Rect(Screen.width/2-100,20,200,200), "Wiimote Not Found!");

		} else {
			//GUI.Label (new Rect(Screen.width/2-100,20,200,200), "Wiimote Found!");
		}
    }

    
	[System.Serializable]
	public class WiiButtons
	{
		public bool a;
		public bool b;
		public bool one;
		public bool two;
		public bool d_up;
		public bool d_down;
		public bool d_left;
		public bool d_right;
		public bool plus;
		public bool minus;
		public bool home;
	}

	void OnApplicationQuit() {
		if (wiimote != null) {
			WiimoteManager.Cleanup(wiimote);
	        wiimote = null;
		}
	}
}
