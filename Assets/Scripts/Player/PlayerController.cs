using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player {
	public class PlayerController : MonoBehaviour {
		void Update () {
			if (GameManager.Instance.IsGameOver) {
				if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0)) {
					SceneManager.LoadScene(0);
				}
			} else if (!GameManager.Instance.IsPlaying) {
				if (Input.GetButtonDown("Jump")) {
					GameManager.Instance.StartGame();
				}
			}
		}
	}
}
