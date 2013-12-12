using UnityEngine;
using System.Collections;

public class PlanesCube : MonoBehaviour {

	public Material top;
	public Material bottom;
	public Material left;
	public Material right;
	public Material front;
	public Material back;

	public void SetUpPlanes() {
		for(int i = 0; i < this.transform.childCount; i++) {
			Transform child = this.transform.GetChild(i);

			setPlane(child);
		}
	}

	private void setPlane(Transform t) {
		switch (t.name) {
			case "top":
				setTexture(t, top);
				break;
			case "bottom":
				setTexture(t, bottom);
				break;
			case "left":
				setTexture(t, left);
				break;
			case "right":
				setTexture(t, right);
				break;
			case "front":
				setTexture(t, front);
				break;
			case "back":
				setTexture(t, back);
				break;
			default:
				Debug.LogError("Unknown plane name: " + t.name);
				break;
		}
	}

	private void setTexture(Transform t, Material m) {
		t.renderer.material = m;
	}
}
