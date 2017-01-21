using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class MirrorScript : MonoBehaviour {

	// // Use this for initialization
	// void Start () {
		
	// }
	public LightPoly.Lights triggerState;
	public LightPoly.Lights outputState;
	public LightPoly.Lights thisState;

	Transform tr;
	SpriteRenderer sr;

	void Awake() {
		tr = transform;
		sr = GetComponent<SpriteRenderer>();
		if(thisState != LightPoly.Lights.NULL)
		sr.color = LightPoly.colorMap[thisState]; 
	}
	
	// Update is called once per frame
	// void Update () {
		
	// }

	void OnTriggerEnter2D(Collider2D other) {
		GameObject o = other.gameObject;
		LightPoly olp = o.GetComponent<LightPoly>();
		Transform ot = o.transform;
		Rigidbody2D orb = o.GetComponent<Rigidbody2D>();

		// TODO
		if(triggerState != LightPoly.Lights.NULL)
		{

		}

		// TODO
		if(thisState != LightPoly.Lights.NULL) 
		{

		}

		// Reflects the veolcity relative to the normal of the perpendicular rotation revtor. 
		Vector2 rot = tr.rotation * Vector2.up;
		Vector2 perp = Vector3.Cross(rot, Vector3.forward).normalized;
		Vector2 vel = orb.velocity;

		// r=d−2(d⋅n)n
		Vector2 reflection = (vel - (2*(Vector2.Dot(vel, perp) * perp)));
		orb.velocity = reflection;
		ot.rotation = Quaternion.FromToRotation(Vector2.right, reflection.normalized);
	}
}
