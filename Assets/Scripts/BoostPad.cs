using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
	public float bounce = 20f;
	public bool cancelVelocity = true;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{

			Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

			// Remove all velocity
			if (cancelVelocity)
				rb.velocity = Vector2.zero;


			// Bounce according to the direction of the pad, pick max between bounce and current force.
			//rb.AddForce(transform.up * bounce, ForceMode2D.Impulse);
			rb.velocity = transform.up * Mathf.Max(bounce, rb.velocity.magnitude);
		}
	}
}
