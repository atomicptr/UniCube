// Copyright (C) 2014 Christopher Kaster
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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
			Shader shader = Shader.Find("Highlight_Cube");

			applyShader(shader);
		} else {
			Shader shader = Shader.Find("Diffuse");

			applyShader(shader);
		}

		this.isHighlighted = highlighted;
	}

	public bool IsSelected() {
		return this.isHighlighted;
	}

	public void ToggleHighlight() {
		isHighlighted = !isHighlighted;

		this.SetHighlighted(isHighlighted);
	}

	private void applyShader(Shader shader) {
		if (shader == null) {
			Debug.LogError("Invalid shader!");
		}

		for(int i = 0; i < this.transform.childCount; i++) {
			Transform child = this.transform.GetChild(i);

			child.renderer.material.shader = shader;
		}
	}
}
