using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalControllerScript : MonoBehaviour {

	public List<Transform> CrystalTransforms;
	List<CrystalField> _crystals;

	bool hasWon;

	// Use this for initialization
	void Awake () {
		_crystals = new List<CrystalField>();

		foreach(var trans in CrystalTransforms) {
			_crystals.Add(trans.gameObject.GetComponent<CrystalField>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool didwin = true;
		foreach(var crys in _crystals) {
			if(crys.getState() != CrystalField.CrystalStates.ACTIVE) {
				didwin = false;
				break;
			}
		}
		hasWon = didwin;

		Debug.Log(Won());
	}

	public bool Won() {
		return hasWon;
	}
}
