using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlanesCube))]
public class PlanesCubeEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		PlanesCube script = (PlanesCube)target;

		if(GUILayout.Button("Setup planes")) {
			script.SetUpPlanes();
		}

		if(GUILayout.Button("Highlight")) {
			script.ToggleHighlight();
		}
	}
}
