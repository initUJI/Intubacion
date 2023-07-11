// Copyright (c) 2018 @emiliana_vt


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDPPenMovement : MonoBehaviour {
	public Vector2 minPosition;
	public Vector2 maxPosition;
	public float stepSize = 0.1f;
	private bool lastJump = false;
	private DokoDemoPainterPen pen;

	void Start() {
		pen = GetComponent<DokoDemoPainterPen>();
	}

	void FixedUpdate() {
		Vector3 move = new Vector3();
		float axis;
		axis = Input.GetAxis("Horizontal");
		if (axis > 0.0f) {
			move.x += stepSize;
		}
		if (axis < 0.0f) {
			move.x -= stepSize;
		}
		axis = Input.GetAxis("Vertical");
		if (axis > 0.0f) {
			move.y += stepSize;
		}
		if (axis < 0.0f) {
			move.y -= stepSize;
		}
		Vector3 pos = move.normalized * stepSize + transform.localPosition;
		if (pos.x > maxPosition.x) {
			pos.x = maxPosition.x;
		}
		if (pos.x < minPosition.x) {
			pos.x = minPosition.x;
		}
		if (pos.y > maxPosition.y) {
			pos.y = maxPosition.y;
		}
		if (pos.y < minPosition.y) {
			pos.y = minPosition.y;
		}
		transform.localPosition = pos;
		if (Input.GetAxis("Jump") != 0) {
			if (!lastJump) {
				pen.penDown = !pen.penDown;
			}
			lastJump = true;
		} else {
			lastJump = false;
		}
	}
}
