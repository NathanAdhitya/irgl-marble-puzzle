using System;
using TMPro;
using UnityEngine;

public class StaticData : MonoBehaviour
{
	public static String currentTeamName = "No Team";
	public static String scoringServer = "http://localhost:5000/";
	public static String scoringServerKey = "1234567890";

	// TMP Input
	public TMP_InputField teamNameInput;

	public void UpdateCurrentTeamName()
	{
		Debug.Log("Updating current team name to " + teamNameInput.text);
		currentTeamName = teamNameInput.text;
	}
}
