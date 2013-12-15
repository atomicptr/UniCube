using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {

	public GameObject cube;

	private CubeController controller;

	private int row = -1;
	private int column = -1;
	private int layer = -1;

	void Start() {
		controller = cube.GetComponent<CubeController>();
	}

	void OnGUI() {
		// actions box
		GUI.Box(new Rect(10,10,200,100), "Actions.");

		// row controls
		if(row != -1 && GUI.Button(new Rect(20,40,180,20), "Row >>")) {
			controller.rotateRowToRight(row);
		}

		if(row != -1 && GUI.Button(new Rect(20,70,180,20), "Row <<")) {
			controller.rotateRowToLeft(row);
		}

		// column controls
		if(column != -1 && GUI.Button(new Rect(20,40,180,20), "Column ^^")) {
			controller.rotateColumnUp(column);
		}

		if(column != -1 && GUI.Button(new Rect(20,70,180,20), "Column vv")) {
			controller.rotateColumnDown(column);
		}

		// layer contorls
		if(layer != -1 && GUI.Button(new Rect(20,40,180,20), "Layer >>")) {
			controller.rotateLayerToRight(layer);
		}

		if(layer != -1 && GUI.Button(new Rect(20,70,180,20), "Layer <<")) {
			controller.rotateLayerToLeft(layer);
		}

		// controls box
		GUI.Box(new Rect(Screen.width - 210, 10, 200, 100), "Controls.");

		if(GUI.Button(new Rect(Screen.width - 220, 40, 180, 20), "Select Row")) {
			if(row == -1) {
				column = -1;
				layer = -1;

				controller.deselectColumn(0);
				controller.deselectLayer(0);
				controller.deselectRow(0);

				controller.deselectColumn(2);
				controller.deselectLayer(2);
				controller.deselectRow(2);

				controller.selectRow(0);
				row = 0;
			} else if(row == 0) {
				controller.deselectRow(0);
				controller.selectRow(2);

				row = 2;
			} else if(row == 2) {
				controller.deselectRow(2);
				controller.selectRow(0);

				row = 0;
			}
		}

		if(GUI.Button(new Rect(Screen.width - 220, 65, 180, 20), "Select Column")) {
			if(column == -1) {
				row = -1;
				layer = -1;
				
				controller.deselectColumn(0);
				controller.deselectLayer(0);
				controller.deselectRow(0);
				
				controller.deselectColumn(2);
				controller.deselectLayer(2);
				controller.deselectRow(2);
				
				controller.selectColumn(0);
				column = 0;
			} else if(column == 0) {
				controller.deselectColumn(0);
				controller.selectColumn(2);
				
				column = 2;
			} else if(column == 2) {
				controller.deselectColumn(2);
				controller.selectColumn(0);
				
				column = 0;
			}
		}

		if(GUI.Button(new Rect(Screen.width - 220, 90, 180, 20), "Select Layer")) {
			if(layer == -1) {
				row = -1;
				column = -1;
				
				controller.deselectColumn(0);
				controller.deselectLayer(0);
				controller.deselectRow(0);
				
				controller.deselectColumn(2);
				controller.deselectLayer(2);
				controller.deselectRow(2);
				
				controller.selectLayer(0);
				layer = 0;
			} else if(layer == 0) {
				controller.deselectLayer(0);
				controller.selectLayer(2);
				
				layer = 2;
			} else if(layer == 2) {
				controller.deselectLayer(2);
				controller.selectLayer(0);
				
				layer = 0;
			}
		}
	}
}
