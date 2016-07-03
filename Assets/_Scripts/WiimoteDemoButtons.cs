using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using WiimoteApi;

public class WiimoteDemoButtons : MonoBehaviour {

    //public WiimoteModel model;

	public static WiiButtons wiiButtons; 
	public WiiButtons buttons; 
    public Wiimote wiimote;

    private Vector2 scrollPosition;

    private Vector3 wmpOffset = Vector3.zero;

    void Start() {
		wiiButtons = new WiiButtons ();
		buttons= wiiButtons;
    }

	void Update () {
		if (!WiimoteManager.HasWiimote ()) { 
			WiimoteManager.FindWiimotes ();
			return;
		} 
			


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

		wiimote.SendPlayerLED (true, false,false,false);

	
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


	}

    void OnGUI()
    {
		if (!WiimoteManager.HasWiimote ()) {
			
			GUI.Label (new Rect(Screen.width/2-100,20,200,200), "Wiimote Not Found!");

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
