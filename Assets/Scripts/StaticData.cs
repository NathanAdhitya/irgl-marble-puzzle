using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticData : MonoBehaviour
{
	public static String currentTeamName = "No Team";
	public static String scoringServer = "http://localhost:5000/";
	public static String scoringServerKey = "1234567890";
	public static List<ScoreData> scoreDatas = new List<ScoreData>();

	// TMP Input
	public TMP_InputField teamNameInput;

	public void UpdateCurrentTeamName()
	{
		Debug.Log("Updating current team name to " + teamNameInput.text);
		currentTeamName = teamNameInput.text;
	}

	public static void GetHighScores()
	{
		GetHighScores(currentTeamName);
	}

	public static void GetHighScores(string teamName)
	{
		List<ScoreData> highestScoreDatas = new List<ScoreData>();

		foreach (ScoreData scoreData in scoreDatas)
		{
			if (scoreData.teamName == teamName)
			{
				// Find same level name, only keep the highest score or add a new entry.
				ScoreData match = highestScoreDatas.Find(x => x.levelName == scoreData.levelName);

				if (match == null)
				{
					highestScoreDatas.Add(scoreData);
				}
				else
				{
					if (scoreData.time > match.time)
					{
						highestScoreDatas.Remove(match);
						highestScoreDatas.Add(scoreData);
					}
				}
			}
		}
	}

	public static int GetHighestScore(string teamName, string levelName)
	{
		int highestScore = 0;

		foreach (ScoreData scoreData in scoreDatas)
		{
			if (scoreData.teamName == teamName && scoreData.levelName == levelName)
			{
				if (scoreData.score > highestScore)
				{
					highestScore = scoreData.score;
				}
			}
		}

		return highestScore;
	}

	public static int GetHighestScore(string levelName)
	{
		return GetHighestScore(currentTeamName, levelName);
	}

	public static void LoadScoresForTeam()
	{
		LoadScoresForTeam(currentTeamName);
	}

	public static void LoadScoresForTeam(string teamName)
	{
		scoreDatas.Clear();
		String dataPath = UnityEngine.Application.persistentDataPath;
		System.IO.File.ReadAllLinesAsync(dataPath + "/times.csv").ContinueWith(task =>
		{
			foreach (String line in task.Result)
			{
				if (line != "")
				{
					ScoreData sd = ScoreData.FromString(line);
					if (sd != null)
						scoreDatas.Add(sd);
				}

			}
		});

		// String path = Application.persistentDataPath + "/times.csv";
		// String toAppend = teamName.Replace(",", "\\,") + "," + levelName + "," + timeSpan.ToString("mm\\:ss\\.fff") + "\n";
		// await System.IO.File.AppendAllTextAsync(path, toAppend);
		// Debug.Log("Written to file: " + toAppend);
	}
}
