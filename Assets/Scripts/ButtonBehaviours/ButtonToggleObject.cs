using UnityEngine;

public class ButtonToggleObject : MonoBehaviour, ButtonBehaviour
{
	public GameObject objectToToggle;

	public void OnButtonPress(bool state)
	{
		objectToToggle.SetActive(state);
	}
}
