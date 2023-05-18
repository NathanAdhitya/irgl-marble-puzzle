using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
	public float bounce = 20f;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{

			Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

			// Remove all velocity
			rb.velocity = Vector2.zero;


			// Bounce according to the direction of the pad
			rb.AddForce(transform.up * bounce, ForceMode2D.Impulse);
		}
	}
}
