using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour {

	public float frequency; // Objects per second
	public Transform thing;

	float accumulatedTime;

	// Use this for initialization
	void Awake() {
		accumulatedTime = 0;
	}
 
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		accumulatedTime += dt;

		if(accumulatedTime > (1 / frequency)  && frequency != 0)
		{
			accumulatedTime = 0;
			Transform cp = Instantiate(thing, transform.position, Quaternion.identity);

			cp.gameObject.GetComponent<Rigidbody2D>().velocity = (transform.rotation * Vector2.right) * 2;
			cp.SetParent(gameObject.transform);


			// Test stuff to check if the light color changing works. 
			// Array values = Enum.GetValues(typeof(LightPoly.Lights));
			// System.Random random = new System.Random();
			// LightPoly.Lights randomLight = (LightPoly.Lights)values.GetValue(random.Next(values.Length));

			// cp.gameObject.GetComponent<LightPoly>().changeLightState(randomLight);
		}
	}
}
