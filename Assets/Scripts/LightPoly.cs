using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPoly : MonoBehaviour {

	public enum Lights {
		WHITE, 
		RED, 
		GREEN,
		BLUE,
		YELLOW,
		MAGENTA,
		CYAN,
		BLACK
	};

	public Lights lightState;

	private SpriteRenderer sr;

	public void changeLightState(Lights state) {
		lightState = state;
		switch(lightState) {
			case Lights.WHITE:
				sr.color = new Color(1f, 1f, 1f, 1f);
				break;  
			case Lights.RED:
				sr.color = new Color(1f, 0f, 0f, 1f);
				break;  
			case Lights.GREEN:
				sr.color = new Color(0f, 1f, 0f, 1f);
				break; 
			case Lights.BLUE:
				sr.color = new Color(0f, 0f, 1f, 1f);
				break; 
			case Lights.YELLOW:
				sr.color = new Color(1f, 1f, 0f, 1f);
				break; 
			case Lights.MAGENTA:
				sr.color = new Color(0f, 1f, 1f, 1f);
				break; 
			case Lights.CYAN:
				sr.color = new Color(1f, 0f, 1f, 1f);
				break; 
			case Lights.BLACK:
				sr.color = new Color(0f, 0f, 0f, 1f);
				break;
		}
	}

	// Use this for initialization
	void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
