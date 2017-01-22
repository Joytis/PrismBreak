using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritStuff : MonoBehaviour {

	float freq;
	float accumulator;
	float start_x;

	// Use this for initialization
	void Start () {
		freq = Random.Range(4.0f, 7.0f);
		accumulator = 0.0f;
		start_x = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		accumulator += Time.deltaTime;
		float newposy = transform.position.y + Time.deltaTime;
		float newposx = start_x + ((Mathf.Sin(freq * accumulator)) / 5);

		transform.position = new Vector2(newposx, newposy);
		transform.localScale = transform.localScale / 1.02f;
	}

	public void SetX(float x) {
		start_x = x;
	}
}
	