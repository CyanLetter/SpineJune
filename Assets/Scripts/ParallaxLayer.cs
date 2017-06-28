using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {

	public Transform parallaxCam;
	public float magnitude;
	// public Vector3 offset;

	Vector3 initialPosition;
	Vector3 initialCameraPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		initialCameraPosition = parallaxCam.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = initialPosition + ((parallaxCam.position - initialCameraPosition) * magnitude);
	}
}
