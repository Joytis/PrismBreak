using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAllCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>());
	}
}
