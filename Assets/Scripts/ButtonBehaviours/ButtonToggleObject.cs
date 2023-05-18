using UnityEngine;

public class ButtonToggleObject : MonoBehaviour, ButtonBehaviour
{
	public GameObject objectToToggle;
	public bool reverse = false;

	public void OnButtonPress(bool state)
	{
		objectToToggle.SetActive(state ^ reverse);
	}
}
