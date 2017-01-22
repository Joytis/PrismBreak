using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour {

	public float frequency; // Objects per second
	public Transform thing;
	public LightPoly.Lights color;

	public AudioClip onClip;
	public AudioClip offClip;

	AudioSource onSource;
	AudioSource offSource;

	bool currentlyOn;
	float accumulatedTime;

	// Use this for initialization
	void Awake() {
		currentlyOn = false;
		onSource = gameObject.AddComponent<AudioSource>();
		offSource = gameObject.AddComponent<AudioSource>();

		onSource.clip = onClip;
		offSource.clip = offClip;

		onSource.volume = 0.8f;

		accumulatedTime = 0;
	}
 
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		accumulatedTime += dt;

		if(accumulatedTime > (1 / frequency)  && frequency != 0)
		{
			accumulatedTime = 0;
			if(currentlyOn){
				Transform cp = Instantiate(thing, transform.position, Quaternion.identity);


				cp.gameObject.GetComponent<Rigidbody2D>().velocity = 
					(transform.rotation * Vector2.right) * LightPoly.POLY_VEOLCITY_SCALAR;
				cp.SetParent(gameObject.transform);
				cp.rotation = transform.rotation;

				Vector2 offset = (cp.rotation * Vector2.right) * 0.7f;
				cp.position += (Vector3)offset;

				cp.GetComponent<LightPoly>().swapColorNow(color);
			}
			// Test stuff to check if the light color changing works. 
			// Array values = Enum.GetValues(typeof(LightPoly.Lights));
			// System.Random random = new System.Random();
			// LightPoly.Lights randomLight = (LightPoly.Lights)values.GetValue(random.Next(values.Length));

			// cp.gameObject.GetComponent<LightPoly>().changeLightState(randomLight);
		}
	}

	void OnMouseDown() {
		if(currentlyOn) {
			offSource.Play();
		}
		else {
			onSource.Play();
		}
		currentlyOn = !currentlyOn; // Toggle :D
	}

}
