// Copyright (C) 2014 Christopher Kaster
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour {

	private InputDevice device;

	private bool somethingSelected = false;

	private int x = 0;
	private int y = 0;
	private int z = 0;

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

	private CubeSides current = CubeSides.RED_SIDE;

	private PlayerInputDevice playerInputDevice;
	public PlayerInputDevice InputDevice {
		get { return this.playerInputDevice; }
	}

	private static PlayerController _instance;
	public static PlayerController Instance {
		get { return _instance; }
	}

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
		PlayerController._instance = this;

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
			this.rotateRow(false);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action3.WasPressed) {
			this.rotateRow(true);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action1.WasPressed) {
			this.rotateColumn(false);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.Action4.WasPressed) {
			this.rotateColumn(true);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.LeftBumper.WasPressed) {
			this.rotateLayer(true);
		} else if(somethingSelected && !CubeController.Instance.IsAnimationRunning && device.RightBumper.WasPressed) {
			this.rotateLayer(false);
		}

		if (device.LeftTrigger.WasPressed) {
			moveCameraToLeft();

			moveCursorLeft();
		} else if (device.RightTrigger.WasPressed) {
			moveCameraToRight();

			moveCursorRight();
		}
	}

	private void updateInputDevice() {
		this.device = InputManager.ActiveDevice;

		if (this.device.Name.ToLower ().Contains ("keyboard")) {
			this.playerInputDevice = PlayerInputDevice.KEYBOARD;
		} else if (this.device.Name.ToLower ().Contains ("xbox 360")) {
			this.playerInputDevice = PlayerInputDevice.XBOX360_GAMEPAD;
		} else if (this.device.Name.ToLower ().Contains ("ps3")) {
			this.playerInputDevice = PlayerInputDevice.PS3_GAMEPAD;
		} else if (this.device.Name.ToLower ().Contains ("ouya")) {
			this.playerInputDevice = PlayerInputDevice.OUYA_GAMEPAD;
		}
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
		current = current.right();

		Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, -90f);
	}

	private void moveCameraToLeft() {
		current = current.left();

		Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, 90f);
	}

	private void select(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;

		CubeController.Instance.deselectEverything();
		CubeController.Instance.selectCube(x, y, z);
	}

	private void rotateRow(bool left) {
		int id = y;

		if(current.isBaseSide()) {
			if(left) {
				CubeController.Instance.enqueueRotateRowToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateRowToRight(id);
			}
		} else {
			id = z;

			if(left) {
				CubeController.Instance.enqueueRotateLayerToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateLayerToRight(id);
			}
		}
	}

	private void rotateColumn(bool up) {
		int id = x;

		if (current == CubeSides.RED_SIDE || current == CubeSides.WHITE_SIDE || current == CubeSides.YELLOW_SIDE) {
			if(up) {
				CubeController.Instance.enqueueRotateColumnUp(id);
			} else {
				CubeController.Instance.enqueueRotateColumnDown(id);
			}
		} else if(current == CubeSides.BLUE_SIDE) {
			id = z;

			if(up) {
				CubeController.Instance.enqueueRotateLayerToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateLayerToRight(id);
			}
		} else if(current == CubeSides.ORANGE_SIDE) {
			if(!up) {
				CubeController.Instance.enqueueRotateColumnUp(id);
			} else {
				CubeController.Instance.enqueueRotateColumnDown(id);
			}
		} else if(current == CubeSides.GREEN_SIDE) {
			id = z;

			if(!up) {
				CubeController.Instance.enqueueRotateLayerToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateLayerToRight(id);
			}
		}
	}

	private void rotateLayer(bool left) {
		int id = z;

		if (current == CubeSides.RED_SIDE) {
			if(left) {
				CubeController.Instance.enqueueRotateLayerToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateLayerToRight(id);
			}
		} else if(current == CubeSides.BLUE_SIDE) {
			id = z == 2 ? 0 : 2;

			if(!left) {
				CubeController.Instance.enqueueRotateColumnUp(id);
			} else {
				CubeController.Instance.enqueueRotateColumnDown(id);
			}
		} else if(current == CubeSides.ORANGE_SIDE) {
			if(!left) {
				CubeController.Instance.enqueueRotateLayerToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateLayerToRight(id);
			}
		} else if(current == CubeSides.GREEN_SIDE) {
			id = z == 2 ? 0 : 2;

			if(left) {
				CubeController.Instance.enqueueRotateColumnUp(id);
			} else {
				CubeController.Instance.enqueueRotateColumnDown(id);
			}
		} else if(current == CubeSides.WHITE_SIDE || current == CubeSides.YELLOW_SIDE) {
			id = y;

			if(left) {
				CubeController.Instance.enqueueRotateRowToLeft(id);
			} else {
				CubeController.Instance.enqueueRotateRowToRight(id);
			}
		}
	}
}

class CubeSides {
	public static CubeSides RED_SIDE = new CubeSides(0);
	public static CubeSides BLUE_SIDE = new CubeSides(1);
	public static CubeSides ORANGE_SIDE = new CubeSides(2);
	public static CubeSides GREEN_SIDE = new CubeSides(3);

	// if white is in front, red should always be at bottom
	public static CubeSides WHITE_SIDE = new CubeSides(4);

	// if yellow is in front, red should always be at top
	public static CubeSides YELLOW_SIDE = new CubeSides(5);

	public int ID;

	private CubeSides(int id) {
		this.ID = id;
	}

	public CubeSides right() {
		if (this == RED_SIDE) {
			return BLUE_SIDE;
		} else if (this == BLUE_SIDE) {
			return ORANGE_SIDE;
		} else if (this == ORANGE_SIDE) {
			return GREEN_SIDE;
		} else if (this == GREEN_SIDE) {
			return RED_SIDE;
		} else {
			return null;
		}
	}

	public CubeSides left() {
		if (this == RED_SIDE) {
			return GREEN_SIDE;
		} else if (this == GREEN_SIDE) {
			return ORANGE_SIDE;
		} else if (this == ORANGE_SIDE) {
			return BLUE_SIDE;
		} else if (this == BLUE_SIDE) {
			return RED_SIDE;
		} else {
			return null;
		}
	}

	public bool isBaseSide() {
		return this == RED_SIDE || this == GREEN_SIDE || this == ORANGE_SIDE || this == BLUE_SIDE;
	}

	public static bool operator ==(CubeSides first, CubeSides second) {
		return first.ID == second.ID;
	}

	public static bool operator !=(CubeSides first, CubeSides second) {
		return first.ID != second.ID;
	}
}

public enum PlayerInputDevice {
	KEYBOARD,
	XBOX360_GAMEPAD,
	PS3_GAMEPAD,
	OUYA_GAMEPAD,
	UNKOWN
}
