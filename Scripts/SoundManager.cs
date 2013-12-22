using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip rotateSound;
	public AudioClip winSound;

	private AudioSource source;

	private static SoundManager soundManager;

	void Start() {
		SoundManager.soundManager = this;

		this.source = this.GetComponent<AudioSource>();
	}

	public void playRotateSound() {
		this.source.audio.clip = rotateSound;
		this.source.Play();
	}

	public void playWinSound() {
		this.source.audio.clip = winSound;
		this.source.Play();
	}

	public static void PlayRotateSound() {
		soundManager.playRotateSound();
	}

	public static void PlayWinSound() {
		soundManager.playWinSound();
	}
}
