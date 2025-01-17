﻿using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;
	public Camera mainCamera;

	private void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		mainCamera = GameObject.Find ("Main Camera 2").GetComponent<Camera>();
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal2");
		float v = Input.GetAxisRaw ("Vertical2");

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	private void Move(float h, float v)
	{
		movement.Set (h, 0f, v);
		
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	private void Turning()
	{
		Ray camRay = mainCamera.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	private void Animating(float h, float v)
	{
		bool walking = (h != 0f || v != 0f);
		anim.SetBool ("IsWalking", walking);
	}
}
