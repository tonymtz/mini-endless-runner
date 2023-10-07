using System.Collections;
using UnityEngine;

namespace UI {
	public class UICurtain : MonoBehaviour {
		public static UICurtain Instance { get; private set; }

		[SerializeField] Animator animator;
		[SerializeField] float chibiPumpkingChompTimeout;
		[SerializeField] GameObject gameOverAnimation;

		private string _roomName;

		void Awake () {
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this) {
				Destroy(this);
			} else {
				Instance = this;
			}

			animator = GetComponent<Animator>();
		}

		void Start () {
			gameOverAnimation.SetActive(false);
		}

		public void CloseCurtain () {
			animator.SetBool("CurtainOpen", false);
		}

		public void OpenCurtain () {
			animator.SetBool("CurtainOpen", true);
		}

		public void StartGameOverAnimation () {
			StartCoroutine(GameOverAnimation());
		}

		IEnumerator GameOverAnimation () {
			CloseCurtain();
			gameOverAnimation.SetActive(true);
			yield return new WaitForSeconds(chibiPumpkingChompTimeout);
			GameObject.FindWithTag("ChibiPumpking").GetComponent<Animator>().SetTrigger("Eating");
		}
	}
}
