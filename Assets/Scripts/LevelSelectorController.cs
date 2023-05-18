using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorController : MonoBehaviour
{
	void Awake()
	{
		Time.timeScale = 1;
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}
}
