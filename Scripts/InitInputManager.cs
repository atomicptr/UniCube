using UnityEngine;
using System.Collections;
using InControl;

public class InitInputManager : MonoBehaviour {
	
	void Start () {
		Debug.Log("Init InputManager");

		InputManager.Setup();
	}

	void Update () {
		InputManager.Update();
	}
}
