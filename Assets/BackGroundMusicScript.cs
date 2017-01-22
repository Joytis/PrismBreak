using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicScript : MonoBehaviour {

	private static BackGroundMusicScript playerInstance;
	
	void Awake(){
		if (playerInstance == null) {
			playerInstance = this;
		} else {
			DestroyObject(gameObject);
		}
	}
}
