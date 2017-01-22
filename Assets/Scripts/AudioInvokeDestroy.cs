using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioInvokeDestroy : MonoBehaviour {

	AudioSource asource;

	// Use this for initialization
	void Awake() {
		asource = GetComponent<AudioSource>();
	}
	
	public void InvokeDestroyAndPlay() {
		asource.Play();
		Invoke("KillMe", 1.0f);
	}

	void KillMe() {
		Destroy(gameObject);
	}
}
