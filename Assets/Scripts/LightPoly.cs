using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]
public class LightPoly : MonoBehaviour {

	public enum Lights {
		NULL,
		WHITE, 
		RED, 
		GREEN,
		BLUE,
		YELLOW,
		MAGENTA,
		CYAN,
		BLACK,
	};

	public const float POLY_VEOLCITY_SCALAR = 2.0f;

	public Lights lightState;
	Lights previouState;

	public const float transitionTime = 1.0f;

	float passedTime;
	// Color transitionColor; // Differential color for the transition
	float tR;  // Transitory red variable
	float tG;  // transitory Green var
	float tB;  // transitory blue var

	// Lights transitionState;
	bool transitioning;

	bool copyCreated;

	static Dictionary<Lights, Color> colorMap = new Dictionary<Lights, Color> {
		{Lights.WHITE, 		new Color(1f, 1f, 1f, 1f) },
		{Lights.RED, 		new Color(1f, 0f, 0f, 1f) },
		{Lights.GREEN, 		new Color(0f, 1f, 0f, 1f) },
		{Lights.BLUE, 		new Color(0f, 0f, 1f, 1f) },
		{Lights.YELLOW, 	new Color(1f, 1f, 0f, 1f) },
		{Lights.MAGENTA, 	new Color(0f, 1f, 1f, 1f) },
		{Lights.CYAN, 		new Color(1f, 0f, 1f, 1f) },
		{Lights.BLACK, 		new Color(0f, 0f, 0f, 1f) },
	};

	private SpriteRenderer sr;

	public void changeLightState(Lights state) {
		lightState = state;
		sr.color = colorMap[lightState];
	}

	// Use this for initialization
	void Awake() {
		transitioning = false;
		sr = GetComponent<SpriteRenderer>();
		passedTime = 0f;
		copyCreated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(transitioning) 
		{
			// Increment time
			passedTime += Time.deltaTime;
			float cmult = (passedTime / transitionTime);

			// Debug.Log(lightState);

			// Check for end case
			if(cmult > 1.0f) {
				transitioning = false;
				sr.color = colorMap[lightState];
			}
			else {
				// Otherwise transition color
				Color cs = colorMap[previouState];

				sr.color = new Color(
					(cs.r - (cmult * tR)),
					(cs.g - (cmult * tG)),
					(cs.b - (cmult * tB)),
					1f
				);
			}

			// Debug.Log(sr.color);
		}
	}

	// How to gradually change colors
	public void transitionToState(Lights state) {
		passedTime = 0f;
		transitioning = true;

		previouState = lightState;

		// Color ct; // Transition color
		// Color cs; // State Color
		Color ct = colorMap[state];
		Color cs = colorMap[lightState];

		lightState = state;

		tR = cs.r - ct.r;
		tG = cs.g - ct.g;
		tB = cs.b - ct.b;

	}

	public void setCopy(){ copyCreated = true; }
	public bool isCopy() { return copyCreated; }
	public void deactivateCopy() { copyCreated = false;}
}
