using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CrystalControllerScript : MonoBehaviour {

	public List<Transform> CrystalTransforms;
	List<CrystalField> _crystals;

	public string NextLevel;

	const float WINTIME = 2.0f;
	float countdown = 0;

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
		if(!hasWon){
			bool didwin = true;
			foreach(var crys in _crystals) {
				if(crys.getState() != CrystalField.CrystalStates.ACTIVE) {
					didwin = false;
					break;
				}
			}
			hasWon = didwin;
			if(hasWon)
			{
				foreach(var cryst in _crystals) {
					cryst.ActivateTheGhost();
				}
				countdown = WINTIME;
			}
		}
		else {
			countdown -= Time.deltaTime;
			if(countdown < 0f) {
				SceneManager.LoadScene(NextLevel);
			}
		}

		Debug.Log(Won());
	}



	public bool Won() {
		return hasWon;
	}
}
