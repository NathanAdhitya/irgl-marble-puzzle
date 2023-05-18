using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwapOverlayManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Color defaultColor;
	public Color hoverColor;
	public Color selectColor;
	private bool isHovered = false;

	private StageSectionManager stageSectionManager;
	Image image;


	void Start()
	{
		stageSectionManager = transform.parent.parent.GetComponent<StageSectionManager>();
		image = GetComponent<Image>();
		setColor(defaultColor);
	}

	void Update()
	{
		Color targetColor;

		if (stageSectionManager.isSelected)
		{
			targetColor = selectColor;
		}
		else if (isHovered)
		{
			targetColor = hoverColor;
		}
		else
		{
			targetColor = defaultColor;
		}

		if (image.color != targetColor)
		{
			Color lerpedColor = Color.Lerp(image.color, targetColor, 0.2f);
			setColor(lerpedColor);
		}

	}


	public void OnPointerEnter(PointerEventData eventData)
	{
		isHovered = true && stageSectionManager.isLocked == false;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHovered = false && stageSectionManager.isLocked == false;
	}

	private void setColor(Color color)
	{
		image.color = color;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		stageSectionManager.OnClick();
	}
}
