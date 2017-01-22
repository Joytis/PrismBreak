using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverAndClickNoises : MonoBehaviour {

	public AudioClip mouseClip;
	public AudioClip clickClip;

	AudioSource mouseSource;
	AudioSource clickSource;

	void Awake() {
		mouseSource = gameObject.AddComponent<AudioSource>();
		clickSource = gameObject.AddComponent<AudioSource>();

		mouseSource.clip = mouseClip;
		clickSource.clip = clickClip;
	}
	
	void OnMouseEnter() {
		mouseSource.Play();
	}

	void OnMouseDown() {
		clickSource.Play();
	}
}

