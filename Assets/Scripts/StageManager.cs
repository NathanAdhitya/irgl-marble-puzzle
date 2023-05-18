using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
	StageSectionManager[] selectedSections;
	public GameObject gameMessage;

	// Check whether marble collides multiple stages
	// If it is, then return false
	// If it isn't, then return true
	public Boolean IsBallInMultipleStages()
	{

		BoxCollider2D[] colliders = transform.Cast<Transform>()
			.Select(transform => transform.GetComponent<BoxCollider2D>())
			.Where(boxCollider => boxCollider != null)
			.ToArray();

		// Get marble collider tagged Player
		Collider2D marbleCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();

		int count = colliders.Count(
			collider =>
			collider.IsTouching(marbleCollider));

		Debug.Log("Count: " + count);

		if (count == 1)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	// Fragile code. TODO: fix
	public void OnSectionSelect(StageSectionManager section)
	{
		bool isBallInMultipleStages = IsBallInMultipleStages();
		if (isBallInMultipleStages)
		{
			gameMessage.GetComponent<GameMessage>().ShowMessage("Stages are locked. Ball is in multiple stages", 2);
			Debug.Log("Ball is in multiple stages");
			return;
		}

		// If it contains, then remove.
		if (selectedSections.Contains(section))
		{
			selectedSections = selectedSections.Where(val => val != section).ToArray();
			section.isSelected = false;
		}
		else
		{
			// If it doesn't contain, then add.
			selectedSections = selectedSections.Append(section).ToArray();
			section.isSelected = true;

			// If total selected sections is 2, then do stuff.
			if (selectedSections.Length == 2)
			{
				// Mark both as not selected
				selectedSections[0].isSelected = false;
				selectedSections[1].isSelected = false;

				// Swap parent for both StageContent in both StageSection
				GameObject parent1 = selectedSections[0].gameObject;
				GameObject parent2 = selectedSections[1].gameObject;
				GameObject marble = GameObject.FindGameObjectWithTag("Player");
				Collider2D marbleCollider = marble.GetComponent<CircleCollider2D>();

				// If ball is colliding in parent1, set parent to parent1 and then swap to parent 2 and vice versa.
				if (parent1.GetComponent<Collider2D>().IsTouching(marbleCollider))
				{
					marble.transform.SetParent(parent1.transform);
				}
				else if (parent2.GetComponent<Collider2D>().IsTouching(marbleCollider))
				{
					marble.transform.SetParent(parent2.transform);
				}

				// Swap children for parent1 and parent2
				Transform[] children1 = parent1.GetComponentsInChildren<Transform>();
				Transform[] children2 = parent2.GetComponentsInChildren<Transform>();

				foreach (Transform child in children1)
				{
					if (child.gameObject.name == "StageContent" || child.gameObject.CompareTag("Player"))
					{
						Vector3 currentLocalPosition = child.gameObject.transform.localPosition;
						child.SetParent(parent2.transform);
						child.gameObject.transform.localPosition = currentLocalPosition;
					}
				}

				foreach (Transform child in children2)
				{
					if (child.gameObject.name == "StageContent" || child.gameObject.CompareTag("Player"))
					{
						// current local position
						Vector3 currentLocalPosition = child.gameObject.transform.localPosition;
						child.SetParent(parent1.transform);
						child.gameObject.transform.localPosition = currentLocalPosition;
					}
				}

				selectedSections = Array.Empty<StageSectionManager>();
				// Put Marble back to root
				marble.transform.SetParent(gameObject.transform.parent);
			}
		}

		// Pause game if there are selected
		if (selectedSections.Length > 0)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	void Start()
	{
		selectedSections = Array.Empty<StageSectionManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
