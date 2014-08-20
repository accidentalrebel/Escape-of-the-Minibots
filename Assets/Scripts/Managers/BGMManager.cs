using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

	private bool _enabled = true;

	Object[] _bgmList;
	int _currentBGMIndex = -1;
	AudioSource _currentAudioSource;

	// ************************************************************************************
	// PUBLIC FUNCITONS
	// ************************************************************************************
	public void enable()
	{
		if ( _enabled )
			return;

		_enabled = true;
		_currentAudioSource.enabled = true;
	}

	public void disable()
	{
		if ( !_enabled )
			return;

		_enabled = false;
		_currentAudioSource.enabled = false;
	}

	public void toggleStatus()
	{
		if ( _enabled )
			disable();
		else
			enable();
	}

	// ************************************************************************************
	// MAIN
	// ************************************************************************************
	void Start()
	{
		Registry.bgmManager = this;

		_bgmList = Resources.LoadAll("Audio/BGM");

#if UNITY_EDITOR
		if ( Registry.debugConfig.disableBGMOnStartup )
			return;
#endif
		PlayNextTrack();
	}

	// ************************************************************************************
	// BGM PLAYING
	// ************************************************************************************
	void PlayNextTrack()
	{
		_currentBGMIndex++;
		if ( _currentBGMIndex >= _bgmList.Length )
			_currentBGMIndex = 0;

		AudioClip currentlyPlayingAudio = _bgmList[_currentBGMIndex] as AudioClip;
		_currentAudioSource = AudioManager.PlayOneShotAudioClip(currentlyPlayingAudio);

		Invoke("PlayNextTrack", currentlyPlayingAudio.length);
	}
}
