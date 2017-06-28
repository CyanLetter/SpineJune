using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTriggerEnter : MonoBehaviour {

	public UnityEvent onTrigger;

	void OnTriggerEnter2D() {
		onTrigger.Invoke();
	}
}
