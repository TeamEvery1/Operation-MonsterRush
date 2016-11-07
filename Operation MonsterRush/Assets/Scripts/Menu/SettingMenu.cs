using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingMenu : MonoBehaviour 
{
	Slider totalSlider, bgmSlider, sfxSlider;

	void Awake()
	{
		totalSlider = this.transform.Find ("Master Slider").gameObject.GetComponent <Slider>();
		bgmSlider = this.transform.Find ("BGM Slider").gameObject.GetComponent <Slider>();
		sfxSlider = this.transform.Find ("SFX Slider").gameObject.GetComponent <Slider>();
	}

	void Start()
	{
		totalSlider.value = AudioListener.volume;
		bgmSlider.value = SoundManagerScript.bgmVolume;
		sfxSlider.value = SoundManagerScript.sfxVolume;
	}
		
	void Update()
	{
		SoundManagerScript.SetBGMVolume (bgmSlider.value);
		SoundManagerScript.SetSFXVolume (sfxSlider.value);
		AudioListener.volume = totalSlider.value;
	}

	public void SlideSound()
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_SOUNDCHECK);
	}


}
