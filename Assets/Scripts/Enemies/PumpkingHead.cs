using Unity.Mathematics;
using UnityEngine;

namespace Enemies {
	public class PumpkingHead : MonoBehaviour {
		Vector3 _offset = new Vector3(0f, 0f, 0f);
		Vector3 _velocity = Vector3.zero;

		[SerializeField] Transform target;
		[SerializeField] float smoothTime = 0.5f;
		[SerializeField] float maxHeight;
		[SerializeField] float minHeight;

		void LateUpdate () {
			float positionY = math.clamp(target.position.y, minHeight, maxHeight);
			Vector3 targetPosition = new Vector3(transform.position.x, positionY, transform.position.z) + _offset;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
		}
	}
}
