using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Behaviour))]
[RequireComponent (typeof(AudioSource))]
public class CrystalField : MonoBehaviour {

	public enum CrystalStates {
		IDLE,
		ACTIVE
	};

	CrystalStates state;
	// SpriteRenderer sr;
	Behaviour halo;
	AudioSource ac;

	public GameObject ghostThing;

	public List<LightPoly.Lights> triggerStates;
	Dictionary<LightPoly.Lights, bool> currentStates;

	Dictionary<LightPoly.Lights, float> timeMap;

	const float ACTIVE_TIMER = 2.0f; // The thing is active for two seconds before going inactive. 
	public const float SPEED_SCALAR = 2.0f;

	// float active_t;
	bool active;
	bool toggle;

	// Use this for initialization
	void Awake () {
		toggle = false;
		// sr = GetComponent<SpriteRenderer>();
		ac = GetComponent<AudioSource>();
		halo = (Behaviour)GetComponent("Halo");
		state = CrystalStates.IDLE;
		// active_t = 0;
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

		timeMap = new Dictionary<LightPoly.Lights, float>() {
			{LightPoly.Lights.WHITE, 0.0f}, 
			{LightPoly.Lights.RED, 0.0f}, 
			{LightPoly.Lights.GREEN, 0.0f},
			{LightPoly.Lights.BLUE, 0.0f},
			{LightPoly.Lights.YELLOW, 0.0f},
			{LightPoly.Lights.MAGENTA, 0.0f},
			{LightPoly.Lights.CYAN, 0.0f},
			{LightPoly.Lights.BLACK, 0.0f},
		};
	}
	
	// Update is called once per frame
	void Update () {
		// if(isActive()) {
		// 	active_t -= Time.deltaTime;
		// 	if(active_t < 0){
		// 		deactivateCrystal();
		// 	}
		// }

		if(isActive()) {
			foreach(var trig in triggerStates) {	
				timeMap[trig] -= Time.deltaTime;
				if(timeMap[trig] < 0){
					deactivateCrystal();
				}
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
			timeMap[lp.lightState] = 2.0f;

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
		if(toggle == false) {
			toggle = true;
			ac.Play();
		}
		active = true;
		// active_t = ACTIVE_TIMER;
		state = CrystalStates.ACTIVE;
		// sr.color = LightPoly.colorMap[triggerStates[0]];
		halo.enabled = true;
		// Debug.Log(halo.enabled);
	}

	void deactivateCrystal() {
		active = false;
		toggle = false;
		// deactivate all dictionary values. 
		List<LightPoly.Lights> keys = new List<LightPoly.Lights>(currentStates.Keys);
		foreach(var entry in keys) {
			timeMap[entry] = 0.0f;
			currentStates[entry] = false;
		}

		// active_t = 0f;
		state = CrystalStates.IDLE;
		// sr.color = new Color(1f, 1f, 1f, 1f);
		halo.enabled = false;
	}

	public bool isActive() {
		return active;
	}

	public void ActivateTheGhost() {
		
	}

	public CrystalStates getState () { return state; }
}
