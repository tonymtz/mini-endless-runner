using UnityEngine;

namespace Levels {
	public class PlatformController : MonoBehaviour {
		public delegate void OnPlayerTriggerDelegate ();

		public OnPlayerTriggerDelegate OnPlayerTrigger;

		[SerializeField] Transform nextPlatformPosition;

		public Transform NextPlatformPosition => nextPlatformPosition;

		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				OnPlayerTrigger?.Invoke();
			}
		}
	}
}
