using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public DateTime startTime;
	public TimeSpan finalTime;
	public TextMeshProUGUI timerText;
	public GameObject gameModal;
	public GameObject deathModal;
	public GameObject finishModal;
	public GameObject confirmModal;
	private bool sectionSelectionPause;
	private bool isPaused;

	void Start()
	{
		startTime = new DateTime(System.DateTime.Now.Ticks);
		sectionSelectionPause = false;
		isPaused = false;
	}

	void Update()
	{
		UpdateTimerGUI();
	}

	private void UpdateTimerGUI()
	{
		if (!isPaused)
			timerText.text = (new DateTime(System.DateTime.Now.Ticks) - startTime).ToString("mm\\:ss\\.fff");
	}

	private void ProcessPause()
	{
		if (sectionSelectionPause || isPaused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void OnDie()
	{
		isPaused = true;
		ProcessPause();
		OpenDeathModal();
	}

	public void OnWin()
	{
		isPaused = true;
		ProcessPause();
		finalTime = new DateTime(System.DateTime.Now.Ticks) - startTime;

		// Store final time and team name in PlayerPrefs
		AppendTimeToLocalFileAsync(StaticData.currentTeamName, SceneManager.GetActiveScene().name, finalTime);

		// Show finish modal
		finishModal.SetActive(true);
		gameModal.SetActive(true);

		// Show time in finish modal
		finishModal.transform.Find("Content").GetComponent<TextMeshProUGUI>().text = "Your time: " + finalTime.ToString("mm\\:ss\\.fff");
	}

	async private void AppendTimeToLocalFileAsync(String teamName, String levelName, TimeSpan timeSpan)
	{
		String path = Application.persistentDataPath + "/times.csv";
		String toAppend = teamName.Replace(",", "\\,") + "," + levelName + "," + timeSpan.ToString("mm\\:ss\\.fff") + "\n";
		await System.IO.File.AppendAllTextAsync(path, toAppend);
		Debug.Log("Written to file: " + toAppend);
	}

	public void OpenExitModal()
	{
		confirmModal.SetActive(true);
		gameModal.SetActive(true);

		confirmModal.transform.Find("Content").GetComponent<TextMeshProUGUI>().text = "Are you sure you want to return to main menu?";
		confirmModal.transform.Find("ButtonContainer/Yes").GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
		confirmModal.transform.Find("ButtonContainer/Yes").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ReturnToMainMenu);
	}

	public void OpenRetryModal()
	{
		confirmModal.SetActive(true);
		gameModal.SetActive(true);

		confirmModal.transform.Find("Content").GetComponent<TextMeshProUGUI>().text = "Are you sure you want to restart the level?";
		confirmModal.transform.Find("ButtonContainer/Yes").GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
		confirmModal.transform.Find("ButtonContainer/Yes").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(RestartLevel);
	}

	private void OpenDeathModal()
	{
		deathModal.SetActive(true);
		gameModal.SetActive(true);
	}

	public void OnSectionSelectionChange(bool state)
	{
		sectionSelectionPause = state;
		ProcessPause();
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void CloseAllModals()
	{
		gameModal.SetActive(false);
		deathModal.SetActive(false);
		finishModal.SetActive(false);
		confirmModal.SetActive(false);
	}

}
