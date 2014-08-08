using UnityEngine;
using System.Collections;

static public class AudioManager {

	static public AudioSource PlayOneShotAudioClip(AudioClip audioClipToPlay)
	{
		GameObject oneShotGameObject = new GameObject("TempAudio");
		AudioSource oneShotAudioSource = oneShotGameObject.AddComponent("AudioSource") as AudioSource;
		oneShotAudioSource.clip = audioClipToPlay;
		oneShotGameObject.transform.parent = Camera.main.transform;
		oneShotGameObject.transform.localPosition = Vector3.zero;

		GameObject.Destroy(oneShotGameObject, audioClipToPlay.length);
		oneShotAudioSource.Play();

		return oneShotAudioSource;
	}
}
