using UnityEngine;

public class SimpleCameraController : MonoBehaviour {
	Vector3 _offset = new Vector3(0f, 0f, 0f);
	float _smoothTime = 0.1f;
	Vector3 _velocity = Vector3.zero;

	[SerializeField] Transform target;

	private void LateUpdate () {
		Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z) + _offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
	}
}
