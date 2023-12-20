using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool pointerDown;
	private float pointerDownTimer;

	[SerializeField]
	private float requiredHoldTime;
	public GameObject PlayerToFollow;

	public UnityEvent onLongClick;
	public UnityEvent onLongClickUp;
	

	//[SerializeField]
	//private Image fillImage;

	public void OnPointerDown(PointerEventData eventData)
	{

		pointerDown = true;
		Debug.Log("OnPointerDown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Reset();
		onLongClickUp.Invoke();
		Debug.Log("OnPointerUp");
	}

	private void Update()
	{
		//transform.position = PlayerToFollow.transform.position;
		if (pointerDown)
		{
			pointerDownTimer += Time.deltaTime;
			if (pointerDownTimer >= requiredHoldTime)
			{
				if (onLongClick != null)
					onLongClick.Invoke();

				Reset();
			}
			//fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
		}
	}

	private void Reset()
	{
		pointerDown = false;
		pointerDownTimer = 0;
		//fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
	}

}