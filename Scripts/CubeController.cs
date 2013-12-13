using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	private string[,,] cubes = new string[,,] {
		{
			{"cube_0x0x0", "cube_0x0x1", "cube_0x0x2"},
			{"cube_0x1x0", "cube_0x1x1", "cube_0x1x2"},
			{"cube_0x2x0", "cube_0x2x1", "cube_0x2x2"}
		},
		{
			{"cube_1x0x0", "cube_1x0x1", "cube_1x0x2"},
			{"cube_1x1x0", "cube_1x1x1", "cube_1x1x2"},
			{"cube_1x2x0", "cube_1x2x1", "cube_1x2x2"}
		},
		{
			{"cube_2x0x0", "cube_2x0x1", "cube_2x0x2"},
			{"cube_2x1x0", "cube_2x1x1", "cube_2x1x2"},
			{"cube_2x2x0", "cube_2x2x1", "cube_2x2x2"}
		}
	};

	void Start () {
		selectLayer(1);
	}

	public void selectRow(int y) {
		setRowAsSelected(y, true);
	}

	public void selectColumn(int x) {
		setColumnAsSelected(x, true);
	}

	public void selectLayer(int z) {
		setLayerAsSelected(z, true);
	}

	public void deselectRow(int y) {
		setRowAsSelected(y, false);
	}

	public void deselectColumn(int x) {
		setColumnAsSelected(x, false);
	}

	public void deselectLayer(int z) {
		setLayerAsSelected(z, false);
	}

	private void setRowAsSelected(int y, bool selected) {
		for(int x = 0; x < 3; x++) {
			for(int z = 0; z < 3; z++) {
				setCubeAsSelected(x, y, z, selected);
			}
		}
	}

	private void setColumnAsSelected(int x, bool selected) {
		for(int y = 0; y < 3; y++) {
			for(int z = 0; z < 3; z++) {
				setCubeAsSelected(x, y, z, selected);
			}
		}
	}

	private void setLayerAsSelected(int z, bool selected) {
		for(int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				setCubeAsSelected(x, y, z, selected);
			}
		}
	}

	private void selectCube(int x, int y, int z) {
		setCubeAsSelected(x, y, z, true);
	}

	private void deselectCube(int x, int y, int z) {
		setCubeAsSelected(x, y, z, false);
	}

	private void setCubeAsSelected(int x, int y, int z, bool selected) {
		Transform cubeTransform = this.findChild(cubes[x, y, z]);
		
		PlanesCube cube = (PlanesCube)cubeTransform.GetComponent("PlanesCube");
		
		cube.SetHighlighted(selected);
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
