using UnityEngine;

namespace Buffs {
	public class StartPlayerFly : MonoBehaviour {
		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				GameManager.Instance.StartPlayerFly();
				Destroy(gameObject);
			}
		}
	}
}
