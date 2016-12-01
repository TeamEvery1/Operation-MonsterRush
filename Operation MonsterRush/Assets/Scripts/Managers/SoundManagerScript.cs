using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioClipID
{
	BGM_MAIN_MENU = 0,
	BGM_BATTLE = 1,
	BGM_GAMEOVER = 2,

	SFX_BUTTONPRESSED1 = 101,
	SFX_BUTTONPRESSED2 = 102,
	SFX_BACKSPACE = 103,
	SFX_SOUNDCHECK = 104,
	SFX_PLAYERATK1 = 105,
	SFX_PLAYERATK2 = 106,
	SFX_PLAYERATK3 = 107,
	SFX_PLAYERATKMISS = 108,
	SFX_PLAYERDEATH = 109,
	SFX_PLAYERCAPTURE = 110,
	SFX_PLAYERCAPTUREDURATION = 111,
	SFX_PLAYERCAPTURESUCCESS = 112,
	SFX_MONSTERALERT = 113,
	SFX_MONSTERHIT = 114,
	SFX_WAVE= 115,
	SFX_HEAL = 116,
	SFX_HITTREE = 117,
	SFX_PLAYERWALK = 118,
	SFX_PLAYERJUMP = 119,
	SFX_PLAYERLAND = 121,
	SFX_HITCRATE = 122,
	SFX_SWIMMING = 123,
	SFX_PLAYERWALKWOOD = 124,
	SFX_COINCOLLECT = 125,
	SFX_MONSTERCOUNTERDECREASE = 126,
	SFX_TIMERALERT = 127,


	TOTAL = 9001
}

[System.Serializable]
public class AudioClipInfo
{
	public AudioClipID audioClipID;
	public AudioClip audioClip;
}


public class SoundManagerScript : MonoBehaviour 
{
	private static SoundManagerScript mInstance;
	
	public static SoundManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				if(GameObject.FindWithTag("SoundManager") != null)
				{
					mInstance = GameObject.FindWithTag("SoundManager").GetComponent<SoundManagerScript>();
				}
				else 
				{
					GameObject obj = new GameObject("_SoundManager");
					mInstance = obj.AddComponent<SoundManagerScript>();
				}
				//!DontDestroyOnLoad(obj);
			}
			return mInstance;
		}
	}

	public static float bgmVolume = 1.0f;
	public static float sfxVolume = 1.0f;
	
	public List<AudioClipInfo> audioClipInfoList = new List<AudioClipInfo>();
	
	public AudioSource bgmAudioSource;
	public AudioSource sfxAudioSource;
	
	public List<AudioSource> sfxAudioSourceList = new List<AudioSource>();
	public List<AudioSource> bgmAudioSourceList = new List<AudioSource>();
	
	// Use this for initialization
	void Awake () 
	{
		AudioSource[] audioSourceList = this.GetComponentsInChildren<AudioSource>();
		
		if(audioSourceList[0].gameObject.name == "BGMAudioSource")
		{
			bgmAudioSource = audioSourceList[0];
			sfxAudioSource = audioSourceList[1];
		}
		else 
		{
			bgmAudioSource = audioSourceList[1];
			sfxAudioSource = audioSourceList[0];
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	AudioClip FindAudioClip(AudioClipID audioClipID)
	{
		for(int i=0; i<audioClipInfoList.Count; i++)
		{
			if(audioClipInfoList[i].audioClipID == audioClipID)
			{
				return audioClipInfoList[i].audioClip;
			}
		}

		Debug.LogError("Cannot Find Audio Clip : " + audioClipID);

		return null;
	}
	
	//! BACKGROUND MUSIC (BGM)
	public void PlayBGM(AudioClipID audioClipID)
	{
		bgmAudioSource.clip = FindAudioClip(audioClipID);
		Debug.Log (audioClipID);
		bgmAudioSource.volume = bgmVolume;
		bgmAudioSource.Play();
	}
	
	public void PauseBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Pause();
		}
	}
	
	public void StopBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Stop();
		}
	}

	public void PlayLoopingBGM(AudioClipID audioClipID)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);

		for(int i = 0; i < bgmAudioSourceList.Count; i++)
		{
			if(bgmAudioSourceList[i].clip == clipToPlay)
			{
				if(bgmAudioSourceList[i].isPlaying)
				{
					return;
				}

				bgmAudioSourceList[i].volume = bgmVolume;
				bgmAudioSourceList[i].Play();
				return;
			}
		}

		AudioSource newInstance = gameObject.AddComponent<AudioSource>();
		newInstance.clip = clipToPlay;
		newInstance.volume = bgmVolume;
		newInstance.loop = true;
		newInstance.Play();
		bgmAudioSourceList.Add(newInstance);
	}

	public void PauseLoopingBGM(AudioClipID audioClipID)
	{
		AudioClip clipToPause = FindAudioClip(audioClipID);

		for(int i=0; i<bgmAudioSourceList.Count; i++)
		{
			if(bgmAudioSourceList[i].clip == clipToPause)
			{
				bgmAudioSourceList[i].Pause();
				return;
			}
		}
	}


	public void StopLoopingBGM(AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		for(int i = 0; i < bgmAudioSourceList.Count; i++)
		{
			if(bgmAudioSourceList[i].clip == clipToStop)
			{
				bgmAudioSourceList[i].Stop();
				return;
			}
		}
	}

	
	//! SOUND EFFECTS (SFX)
	public void PlaySFX(AudioClipID audioClipID)
	{
		sfxAudioSource.PlayOneShot(FindAudioClip(audioClipID), sfxVolume);
	}
	
	public void PlayLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);
		
		for(int i = 0; i < sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToPlay)
			{
				if(sfxAudioSourceList[i].isPlaying)
				{
					return;
				}

				sfxAudioSourceList[i].volume = sfxVolume;
				sfxAudioSourceList[i].Play();
				return;
			}
		}
		
		AudioSource newInstance = gameObject.AddComponent<AudioSource>();
		newInstance.clip = clipToPlay;
		newInstance.volume = sfxVolume;
		newInstance.loop = true;
		newInstance.Play();
		sfxAudioSourceList.Add(newInstance);
	}
	
	public void PauseLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPause = FindAudioClip(audioClipID);
		
		for(int i = 0; i < sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToPause)
			{
				sfxAudioSourceList[i].Pause();
				return;
			}
		}
	}
	
	
	public void StopLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);
		
		for(int i = 0; i < sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToStop)
			{
				sfxAudioSourceList[i].Stop();
				return;
			}
		}
	}
	
	public static void SetBGMVolume(float value)
	{
		bgmVolume = value;
	}
	
	public static void SetSFXVolume(float value)
	{
		sfxVolume = value;
	}
}