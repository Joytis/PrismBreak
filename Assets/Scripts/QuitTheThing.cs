using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitTheThing : MonoBehaviour {

	public void DoTheQuit() {
		Debug.Log("QUITTING");
		Application.Quit();
	}
}
