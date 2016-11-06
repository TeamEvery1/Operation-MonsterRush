using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSwitchScript : MonoBehaviour {


	//public Sprite[] spriteList;
	//public Image thisImage;

	// Use this for initialization
	void Start () {
		//thisImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseExit()
	{
		//thisImage.sprite  = this.spriteList[0];
	}

	public void OnMouseEnter()
	{
		//thisImage.sprite  = this.spriteList[1];
	}
	//this.spriteList[1];
	public void OnMouseDown()
	{
		
		SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_BUTTONPRESSED1);
		//thisImage.sprite = this.spriteList[2];
	}
}
