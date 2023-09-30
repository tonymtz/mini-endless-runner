using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerOnTouch : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			SceneManager.LoadScene(0);
		}
	}
}
