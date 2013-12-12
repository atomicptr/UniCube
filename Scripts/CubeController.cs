using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	private int selectX = 0;
	private int selectY = 0;
	private int selectZ = 0;

	void Start () {
		Transform firstCube = this.findChild("cube_0x0x0");

		PlanesCube cube = (PlanesCube)firstCube.GetComponent("PlanesCube");

		cube.SetHighlighted(true);
	}

	void Update () {
		if(Input.GetKeyUp("d")) {
			Transform t = null;

			if(selectX + 1 > 2) {
				if(selectZ + 1 > 2) {
					selectX = 0;
				} else {
					t = findChild("cube_2x0x" + ++selectZ);
				}
			} else {
				t = findChild("cube_" + ++selectX + "x0x0");
			}

			PlanesCube.SelectedCube.SetHighlighted(false);

			PlanesCube cube = (PlanesCube)t.GetComponent("PlanesCube");

			cube.SetHighlighted(true);
		}
	}

	private Transform findChild(string name) {
		for(int i = 0; i < this.transform.childCount; i++) {
			Transform child = this.transform.GetChild(i);

			if(child.name == name) {
				return child;
			}
		}

		return null;
	}
}
