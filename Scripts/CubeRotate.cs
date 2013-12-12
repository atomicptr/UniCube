using UnityEngine;
using System.Collections;

public class CubeRotate : MonoBehaviour {

	public int rotationSpeed = 40;

	void Start () {
		this.transform.eulerAngles = new Vector3(-45, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
	}
}
