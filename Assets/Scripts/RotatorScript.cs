using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour {

	private float _sensitivity;
	private Vector3 _mouseReference;
	private Vector3 _mouseOffset;
	private Vector3 _rotation;
	private bool _isRotating;

	public AudioClip sandmove;
	AudioSource sandSource;

	void Start ()
	{
		_sensitivity = 0.4f;
		_rotation = Vector3.zero;

		sandSource = gameObject.AddComponent<AudioSource>();
		sandSource.clip = sandmove;
		sandSource.volume = 0.02f;
	}

	void Update()
	{
		if(_isRotating)
		{
			// offset
			_mouseOffset = (Input.mousePosition - _mouseReference);
			// Debug.Log(_mouseOffset);

			// apply rotation
			_rotation.z = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
			// Debug.Log(_rotation);

			// rotate
			transform.parent.Rotate(_rotation);

			// store mouse
			_mouseReference = Input.mousePosition;
		}
	}

	void OnMouseDown()
	{
		// Debug.Log("MOUSEDOWN");
	 	// rotating flag
		_isRotating = true;

		sandSource.Play();
	 
	 	// store mouse
	 	_mouseReference = Input.mousePosition;
	}

	void OnMouseUp()
	{
	 // rotating flag
		_isRotating = false;
	}	
}
