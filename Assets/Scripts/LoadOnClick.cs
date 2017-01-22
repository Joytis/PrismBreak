using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

    public void LoadScene(int level)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void LoadScene(string sname)
    {
    	loadingImage.SetActive(true);
    	SceneManager.LoadScene(sname);
    }
}
