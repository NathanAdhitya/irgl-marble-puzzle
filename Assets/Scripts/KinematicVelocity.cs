using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KinematicVelocity : MonoBehaviour
{
	private float _prevRotation;
	private Vector2 _prevPosition;

	private Rigidbody2D _rigidbody;

	public void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();

		_prevPosition = _rigidbody.position;
		_prevRotation = _rigidbody.rotation;
	}

	public void FixedUpdate()
	{
		var currentPosition = _rigidbody.position;
		var currentRotation = _rigidbody.rotation;

		_rigidbody.position = _prevPosition;
		_rigidbody.rotation = _prevRotation;
		_rigidbody.MovePosition(currentPosition);
		_rigidbody.MoveRotation(currentRotation);

		_prevPosition = currentPosition;
		_prevRotation = currentRotation;
	}
}