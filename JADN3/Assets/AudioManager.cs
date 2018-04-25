using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	//Plays a sound.
	public void PlaySound(GameObject audioSource, Sound sound){
		//If the object we are sending doesn't have an Audio source, use the manager's.
		if(audioSource.GetComponent<AudioSource>() == null)	
			GetComponent<AudioSource>().PlayOneShot(sound.clip);
		audioSource.GetComponent<AudioSource>().PlayOneShot(sound.clip);
	}

	public void PlaySoundWithTag(string tag){
		for(int i=0;i<sounds.Length;i++){
			if(sounds[i].tag == tag){
				GetComponent<AudioSource>().PlayOneShot(sounds[i].clip);
			}
		}
	}

	//This returns a sound by a tag.
	public Sound GetSoundByTag(string tag){
		for(int i = 0; i < sounds.Length; i++){
			if(sounds[i].tag == tag){
				return sounds[i];	//Returns the correct Sound.
			}
		}
		return sounds[0];	//Returns default sound.
	}

	public Sound GetSoundByObject(Sound sound){
		for(int i=0;i< sounds.Length; i++){
			if(SoundsMatch(sounds[i],sound)){
				return sounds[i];
			}
		}
		return sounds[0];	//Return default sound if nothing else.
	}

	private bool SoundsMatch(Sound s1, Sound s2){
		return (s1.clip == s2.clip && s1.tag == s2.tag);
	}

}

[System.Serializable]
public struct Sound{
	public AudioClip clip;
	public string tag;

	public Sound(AudioClip clip, string tag){
		this.clip = clip;
		this.tag = tag;
	}
}
