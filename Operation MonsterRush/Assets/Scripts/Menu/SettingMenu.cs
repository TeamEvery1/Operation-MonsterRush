using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingMenu : MonoBehaviour 
{
	Slider totalSlider, bgmSlider, sfxSlider;

	void Awake()
	{
		totalSlider = this.transform.Find ("Slider").gameObject.GetComponent <Slider>();
		bgmSlider = this.transform.Find ("Slider (1)").gameObject.GetComponent <Slider>();
		sfxSlider = this.transform.Find ("Slider (2)").gameObject.GetComponent <Slider>();
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
