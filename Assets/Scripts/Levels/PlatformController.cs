using UnityEngine;

namespace Levels {
	public class PlatformController : MonoBehaviour {
		public delegate void OnPlayerTriggerDelegate ();

		public OnPlayerTriggerDelegate OnPlayerTrigger;

		[SerializeField] Transform nextPlatformPosition;
		[SerializeField] Transform nextBuffPosition;

		public Transform NextPlatformPosition => nextPlatformPosition;
		public Transform NextBuffPosition => nextBuffPosition;

		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				OnPlayerTrigger?.Invoke();
			}
		}
	}
}
