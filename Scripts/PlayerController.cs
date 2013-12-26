using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour {

	private InputDevice device;

	private bool somethingSelected = false;

	private int x = 0;
	private int y = 0;

	private int leftRightIndex = 0;
	private int upDownIndex = 0;

	// x, z
	private Vector2[] leftRightMovement = {
		new Vector2(0, 0),
		new Vector2(2, 0),
		new Vector2(2, 2),
		new Vector2(0, 2),
	};

	// y, z
	private Vector2[] upDownMovement = {
		new Vector2(0, 0),
		new Vector2(2, 0),
		new Vector2(2, 2),
		new Vector2(0, 2),
	};

	private bool leftWasPressed {
		get {
			return device.DPadLeft.WasPressed || (device.LeftStickX.WasPressed && device.LeftStickX.Value < 0f);
		}
	}
	
	private bool rightWasPressed {
		get {
			return device.DPadRight.WasPressed || (device.LeftStickX.WasPressed && device.LeftStickX.Value > 0f);
		}
	}
	
	private bool upWasPressed {
		get {
			return device.DPadUp.WasPressed || (device.LeftStickY.WasPressed && device.LeftStickY.Value > 0f);
		}
	}
	
	private bool downWasPressed {
		get {
			return device.DPadDown.WasPressed || (device.LeftStickY.WasPressed && device.LeftStickY.Value < 0f);
		}
	}

	void Start() {
		updateInputDevice();
	}

	void Update() {
		updateInputDevice();

		if(!somethingSelected && (rightWasPressed || leftWasPressed || upWasPressed || downWasPressed)) {
			somethingSelected = true;

			select(0, 0, 0);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && (leftWasPressed)) {
			moveCursorLeft();
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && (rightWasPressed)) {
			moveCursorRight();
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && (upWasPressed)) {
			moveCursorUp();
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && (downWasPressed)) {
			moveCursorDown();
		}

		if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action2.WasPressed) {
			CubeController.Instance.enqueueRotateRowToRight(y);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action3.WasPressed) {
			CubeController.Instance.enqueueRotateRowToLeft(y);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action1.WasPressed) {
			CubeController.Instance.enqueueRotateColumnDown(x);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action4.WasPressed) {
			CubeController.Instance.enqueueRotateColumnUp(x);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.LeftBumper.WasPressed) {
			CubeController.Instance.enqueueRotateLayerToLeft(0);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.RightBumper.WasPressed) {
			CubeController.Instance.enqueueRotateLayerToRight(0);
		}

		if (device.LeftTrigger.WasPressed) {
			moveCameraToLeft();
		} else if (device.RightTrigger.WasPressed) {
			moveCameraToRight();
		}
	}

	private void updateInputDevice() {
		this.device = InputManager.ActiveDevice;
	}

	private void moveCursorLeft() {
		if (leftRightIndex - 1 < 0) {
			leftRightIndex = leftRightMovement.Length - 1;
		} else {
			leftRightIndex--;
		}
		
		Vector2 left = leftRightMovement[leftRightIndex];
		
		select((int)left.x, y, (int)left.y);
	}

	private void moveCursorRight() {
		if (leftRightIndex + 1 >= leftRightMovement.Length) {
			leftRightIndex = 0;
		} else {
			leftRightIndex++;
		}

		Vector2 right = leftRightMovement[leftRightIndex];

		select((int)right.x, y, (int)right.y);
	}
	
	private void moveCursorUp() {
		if (upDownIndex - 1 < 0) {
			upDownIndex = upDownMovement.Length - 1;
		} else {
			upDownIndex--;
		}
		
		Vector2 up = upDownMovement[upDownIndex];
		
		select(x, (int)up.x, (int)up.y);
	}

	private void moveCursorDown() {
		if (upDownIndex + 1 >= upDownMovement.Length) {
			upDownIndex = 0;
		} else {
			upDownIndex++;
		}
		
		Vector2 down = upDownMovement[upDownIndex];
		
		select(x, (int)down.x, (int)down.y);
	}

	private void moveCameraToRight() {
		Camera.mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, -90f);
	}

	private void moveCameraToLeft() {
		Camera.mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, 90f);
	}

	private void select(int x, int y, int z) {
		this.x = x;
		this.y = y;

		CubeController.Instance.deselectEverything();
		CubeController.Instance.selectCube(x, y, z);
	}
}
