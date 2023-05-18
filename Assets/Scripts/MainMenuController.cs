using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	void Awake()
	{
		Time.timeScale = 1;
	}

	public void GoToLevelSelector()
	{
		SceneManager.LoadScene("Level Selector");
	}
}
