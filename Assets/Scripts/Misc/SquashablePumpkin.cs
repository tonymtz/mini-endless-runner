using Audio;
using GameManagement;
using System.Collections;
using UI;
using UnityEngine;

namespace Misc {
	public class SquashablePumpkin : MonoBehaviour {
		[SerializeField] float teardownTimeout = 2f;
		[SerializeField] int bonus = 5;

		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				GameManager.Instance.ScoreBonus(bonus);
				UIScoreWithAnimation.Instance.Animate();
				OnActorInteraction();
				AudioManager.Instance.Sfx("squash");
			} else if (other.CompareTag("Pumpking")) {
				OnActorInteraction();
			}
		}

		void OnActorInteraction () {
			GetComponent<Animator>().SetBool("Squashed", true);
			StartCoroutine(Teardown());
		}

		IEnumerator Teardown () {
			yield return new WaitForSeconds(teardownTimeout);
			Destroy(gameObject);
		}
	}
}
