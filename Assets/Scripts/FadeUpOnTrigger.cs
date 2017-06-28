using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUpOnTrigger : MonoBehaviour {

	public Light[] lights;
	public float fadeTime;
	public float targetIntensity;

	float lerpTime;
	bool lerping = false;
	float percentComplete;
	
	void Start() {
		for (int i = 0; i < lights.Length; i++) {
			lights[i].intensity = 0f;
		}
	}

	// Update is called once per frame
	void Update () {
		if (lerping) {
			lerpTime += Time.deltaTime;
			percentComplete = lerpTime / fadeTime;

			if (percentComplete >= 1f) {
				percentComplete = 1f;
				lerping = false;
			}

			for (int i = 0; i < lights.Length; i++) {
				lights[i].intensity = Mathf.Lerp(0f, targetIntensity, percentComplete);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!lerping && lerpTime < fadeTime) {
			lerping = true;
		}
	}
}
