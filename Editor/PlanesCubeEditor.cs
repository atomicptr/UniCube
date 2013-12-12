using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlanesCube))]
public class PlanesCubeEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		PlanesCube script = (PlanesCube)target;

		EditorGUILayout.LabelField("Name", script.Name);

		if(GUILayout.Button("Setup planes")) {
			script.SetUpPlanes();
		}
	}
}
