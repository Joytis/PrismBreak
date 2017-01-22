using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnButts : MonoBehaviour {

	public AudioClip grab;
	public AudioClip drop;

	AudioSource gsource;
	AudioSource dsource;

	// Use this for initialization
	void Awake () {
		gsource = gameObject.AddComponent<AudioSource>();
		dsource = gameObject.AddComponent<AudioSource>();

		gsource.clip = grab;
		dsource.clip = drop;
	}
	
	void OnMouseDown() {
		gsource.Play();
	}

	void OnMouseUp() {
		dsource.Play();
	}
}
