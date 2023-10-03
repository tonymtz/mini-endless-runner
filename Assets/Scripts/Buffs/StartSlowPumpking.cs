using GameManagement;
using UnityEngine;

namespace Buffs {
	public class StartSlowPumpking : MonoBehaviour {
		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				GameManager.Instance.StartPumpkingSlow();
				Destroy(gameObject);
			}
		}
	}
}
