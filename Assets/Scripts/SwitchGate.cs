using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGate : MonoBehaviour {

	public SpriteRenderer[] locks;
	public Color unlockColor;
	Animator anim;
	int locksTriggered = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Unlock() {
		locks[locksTriggered].color = unlockColor;
		locksTriggered += 1;
		if (locksTriggered >= locks.Length) {
			anim.SetTrigger("activate");
		}
	}
}
