using GameManagement;
using System.Collections;
using UnityEngine;

namespace Misc {
	public class Crow : MonoBehaviour {
		[SerializeField] float speed = 4f;
		[SerializeField] float teardownTimeout = 5f;
		[SerializeField] int bonus = 2;

		bool _isScared;
		Rigidbody2D _rigidbody2D;

		void Start () {
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		void FixedUpdate () {
			if (!_isScared) {
				return;
			}

			_rigidbody2D.velocity = new Vector2(speed/2, speed);
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (other.CompareTag("Player")) {
				GameManager.Instance.ScoreBonus(bonus);
				OnActorInteraction();
			} else if (other.CompareTag("Pumpking")) {
				OnActorInteraction();
			}
		}

		void OnActorInteraction () {
			_isScared = true;
			GetComponent<Animator>().SetBool("Flying", true);
			StartCoroutine(Teardown());
		}

		IEnumerator Teardown () {
			yield return new WaitForSeconds(teardownTimeout);
			Destroy(gameObject);
		}
	}
}
