using UnityEngine;

public class FocusController : MonoBehaviour
{
	// The gameobject to fit inside the camera view
	public GameObject target;

	// The speed of zooming
	public float zoomSpeed = 5f;

	// The minimum and maximum orthographic size values
	public float minSize = 1f;
	public float maxSize = 10f;

	// The margin around the gameobject in pixels
	public float margin = 50f;

	// The camera component
	private Camera cam;

	// The collider 2D component of the target
	private Collider2D col;

	// The target distance and orthographic size
	private float targetDistance;
	private float targetSize;

	// Start is called before the first frame update
	void Start()
	{
		// Get the camera component
		cam = GetComponent<Camera>();

		// Get the collider 2D component of the target
		col = target.GetComponent<Collider2D>();

		// Calculate the initial distance and orthographic size
		CalculateDistanceAndSize();
	}

	// Update is called once per frame
	void Update()
	{
		// Lerp the camera's position and orthographic size to the target values
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -targetDistance), zoomSpeed * Time.deltaTime);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);

		// Run calculation every 0.2s
		if (Time.frameCount % 12 == 0)
		{
			CalculateDistanceAndSize();
		}
	}

	// Calculate the distance and orthographic size based on the bounds size and margin
	void CalculateDistanceAndSize()
	{
		// Get the bounds size of the collider 2D
		Vector3 size = col.bounds.size;

		// Convert the margin from pixels to world units
		float marginWorld = margin / cam.pixelHeight * cam.orthographicSize * 2f;

		// Add the margin to the size
		size += new Vector3(marginWorld, marginWorld, 0f);

		// Calculate the aspect ratio of the camera
		float aspect = cam.aspect;

		// Calculate the orthographic size based on the size and aspect ratio
		targetSize = Mathf.Max(size.x / aspect, size.y) / 2f;

		// Clamp the orthographic size to the min and max values
		targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

		// Calculate the distance based on the orthographic size and size
		targetDistance = targetSize * 2f / Mathf.Tan(cam.fieldOfView / 2f * Mathf.Deg2Rad);
	}
}