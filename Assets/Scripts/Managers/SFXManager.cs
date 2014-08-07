using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	public AudioClip SFXJump;
	public AudioClip SFXDoorExit;
	public AudioClip SFXHazardShock;

	void Start () 
	{
		Registry.sfxManager = this;
	}

	public void PlaySFX(AudioClip audioClipToPlay)
	{
		AudioSource.PlayClipAtPoint(audioClipToPlay, Camera.main.transform.position);
	}
}
