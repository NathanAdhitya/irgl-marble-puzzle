using UnityEngine;

public class Rotatable2D : MonoBehaviour
{
	// The point around which the object will rotate
	public Transform pivot;

	// The speed of rotation
	public float speed = 50f;

	// Maximum rotation in degrees
	public float maxRotation = 20f;

	// The previous mouse position
	private Vector3 prevMousePos;

	// Update is called once per frame
	void Update()
	{
		// If the left mouse button is pressed
		if (Input.GetMouseButtonDown(0))
		{
			// Store the current mouse position
			prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		// If the left mouse button is held down
		if (Input.GetMouseButton(0))
		{
			// Get the current mouse position
			Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Get the direction vectors from the pivot to the mouse positions
			Vector3 prevDir = prevMousePos - pivot.position;
			Vector3 currDir = currMousePos - pivot.position;

			// Get the angle between the direction vectors
			float angle = Vector3.SignedAngle(prevDir, currDir, Vector3.forward);

			// Rotate the object around the pivot by the angle
			transform.RotateAround(pivot.position, Vector3.forward, angle * speed * Time.deltaTime);

			// Ensure the resulting angle is within the range
			float currRotation = transform.rotation.eulerAngles.z;
			if (currRotation > 180f)
			{
				currRotation -= 360f;
			}
			currRotation = Mathf.Clamp(currRotation, -maxRotation, maxRotation);
			transform.rotation = Quaternion.Euler(0f, 0f, currRotation);

			// Update the previous mouse position
			prevMousePos = currMousePos;
		}
	}
}