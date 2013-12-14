using UnityEngine;
using System.Collections;

public class PlanesCube : MonoBehaviour {

	public Material top;
	public Material bottom;
	public Material left;
	public Material right;
	public Material front;
	public Material back;

	private bool isHighlighted = false;

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

	public void SetHighlighted(bool highlighted) {
		if(highlighted) {
			Shader shader = Shader.Find("Self-Illumin/Parallax Diffuse");

			applyShader(shader);
		} else {
			Shader shader = Shader.Find("Diffuse");

			applyShader(shader);
		}

		this.isHighlighted = highlighted;
	}

	public void ToggleHighlight() {
		isHighlighted = !isHighlighted;

		this.SetHighlighted(isHighlighted);
	}

	private void applyShader(Shader shader) {
		for(int i = 0; i < this.transform.childCount; i++) {
			Transform child = this.transform.GetChild(i);

			child.renderer.material.shader = shader;
		}
	}
}
