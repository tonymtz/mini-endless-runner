using GameManagement;
using UnityEngine;

namespace Buffs {
	public class StartMoreJump : MonoBehaviour {
		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				GameManager.Instance.StartMoreJump();
				Destroy(gameObject);
			}
		}
	}
}
