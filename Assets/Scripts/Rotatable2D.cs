using UnityEngine;

public class Rotatable2D : MonoBehaviour
{
	// The point around which the object will rotate
	public Transform pivot;

	private Rigidbody2D rb;

	private GameObject player;

	// The speed of rotation
	public float speed = 50f;

	// Maximum rotation in degrees
	public float maxRotation = 20f;

	// The previous mouse position
	private Vector3 prevMousePos;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		// Calculate deltaTime independent of TimeScale
		float deltaTime = Time.deltaTime;

		if (Time.timeScale != 0)
			deltaTime /= Time.timeScale;

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
			transform.RotateAround(pivot.position, Vector3.forward, angle * speed * deltaTime);
			//rb.MoveRotation(rb.rotation + angle * speed * deltaTime);

			//rb.MoveRotation(rb.rotation + angle * speed * deltaTime);
			//float targetRotation = rb.rotation + angle * speed * deltaTime;
			//float targetRotation = transform.rotation.eulerAngles.z + angle * speed * deltaTime;

			// Ensure the resulting angle is within the range
			float currRotation = transform.rotation.eulerAngles.z;
			//float currRotation = targetRotation;
			if (currRotation > 180f)
			{
				currRotation -= 360f;
			}
			currRotation = Mathf.Clamp(currRotation, -maxRotation, maxRotation);
			transform.rotation = Quaternion.Euler(0f, 0f, currRotation);
			//rb.MoveRotation(currRotation);

			// Move the player's rigidbody position to the player's relative position in the arena after rotation
			// Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
			// Vector3 playerRelativePos = transform.InverseTransformPoint(player.transform.position);
			// playerRb.MovePosition(transform.TransformPoint(playerRelativePos));


			// Update the previous mouse position
			prevMousePos = currMousePos;
		}
	}
}