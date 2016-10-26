using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoyStickScripts : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image bgImg;
	private Image joystickImg;

	public bool canMove;
	private float timer = 0.0f;
	private float delayTimer = 10.0f;
	public Vector3 InputDirection{ set; get; }

	private void Start()
	{
		canMove = true;
		bgImg = GetComponent<Image>();
		joystickImg = transform.GetChild(0).GetComponent<Image>();
		InputDirection = Vector3.zero;
	}

	private void Update()
	{
		if(canMove ==false)
		{
			timer += Time.deltaTime;
			if(timer >= delayTimer)
			{
				canMove = true;
			}
		}
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		if(canMove == true)
		{
			Vector2 pos = Vector2.zero;
			if(RectTransformUtility.ScreenPointToLocalPointInRectangle
				(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
			{
				pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
				pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

				float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
				float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

				InputDirection = new Vector3(x, 0, y);
				InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

				joystickImg.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3)
					,InputDirection.z * (bgImg.rectTransform.sizeDelta.y/ 3));
			}
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		if(canMove == true)
		{
			OnDrag(ped);
		}
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		if(canMove == true)
		{
			InputDirection = Vector3.zero;
			joystickImg.rectTransform.anchoredPosition = Vector3.zero;
		}
	}
}
