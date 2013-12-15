using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour {

	public GameObject cube;

	// Update is called once per frame
	void Update () {
		transform.LookAt(cube.transform);
		
		transform.Translate(Vector3.right * Time.deltaTime);
	}
}
