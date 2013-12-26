using UnityEngine;
using System.Collections;
using InControl;

public class CameraRotator : MonoBehaviour {

	private float speed = 10f;

	private GameObject pivot;

	void Start() {
		pivot = new GameObject("camera pivot");
	}

	void LateUpdate () {
		Transform target = pivot.transform;

		float mouseDeltaX = 0f;
		float mouseDeltaY = 0f;

		mouseDeltaX = InputManager.ActiveDevice.RightStickX.Value * speed;
		mouseDeltaY = InputManager.ActiveDevice.RightStickY.Value * speed;

		Quaternion rotation = Quaternion.Euler(mouseDeltaY, mouseDeltaX, 0);
		transform.rotation = rotation;
		
		//Move the camera
		Vector3 position = rotation * this.transform.position + target.position;
		transform.position = position;

		// look at target
		transform.LookAt (target);
	}
}
