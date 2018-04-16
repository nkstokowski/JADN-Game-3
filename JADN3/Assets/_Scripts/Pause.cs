using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	bool isPaused = false;
	public GameObject pausedText;
	public GameObject menuText;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape)){
			TogglePaused();
		}
	}

	public void TogglePaused() {
		isPaused = !isPaused;
		if (isPaused){
			menuText.SetActive(true);
			pausedText.SetActive(true);
			Time.timeScale = 0f;
		} else {
			menuText.SetActive(false);
			pausedText.SetActive(false);
			Time.timeScale = 1f;
		}
	}

	public void ReturnToMenu(){
		SceneManager.LoadScene("Menu");
	}
}
