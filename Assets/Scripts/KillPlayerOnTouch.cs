using GameManagement;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			GameManager.Instance.GameOver();
		}
	}
}
