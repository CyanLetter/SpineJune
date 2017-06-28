using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSwitch : MonoBehaviour {

	public SwitchGate gate;
	Animator anim;
	bool triggered = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (!triggered) {
			triggered = true;
			gate.Unlock();
			anim.SetTrigger("activate");
		}
	}
}
