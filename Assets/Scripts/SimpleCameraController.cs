using UnityEngine;

public class SimpleCameraController : MonoBehaviour {
	[SerializeField] Transform target;

	void LateUpdate () {
		if (target) {
			transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
		}
	}
}
