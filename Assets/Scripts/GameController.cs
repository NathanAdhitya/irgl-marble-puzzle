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
	private DateTime startTime;
	private TimeSpan finalTime;
	public GameObject gameModalPrefab;
	public GameObject sideUIPrefab;
	private GameObject sideUI;
	private GameObject topUI;
	public GameObject topUIPrefab;
	private TextMeshProUGUI timerText;
	private GameObject gameModal;
	private GameObject deathModal;
	private GameObject finishModal;
	private GameObject confirmModal;
	private GameObject gameArena;
	private bool sectionSelectionPause;
	private bool isPaused;
	public String levelName = "Unnamed Level";

	void Awake()
	{
		startTime = new DateTime(System.DateTime.Now.Ticks);
		sectionSelectionPause = false;
		isPaused = false;

		// Create the modal
		gameModal = Instantiate(gameModalPrefab);

		// Bind Camera to modal
		gameModal.GetComponent<Canvas>().worldCamera = Camera.main;

		// Fill in all the other modals
		deathModal = gameModal.transform.Find("DiedPanel").gameObject;
		finishModal = gameModal.transform.Find("FinishPanel").gameObject;
		confirmModal = gameModal.transform.Find("ConfirmPanel").gameObject;

		// Bind all the modal buttons.
		confirmModal.transform.Find("ButtonContainer/No")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(CloseAllModals);
		deathModal.transform.Find("ButtonContainer/Try Again")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(RestartLevel);
		deathModal.transform.Find("ButtonContainer/Return")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(ReturnToMainMenu);
		finishModal.transform.Find("ButtonContainer/Return")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(ReturnToMainMenu);
		finishModal.transform.Find("ButtonContainer/Try Again")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(RestartLevel);
		CloseAllModals();

		// Get GameArena
		gameArena = GameObject.Find("GameArena");

		// Create Board Side UI
		sideUI = Instantiate(sideUIPrefab, gameArena.transform);

		// Correct camera and buttons
		sideUI.GetComponent<Canvas>().worldCamera = Camera.main;
		sideUI.transform.Find("ExitButton")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(OpenExitModal);
		sideUI.transform.Find("RestartButton")
			.GetComponent<UnityEngine.UI.Button>()
			.onClick.AddListener(OpenRetryModal);

		// Create Top UI
		topUI = Instantiate(topUIPrefab, gameArena.transform);

		// Set Level Name
		topUI.transform.Find("LevelName").GetComponent<TextMeshProUGUI>().text = levelName;

		// Set timer text
		timerText = topUI.transform.Find("TimerText").GetComponent<TextMeshProUGUI>();

		Camera.main.GetComponent<FocusController>().target = gameArena;
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
		SceneManager.LoadScene("Level Selector");
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
