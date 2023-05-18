using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMessage : MonoBehaviour
{

	public float TimeoutCounter;
	public CanvasGroup background;
	public TextMeshProUGUI text;

	public void ShowMessage(string message, float timeout)
	{
		text.text = message;
		TimeoutCounter = timeout;
	}

	void Start()
	{
		TimeoutCounter = 0;
		background.alpha = 0;
	}

	void Update()
	{
		if (TimeoutCounter > 0)
		{
			TimeoutCounter -= Time.deltaTime;
			if (TimeoutCounter <= 0)
			{
				TimeoutCounter = 0;
				background.alpha = 0;
			}
			else
			{
				background.alpha = 1;
			}
		}
	}
}
