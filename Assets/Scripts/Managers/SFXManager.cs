using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	public AudioClip jumpSFX;
	public AudioClip doorExitSFX;

	void Start () 
	{
		Registry.sfxManager = this;
	}

	public void PlaySFX(AudioClip audioClipToPlay)
	{
		AudioSource.PlayClipAtPoint(audioClipToPlay, Camera.main.transform.position);
	}
}
