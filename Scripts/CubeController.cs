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

	private GameObject tempParent;

	void Start() {
		rotateRowToRight(0);
	}
	
	public void rotateRowToRight(int y) {
		string[,] row = this.getRowIdentifier(y);

		Transform[,] cubeTransforms = new Transform[3, 3];

		string[,] tempRow = this.getRowIdentifier(y);

		// set temp parent to center object
		tempParent = this.findChild(row[1, 1]).gameObject;

		// get transforms
		for(int x = 0; x < 3; x++) {
			for(int z = 0; z < 3; z++) {
				cubeTransforms[x, z] = this.getCubeTransform(x, y, z);

				cubeTransforms[x, z].parent = tempParent.transform;
			}
		}

		// rotate sides
		row[0, 1] = tempRow[1, 0];
		row[1, 2] = tempRow[0, 1];
		row[2, 1] = tempRow[1, 2];
		row[1, 0] = tempRow[2, 1];
		
		// rotate diagonals
		row[0, 0] = tempRow[2, 0];
		row[0, 2] = tempRow[0, 0];
		row[2, 2] = tempRow[0, 2];
		row[2, 0] = tempRow[2, 2];

		// rotate real cubes

		// find center
		Vector3 center = this.findChild("top", tempParent.transform).renderer.bounds.center;

		tempParent.transform.RotateAround(center, Vector3.up, 90f);

		// put cubes back to good old object
		for(int x = 0; x < 3; x++) {
			for(int z = 0; z < 3; z++) {
				cubeTransforms[x, z].parent = this.transform;
			}
		}
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
		PlanesCube cube = getCube(x, y, z);
		
		cube.SetHighlighted(selected);
	}

	private Transform getCubeTransform(int x, int y, int z) {
		return this.findChild(cubes[x, y, z]);
	}

	private PlanesCube getCube(int x, int y, int z) {
		Transform cubeTransform = getCubeTransform(x, y, z);
		
		return (PlanesCube)cubeTransform.GetComponent("PlanesCube");
	}

	private string[,] getRowIdentifier(int y) {
		string[,] row = new string[3, 3];

		for(int x = 0; x < 3; x++) {
			for(int z = 0; z < 3; z++) {
				row[x, z] = cubes[x, y, z];
			}
		}

		return row;
	}

	private void setRowIdentifier(int y, string[,] row) {
		for(int x = 0; x < 3; x++) {
			for(int z = 0; z < 3; z++) {
				cubes[x, y, z] = row[x, z];
			}
		}
	}

	// TODO: use hashmap instead
	private Transform findChild(string name) {
		return this.findChild(name, this.transform);
	}

	private Transform findChild(string name, Transform from) {
		for(int i = 0; i < from.childCount; i++) {
			Transform child = from.GetChild(i);
			
			if(child.name == name) {
				return child;
			}
		}
		
		return null;
	}
}
