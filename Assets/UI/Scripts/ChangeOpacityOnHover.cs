using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeOpacityOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public float opacity = 0.5f;
	public float hoverOpacity = 1f;
	private float currentOpacity = 0;
	

	// Sets the opacity of the Image component while respecting the current color of the image.
	void setOpacity(float opacity)
	{
		currentOpacity = opacity;
	}

	// Sets the opacity of the Image component to the hover opacity.
	public void OnPointerEnter(PointerEventData eventData)
	{
		setOpacity(hoverOpacity);
	}
	
	// Sets the opacity of the Image component to the default opacity.
	public void OnPointerExit(PointerEventData eventData)
	{
		setOpacity(opacity);
	}

	void Start()
	{
		// Set the initial opacity of the Image component while respecting the current color
		// of the image.
		setOpacity(opacity);
	}

	void Update()
	{
		// Animate opacity change on hover in relation to deltaTime
		// to make the transition smooth.
		Image image = GetComponent<Image>();
		Color color = image.color;
		color.a = Mathf.Lerp(color.a, currentOpacity, Time.deltaTime * 50);
		image.color = color;
		
	}
}
