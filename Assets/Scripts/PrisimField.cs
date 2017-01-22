using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PrisimField : MonoBehaviour {	

	public enum PrismTypes {
		SPLITTER,
		COMBINER,
	}

	public PrismTypes prismType;

	public List<LightPoly.Lights> triggerStates;
	public List<LightPoly.Lights> splitterStates;

	Dictionary<LightPoly.Lights, bool> currentStates;
	const float ACTIVE_TIMER = 2.0f; // The thing is active for two seconds before going inactive. 
	public const float SPEED_SCALAR = 2.0f;
	public float EntryAngle = 10.0f;

	float active_t;
	bool active;

	// Use this for initialization
	void Awake() {

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
				deactivatePrism();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Debug.Log("COLLISION HAPPENED");



		// Manipulate Rigidbody2D if it exists. 
		Rigidbody2D nrb = other.gameObject.GetComponent<Rigidbody2D>();
		// Debug.Log("Prevec: " + nrb.velocity);
		if(nrb != null)
		{
			nrb.velocity /= SPEED_SCALAR;
		}

		// Manipulate the LightPoly stuff
		LightPoly lp = other.gameObject.GetComponent<LightPoly>();
		if(lp != null) {
			// Print out the state for sanity
			// Debug.Log("Triggered. LightState: " + lp.lightState);

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

			switch(prismType)
			{	
			//=====================
			// SPLITTER
			//=====================
			case PrismTypes.SPLITTER:
				if(isactive) {
					activatePrism();

					if(!lp.isCopy() && (lp.lightState == triggerStates[0])){
						GameObject o = other.gameObject;

						GameObject n;
						LightPoly nlp;

						// Loop through created splits and create polygons for them.
						int count = 0;
						bool neg = false;
						foreach (var state in splitterStates) {
							// Debug.Log("SplitterState: " + state);
							Quaternion rot = 
								Quaternion.AngleAxis((EntryAngle * count) * 
								((neg) ? -1 : 1), Vector3.forward);

							n = Instantiate(o);
							n.transform.SetParent(o.transform.parent);
							n.transform.position = o.transform.position;
							n.transform.rotation = o.transform.rotation;
							n.transform.localScale = o.transform.localScale;
							nlp = n.GetComponent<LightPoly>();
							nrb = n.GetComponent<Rigidbody2D>();

							nlp.lightState = lp.lightState;

							nrb.velocity = o.GetComponent<Rigidbody2D>().velocity;

							// Debug.Log("Prerot: " + nrb.velocity);
							n.transform.rotation = n.transform.rotation * rot;

							// Debug.Log("Prevel: "+ nrb.velocity);
							nrb.velocity = rot * nrb.velocity;

							// Debug.Log("Postvel" + nrb.velocity);

							if(nlp != null && nrb != null){
								nlp.transitionToState(state);
								nlp.setCopy();
								// nrb.velocity /= SPEED_SCALAR;
							}
								// Quaternion.AngleAxis(30, Vector3.up);
							neg = !neg;
							if(neg == true){
								count++;
							}
						}	

						// After copies created and manipulated, blow 'em up;
						Destroy(o);
					}
				}
				else {
					deactivatePrism();
				}
				break;

			//=====================
			// COMBINER
			//=====================
			case PrismTypes.COMBINER:

				break;
			}
		}

		// Debug.Log(isActive());
	}

	void OnTriggerExit2D(Collider2D other) {
		// Debug.Log("LEFT FIELD");

		Rigidbody2D nrb = other.GetComponent<Rigidbody2D>();
		if(nrb != null) {
			// Debug.Log("Normal");
			nrb.velocity = nrb.velocity.normalized * SPEED_SCALAR;
			// nrb.velocity *= LightPoly.POLY_VEOLCITY_SCALAR;
			// Debug.Log(nrb.velocity);
		}

		LightPoly lp = other.GetComponent<LightPoly>();
		if(lp != null) {
			lp.deactivateCopy();
		}
	}

	void activatePrism() {
		active = true;
		active_t = ACTIVE_TIMER;
	}

	void deactivatePrism() {
		active = false;
		// deactivate all dictionary values. 
		List<LightPoly.Lights> keys = new List<LightPoly.Lights>(currentStates.Keys);
		foreach(var entry in keys) {
			currentStates[entry] = false;
		}
		active_t = 0f;
	}

	public bool isActive() {
		return active;
	}
}
