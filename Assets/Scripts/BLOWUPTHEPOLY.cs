using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLOWUPTHEPOLY : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		// Manipulate Rigidbody2D if it exists. 
		LightPoly lp = other.gameObject.GetComponent<LightPoly>();
		if(lp != null) {
			Destroy(lp.gameObject);
		}
	}
}
