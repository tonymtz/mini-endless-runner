using UnityEngine;

public class SimpleCameraController : MonoBehaviour {
	Vector3 _velocity = Vector3.zero;

	[SerializeField] Transform target;
	[SerializeField] float smoothTime = 0.1f;
	[SerializeField] Vector3 offset;

	void LateUpdate () {
		Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z) + offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
	}
}
