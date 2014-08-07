using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

	Object[] _bgmList;
	int _currentBGMIndex = -1;

	void Start()
	{
		_bgmList = Resources.LoadAll("Audio/BGM");

		PlayNextTrack();
	}

	void PlayNextTrack()
	{
		_currentBGMIndex++;
		if ( _currentBGMIndex >= _bgmList.Length )
			_currentBGMIndex = 0;

		AudioClip audioToPlay = _bgmList[_currentBGMIndex] as AudioClip;
		AudioSource.PlayClipAtPoint(audioToPlay, Camera.main.transform.position);

		Invoke( "PlayNextTrack", audioToPlay.length );
	}
}
