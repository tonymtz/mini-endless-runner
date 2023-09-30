using UnityEngine;

namespace Buffs {
	public class StartMoreSpeed : MonoBehaviour {
		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				GameManager.Instance.StartMoreSpeed();
				Destroy(gameObject);
			}
		}
	}
}
