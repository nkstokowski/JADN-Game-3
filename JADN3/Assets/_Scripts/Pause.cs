using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	bool isPaused = false;
	public GameObject pausedText;
	public GameObject menuText;
	public GameObject resetText;
	public Scene scene;

	// Update is called once per frame
	void Update () {
		scene = SceneManager.GetActiveScene ();

		if(Input.GetKeyUp(KeyCode.Escape)){
			TogglePaused();
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			SceneManager.LoadScene (scene.name);
		}
	}

	public void TogglePaused() {
		isPaused = !isPaused;
		if (isPaused){
			menuText.SetActive(true);
			pausedText.SetActive(true);
			resetText.SetActive (true);
			Time.timeScale = 0f;
		} else {
			menuText.SetActive(false);
			pausedText.SetActive(false);
			resetText.SetActive (false);
			Time.timeScale = 1f;
		}
	}

	public void Reload()
	{
		Time.timeScale = 1f;
		Debug.Log(scene.name);
		GetComponent<GameManager>().LoadLevel(scene.name);
	}

	public void ReturnToMenu(){
		Time.timeScale = 1f;
		GetComponent<GameManager>().LoadLevel("Start menu");
	}

	public void SkipTutorial(){
		Time.timeScale = 1f;
		GetComponent<GameManager> ().LoadLevel ("Level1");
	}
}
