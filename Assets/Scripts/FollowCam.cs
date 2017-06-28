

using UnityEngine;

namespace Spine.Unity.Examples {
	public class FollowCam : MonoBehaviour {
		public Transform target;
		public Vector3 offset;
		public float smoothing = 5f;

		public bool constrain;
		public Vector2 min;
		public Vector2 max;

		// Update is called once per frame
		void LateUpdate () {
			
			Vector3 goalPoint;

			if (constrain) {
				float goalX = Mathf.Clamp(target.position.x + offset.x, min.x, max.x);
				float goalY = Mathf.Clamp(target.position.y + offset.y, min.y, max.y);
				goalPoint = new Vector3(goalX, goalY, target.position.z + offset.z);
			} else {
				goalPoint = target.position + offset;
			}

			transform.position = Vector3.Lerp(transform.position, goalPoint, smoothing * Time.deltaTime);
		}
	}
}