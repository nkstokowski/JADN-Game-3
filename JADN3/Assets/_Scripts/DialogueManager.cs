using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Queue<string> sentences;
	public string toLevelName;
	public Text storyText;
	public GameObject continueButton;

	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue){
		Debug.Log(dialogue.sentences.Length);
		sentences = new Queue<string>();

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence() {
		if(sentences.Count == 0) {
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence) {
		storyText.text += "";
		for(int i = 0; i< sentence.ToCharArray().Length; i++){
			storyText.text += sentence[i];
			yield return new WaitForSeconds(0.025f);
		}
		storyText.text += "\n";
		float randomWaitTime = Random.Range(0.75f,2f);
		yield return new WaitForSeconds(randomWaitTime);
		DisplayNextSentence();
	}

	public void EndDialogue(){
		continueButton.SetActive(true);
	}

}
