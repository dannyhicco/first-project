﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float turningSpeed = 200f;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;

	private void Awake()
	{
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		float r = Input.GetAxisRaw ("Rotate");

		Move (h, v);
		Turning (r);
		Animating (h, v);
	}

	private void Move(float h, float v)
	{
		movement.Set (h, 0f, v);
		
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	private void Turning(float r) 
	{
		
		if (r > 0)
			transform.Rotate(Vector3.up * turningSpeed * Time.deltaTime);
		
		if (r < 0)
			transform.Rotate(Vector3.down * turningSpeed * Time.deltaTime);
		
	}

	private void Animating(float h, float v)
	{
		bool walking = (h != 0f || v != 0f);
		anim.SetBool ("IsWalking", walking);
	}
}
