using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSectionManager : MonoBehaviour
{
	public GameObject stageContent;
	public GameObject swapOverlay;
	public Boolean isSelected;
	public StageManager stageManager;

	public bool isLocked = false;

	public void OnClick()
	{
		if (!isLocked)
			stageManager.OnSectionSelect(this);
	}

	void Start()
	{
		stageManager = transform.parent.GetComponent<StageManager>();
		isSelected = false;
	}
}
