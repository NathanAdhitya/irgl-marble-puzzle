using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreFieldLoader : MonoBehaviour
{
	public string levelName;
	private TextMeshProUGUI text;

	void Awake()
	{
		// Get highest score for levelName, set it to the TMP Text
		text = GetComponent<TextMeshProUGUI>();

		// Get the highest score for levelName
		int highestScore = StaticData.GetHighestScore(levelName);

		// Set the text to the highest score
		text.text = highestScore.ToString();
	}
}
