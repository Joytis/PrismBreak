using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSound : MonoBehaviour {

	public AudioClip clip;
	AudioSource source;

	// Use this for initialization
	void Awake () {
		source = gameObject.AddComponent<AudioSource>();
		source.clip = clip;
	}
	
	void OnMouseDown() {
		source.Play();
	}
}
