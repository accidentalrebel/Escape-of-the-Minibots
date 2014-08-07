using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	public AudioClip jumpSFX;

	void Start () 
	{
		Registry.sfxManager = this;
	}

	public void PlaySFX(string sfxName)
	{
		AudioClip audioClipToPlay = new AudioClip();
		if ( sfxName == "jump" )
			audioClipToPlay = jumpSFX;

		AudioSource.PlayClipAtPoint(audioClipToPlay, Camera.main.transform.position);
	}
}
