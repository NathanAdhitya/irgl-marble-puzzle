using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectorController : MonoBehaviour
{
	public TextMeshProUGUI text;

	void Awake()
	{
		// set team name
		text.text = StaticData.currentTeamName;
		Time.timeScale = 1;
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void ReturnToLevelSelection()
	{
		SceneManager.LoadScene("Level Selector");
	}

	public void GoToHighScores()
	{
		SceneManager.LoadScene("High Scores");
	}

	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}
}
