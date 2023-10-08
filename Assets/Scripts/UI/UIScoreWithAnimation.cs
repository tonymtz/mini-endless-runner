using GameManagement;
using UnityEngine;

namespace UI {
	public class UIScoreWithAnimation : MonoBehaviour {
		#region Singleton

		public static UIScoreWithAnimation Instance { get; private set; }

		void Awake () {
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this) {
				Destroy(this);
			} else {
				Instance = this;
			}
		}

		#endregion

		[SerializeField] OutlinedText text;
		[SerializeField] string label;

		void FixedUpdate () {
			if (!GameManager.Instance.IsPlaying) return;

			if (label != null && label.Trim() != "") {
				text.SetText(string.Format("{0}\n{1}", label, GameManager.Instance.Score));
			} else {
				text.SetText(string.Format("{0}", GameManager.Instance.Score));
			}
		}

		public void Animate () {
			GetComponent<Animator>().SetTrigger("Bump");
		}
	}
}
