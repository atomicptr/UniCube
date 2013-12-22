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
		if(controller.IsSolved()) {
			GUI.Label(new Rect(Screen.width/2 - 50, 20, 100, 25), "Cube is solved!");
		}

		GUI.Label(new Rect (Screen.width - 180, Screen.height - 50, 180, 25), "Early development prototype");
		GUI.Label(new Rect (Screen.width - 180, Screen.height - 70, 180, 25),
		          "Rotation Queue: " + controller.GetNumberOfQueuedRotations());

		// actions box
		GUI.Box(new Rect(10,10,200,100), "Actions.");

		// row controls
		if(row != -1 && GUI.Button(new Rect(20,40,180,20), "Row >>")) {
			controller.enqueueRotateRowToRight(row);
		}

		if(row != -1 && GUI.Button(new Rect(20,70,180,20), "Row <<")) {
			controller.enqueueRotateRowToLeft(row);
		}

		// column controls
		if(column != -1 && GUI.Button(new Rect(20,40,180,20), "Column ^")) {
			controller.enqueueRotateColumnUp(column);
		}

		if(column != -1 && GUI.Button(new Rect(20,70,180,20), "Column v")) {
			controller.enqueueRotateColumnDown(column);
		}

		// layer contorls
		if(layer != -1 && GUI.Button(new Rect(20,40,180,20), "Layer >>")) {
			controller.enqueueRotateLayerToRight(layer);
		}

		if(layer != -1 && GUI.Button(new Rect(20,70,180,20), "Layer <<")) {
			controller.enqueueRotateLayerToLeft(layer);
		}

		if((Application.platform == RuntimePlatform.WindowsEditor ||
		   Application.platform == RuntimePlatform.OSXEditor) &&
		   GUI.Button (new Rect (10, Screen.height - 60, 100, 25), "Print")) {
			controller.PrintCubeDataStructure();
		}

		if (GUI.Button (new Rect (10, Screen.height - 30, 100, 25), "Randomize")) {
			controller.Randomize(20);
		}

		// controls box
		GUI.Box(new Rect(Screen.width - 210, 10, 200, 100), "Controls.");

		if(GUI.Button(new Rect(Screen.width - 220, 40, 180, 20), "Select Row")) {
			if(row == -1) {
				column = -1;
				layer = -1;

				controller.deselectEverything();
				controller.selectRow(0);
				row = 0;
			} else if(row == 0) {
				controller.deselectEverything();
				controller.selectRow(2);

				row = 2;
			} else if(row == 2) {
				controller.deselectEverything();
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
