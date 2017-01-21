using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class CrystalField : MonoBehaviour {

	public enum CrystalStates {
		IDLE,
		ACTIVE
	};

	CrystalStates state;
	SpriteRenderer sr;

	public List<LightPoly.Lights> triggerStates;
	Dictionary<LightPoly.Lights, bool> currentStates;

	const float ACTIVE_TIMER = 2.0f; // The thing is active for two seconds before going inactive. 
	public const float SPEED_SCALAR = 2.0f;

	float active_t;
	bool active;

	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer>();
		state = CrystalStates.IDLE;
		active_t = 0;
		// Initialize the current list of stuff in the dict
		currentStates = new Dictionary<LightPoly.Lights, bool>() {
			{LightPoly.Lights.WHITE, false}, 
			{LightPoly.Lights.RED, false}, 
			{LightPoly.Lights.GREEN, false},
			{LightPoly.Lights.BLUE, false},
			{LightPoly.Lights.YELLOW, false},
			{LightPoly.Lights.MAGENTA, false},
			{LightPoly.Lights.CYAN, false},
			{LightPoly.Lights.BLACK, false},
		};
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive()) {
			active_t -= Time.deltaTime;
			if(active_t < 0){
				deactivateCrystal();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Debug.Log("COLLISION HAPPENED");

		// Manipulate the LightPoly stuff
		LightPoly lp = other.gameObject.GetComponent<LightPoly>();
		if(lp != null) {
			// Assume true until proven false
			bool isactive = true;

			// Itterate through prerequisite states. 
			currentStates[lp.lightState] = true;
			foreach (var trig in triggerStates) {
				// If it's not true, it's not active. 
				if(currentStates[trig] != true) {
					isactive = false;
				}
			}

			if(isactive) {
				activateCrystal();
			}

		}
		Destroy(other.gameObject);

	}

	void activateCrystal() {
		active = true;
		active_t = ACTIVE_TIMER;
		state = CrystalStates.ACTIVE;
		sr.color = LightPoly.colorMap[triggerStates[0]];
	}

	void deactivateCrystal() {
		active = false;
		// deactivate all dictionary values. 
		List<LightPoly.Lights> keys = new List<LightPoly.Lights>(currentStates.Keys);
		foreach(var entry in keys) {
			currentStates[entry] = false;
		}
		active_t = 0f;
		state = CrystalStates.IDLE;
		sr.color = new Color(1f, 1f, 1f, 0.30f);
	}

	public bool isActive() {
		return active;
	}

	public CrystalStates getState () { return state; }
}
