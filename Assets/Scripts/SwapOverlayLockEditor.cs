using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SwapOverlayLockEditor : MonoBehaviour
{
	StageSectionManager stageSectionManager;

	void Awake()
	{
		stageSectionManager = transform.parent.parent.GetComponent<StageSectionManager>();
	}

	void Update()
	{
		if (stageSectionManager != null)
		{
			// Debug.Log(stageSectionManager.isLocked);
			if (stageSectionManager.isLocked)
			{
				GetComponent<Image>().enabled = true;
			}
			else
			{
				GetComponent<Image>().enabled = false;
			}
		}
	}
}
