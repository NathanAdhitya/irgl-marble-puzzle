using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleableButton : MonoBehaviour
{
	public bool state = false;
	public ButtonBehaviour buttonBehaviour;
	private float depressedY = -0.1f;
	private Vector2 originalSize;
	public GameObject buttonSquare;


	void Start()
	{
		originalSize = buttonSquare.transform.localScale;
		buttonBehaviour = GetComponent<ButtonBehaviour>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			state = !state;
			if (state)
			{
				buttonSquare.transform.localScale = new Vector2(originalSize.x, originalSize.y + depressedY);
			}
			else
			{
				buttonSquare.transform.localScale = originalSize;
			}
			buttonBehaviour.OnButtonPress(state);
		}
	}
}
