using System;
using UnityEngine;

public class ScoreData
{
	public int score;
	public string levelName;
	public TimeSpan time;
	public string teamName;

	public ScoreData(string teamName, string levelName, TimeSpan time, int score)
	{
		this.teamName = teamName;
		this.levelName = levelName;
		this.time = time;
		this.score = score;
	}

	public override string ToString()
	{
		return teamName.Replace(",", "\\,") + "," + levelName + "," + time.ToString("mm\\:ss\\.fff") + "," + score + "\n";
	}

	static public ScoreData FromString(string line)
	{
		String[] parts = line.Split(',');
		try
		{
			return new ScoreData(
				parts[0],
				 parts[1],
				  TimeSpan.ParseExact(
					parts[2],
					 new string[] { "mm\\:ss\\.fff" },
					 System.Globalization.CultureInfo.InvariantCulture
				),
				Int32.Parse(parts[3])
				);
		}
		catch (Exception e)
		{
			Debug.Log("Error parsing score data: " + e.Message);
			return null;
		}
	}
}