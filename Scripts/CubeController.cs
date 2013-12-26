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

	private string[,,] solvedCube = new string[,,] {
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

	private bool[,,] cachedSelections = new bool[,,] {
		{
			{false, false, false},
			{false, false, false},
			{false, false, false}
		},
		{
			{false, false, false},
			{false, false, false},
			{false, false, false}
		},
		{
			{false, false, false},
			{false, false, false},
			{false, false, false}
		}
	};

	private GameObject tempParent;
	private Queue rotationQueue;

	private const float animationSpeed = 0.8f;
	private const float randomizeSpeedFactor = 5f;

	private RotationQueueItem currentItem;
	private bool animationRunning = false;
	private bool randomizing = false;

	private bool playedWinSound = true;

	private static CubeController _instance;
	public static CubeController Instance {
		get {
			return _instance;
		}
	}

	public bool IsAnimationRunning {
		get {
			return animationRunning;
		}
	}

	void Start() {
		CubeController._instance = this;
		rotationQueue = new Queue();

		Randomize(20);
	}

	void FixedUpdate() {
		if(!animationRunning && rotationQueue.Count > 0) {
			animationRunning = true;

			// something changed, win sound can be played again
			playedWinSound = false;

			currentItem = (RotationQueueItem)rotationQueue.Dequeue();

			doRotation(currentItem);
		}

		if (!playedWinSound && this.IsSolved() && rotationQueue.Count == 0) {
			playedWinSound = true;

			SoundManager.PlayWinSound();
		}
	}

	private void doRotation(RotationQueueItem item) {
		switch(item.rotation) {
			case Rotation.ROW_LEFT:
				this.rotateRowToLeft(item.axisNum);
				break;
			case Rotation.ROW_RIGHT:
				this.rotateRowToRight(item.axisNum);
				break;
			case Rotation.COLUMN_UP:
				this.rotateColumnUp(item.axisNum);
				break;
			case Rotation.COLUMN_DOWN:
				this.rotateColumnDown(item.axisNum);
				break;
			case Rotation.LAYER_LEFT:
				this.rotateLayerToLeft(item.axisNum);
				break;
			case Rotation.LAYER_RIGHT:
				this.rotateLayerToRight(item.axisNum);
				break;
		}
	}

	public void PrintCubeDataStructure() {
		for (int x = 0; x < 3; x++) {
			print("{");

			for(int y = 0; y < 3; y++) {
				print("(");

				for(int z = 0; z < 3; z++) {
					print("Position: x: " + x + "y: " + y + "z: " + z + ", " + cubes[x, y, z]);
				}

				print(")");
			}

			print("}");
		}
	}

	public int GetNumberOfQueuedRotations() {
		return rotationQueue.Count;
	}

	public bool IsSolved() {
		for(int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				for(int z = 0; z < 3; z++) {
					if(cubes[x, y, z] != solvedCube[x, y, z]) {
						return false;
					}
				}
			}
		}
		
		return true;
	}

	public void Randomize(int n) {
		this.randomizing = true;
		this.deselectEverything();

		for(int i = 0; i < n; i++) {
			this.enqueueRandomAction();
		}
	}

	private void enqueueRandomAction() {
		int rnd = Random.Range(0, 6);

		int element = Random.Range(0, 100) > 50 ? 0 : 2;

		RotationQueueItem item = new RotationQueueItem();

		item.axisNum = element;

		switch(rnd) {
			case 0:
				item.rotation = Rotation.ROW_RIGHT;
				break;
			case 1:
				item.rotation = Rotation.COLUMN_UP;
				break;
			case 2:
				item.rotation = Rotation.LAYER_LEFT;
				break;
			case 3:
				item.rotation = Rotation.ROW_LEFT;
				break;
			case 4:
				item.rotation = Rotation.COLUMN_DOWN;
				break;
			case 5:
				item.rotation = Rotation.LAYER_RIGHT;
				break;
		}

		rotationQueue.Enqueue(item);
	}

	public void enqueueRotateRowToLeft(int y) {
		enqueueItem(y, Rotation.ROW_LEFT);
	}

	public void enqueueRotateRowToRight(int y) {
		enqueueItem(y, Rotation.ROW_RIGHT);
	}

	public void enqueueRotateColumnUp(int x) {
		enqueueItem(x, Rotation.COLUMN_UP);
	}

	public void enqueueRotateColumnDown(int x) {
		enqueueItem(x, Rotation.COLUMN_DOWN);
	}

	public void enqueueRotateLayerToLeft(int z) {
		enqueueItem(z, Rotation.LAYER_LEFT);
	}

	public void enqueueRotateLayerToRight(int z) {
		enqueueItem(z, Rotation.LAYER_RIGHT);
	}

	private void enqueueItem(int axisNum, Rotation rotation) {
		this.backupSelections();

		RotationQueueItem item = new RotationQueueItem();
		
		item.axisNum = axisNum;
		item.rotation = rotation;
		
		rotationQueue.Enqueue(item);
	}

	private void backupSelections() {
		for(int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				for(int z = 0; z < 3; z++) {
					cachedSelections[x, y, z] = getCube(x, y, z).IsSelected();
				}
			}
		}

		this.deselectEverything();
	}

	private void restoreSelections() {
		this.deselectEverything();

		for(int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				for(int z = 0; z < 3; z++) {
					this.setCubeAsSelected(x, y, z, cachedSelections[x, y, z]);
				}
			}
		}
	}

	private void rotateRowToLeft(int y) {
		this.rotate(this.getRowIdentifier(y), "y", y, true);
	}

	private void rotateRowToRight(int y) {
		this.rotate(this.getRowIdentifier(y), "y", y, false);
	}

	private void rotateColumnUp(int x) {
		this.rotate(this.getColumnIdentifier(x), "x", x, true);
	}

	private void rotateColumnDown(int x) {
		this.rotate(this.getColumnIdentifier(x), "x", x, false);
	}

	private void rotateLayerToLeft(int z) {
		this.rotate(this.getLayerIdentifier(z), "z", z, true);
	}

	private void rotateLayerToRight(int z) {
		this.rotate(this.getLayerIdentifier(z), "z", z, false);
	}

	private void rotate(string[,] rowIdentifier, string axis, int axisValue, bool rotatePositive) {
		// TODO enable rotation with center axis
		if(axisValue == 1) {
			return;
		}

		// set animation running
		this.animationRunning = true;
		
		string[,] row = (string[,])rowIdentifier.Clone();
		
		Transform[,] cubeTransforms = new Transform[3, 3];
		
		string[,] tempRow = (string[,])rowIdentifier.Clone ();
		
		// set temp parent to center object
		tempParent = this.findChild(row[1, 1]).gameObject;
		
		// get transforms
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				int x = 0;
				int y = 0;
				int z = 0;
				
				if(axis == "x") {
					x = axisValue;
					y = i;
					z = j;
					
					cubeTransforms[y, z] = this.getCubeTransform(x, y, z);
					
					cubeTransforms[y, z].parent = tempParent.transform;
				} else if(axis == "y") {
					x = i;
					y = axisValue;
					z = j;
					
					cubeTransforms[x, z] = this.getCubeTransform(x, y, z);
					
					cubeTransforms[x, z].parent = tempParent.transform;
				} else if(axis == "z") {
					x = i;
					y = j;
					z = axisValue;
					
					cubeTransforms[x, y] = this.getCubeTransform(x, y, z);
					
					cubeTransforms[x, y].parent = tempParent.transform;
				}
			}
		}
		
		// rotate datastructure
		this.performDataStructureRotation(row, tempRow, rotatePositive);
		
		// rotate real cubes
		string face = "???";
		Vector3 rotationVector = Vector3.zero;

		float rotationAngle = rotatePositive ? -90f : 90f;;

		if (axis == "x") {
			face = "left";
			
			rotationVector = Vector3.left;

			rotationAngle = rotatePositive ? -90f : 90f;
		} else if (axis == "y") {
			face = "top";
			
			rotationVector = Vector3.up;

			rotationAngle = rotatePositive ? 90f : -90f;
		} else if (axis == "z") {
			face = "front";
			
			rotationVector = Vector3.back;

			rotationAngle = rotatePositive ? -90f : 90f;
		}
		
		// find center
		Vector3 center = this.findChild(face, tempParent.transform).renderer.bounds.center;
		
		//tempParent.transform.RotateAround(center, rotationVector, rotationAngle);
		this.animatedRotation(tempParent.transform, center, rotationVector, rotationAngle, cubeTransforms);
		
		// apply changes to data structure
		if (axis == "x") {
			this.setColumnIdentifier(axisValue, row);
		} else if (axis == "y") {
			this.setRowIdentifier(axisValue, row);
		} else if (axis == "z") {
			this.setLayerIdentifier(axisValue, row);
		}
	}

	private void performDataStructureRotation(string[,] row, string[,] tempRow, bool positiveRotation) {
		if (positiveRotation) {
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
		} else {
			// rotate sides
			row[0, 1] = tempRow[1, 2];
			row[1, 2] = tempRow[2, 1];
			row[2, 1] = tempRow[1, 0];
			row[1, 0] = tempRow[0, 1];
			
			// rotate diagonals
			row[0, 0] = tempRow[0, 2];
			row[0, 2] = tempRow[2, 2];
			row[2, 2] = tempRow[2, 0];
			row[2, 0] = tempRow[0, 0];
		}
	}

	private void animatedRotation(Transform transform, Vector3 center, Vector3 rotationVector, float rotationAngle, Transform[,] cubeTransforms) {
		SoundManager.PlayRotateSound();

		StartCoroutine (RotationCoroutine(transform, center, rotationVector, rotationAngle, cubeTransforms));
	}
	
	private IEnumerator RotationCoroutine(Transform transform, Vector3 point, Vector3 axis, float rotateAmount, Transform[,] cubeTransforms) {
		float rotateTime = randomizing ? animationSpeed / randomizeSpeedFactor : animationSpeed;
		
		float step = 0f;
		float rate = 1f / rotateTime;
		float smoothStep = 0f;
		float lastStep = 0f;
		
		while (step < 1f) {
			step += Time.deltaTime * rate;
			smoothStep = Mathf.SmoothStep(0f, 1f, step);
			
			transform.RotateAround(point, axis, rotateAmount * (smoothStep - lastStep));
			lastStep = smoothStep;
			
			yield return null;
		}
		
		if (step > 1f) {
			transform.RotateAround(point, axis, rotateAmount * (1f - lastStep));
		}
		
		// put cubes back to good old object
		this.setCubeTransformParent(cubeTransforms, this.transform);
		
		animationRunning = false;

		// if there are no more rotations enqueued, restore selections
		if(rotationQueue.Count == 0) {
			this.restoreSelections();
		}

		// done randomizing
		if(randomizing && rotationQueue.Count == 0) {
			randomizing = false;
		}
	}

	private void setCubeTransformParent(Transform[,] transforms, Transform parent) {
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				transforms[i, j].parent = parent;
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

	public void deselectEverything() {
		for (int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				for(int z = 0; z < 3; z++) {
					deselectCube(x, y, z);
				}
			}
		}
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

	public void selectCube(int x, int y, int z) {
		setCubeAsSelected(x, y, z, true);
	}

	private void deselectCube(int x, int y, int z) {
		setCubeAsSelected(x, y, z, false);
	}

	private void setCubeAsSelected(int x, int y, int z, bool selected) {
		if (animationRunning) {
			return;
		}

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

	private string[,] getColumnIdentifier(int x) {
		string[,] row = new string[3, 3];
		
		for(int y = 0; y < 3; y++) {
			for(int z = 0; z < 3; z++) {
				row[y, z] = cubes[x, y, z];
			}
		}
		
		return row;
	}
	
	private void setColumnIdentifier(int x, string[,] row) {
		for(int y = 0; y < 3; y++) {
			for(int z = 0; z < 3; z++) {
				cubes[x, y, z] = row[y, z];
			}
		}
	}

	private string[,] getLayerIdentifier(int z) {
		string[,] row = new string[3, 3];
		
		for(int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				row[x, y] = cubes[x, y, z];
			}
		}
		
		return row;
	}
	
	private void setLayerIdentifier(int z, string[,] row) {
		for(int x = 0; x < 3; x++) {
			for(int y = 0; y < 3; y++) {
				cubes[x, y, z] = row[x, y];
			}
		}
	}

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

enum Rotation {
	ROW_LEFT,
	ROW_RIGHT,
	COLUMN_UP,
	COLUMN_DOWN,
	LAYER_LEFT,
	LAYER_RIGHT
}

struct RotationQueueItem {
	public int axisNum;
	public Rotation rotation;
}

