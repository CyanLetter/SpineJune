using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTransition : MonoBehaviour {

	public AudioSource[] sources;
	public float fadeTime;
	public int currentSource = 0;



	float lerpTime;
	bool lerping = false;
	float percentComplete;
	
	void Start() {
		for (int i = 0; i < sources.Length; i++) {
			sources[i].volume = 0f;
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < sources.Length; i++) {
			if (i == currentSource) {
				sources[i].volume += fadeTime;
			} else {
				sources[i].volume -= fadeTime;
			}
			
		}
	}

	public void ChangeSource(int newSource) {
		if (newSource < sources.Length) {
			currentSource = newSource;
		}
	}
}
