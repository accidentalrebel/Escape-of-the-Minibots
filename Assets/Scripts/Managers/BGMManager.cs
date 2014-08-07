using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

	Object[] _bgmList;

	void Start()
	{
		_bgmList = Resources.LoadAll("Audio/BGM");

		PlayBGM(0);
	}

	void PlayBGM(int trackNumber)
	{
		AudioClip audioToPlay = _bgmList[trackNumber] as AudioClip;
		AudioSource.PlayClipAtPoint(audioToPlay, Camera.main.transform.position);
	}
}
