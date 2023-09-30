using UnityEngine;
using UnityEngine.UI;

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

		[SerializeField] Text text;
		[SerializeField] string label;

		void Update () {
			if (label != null && label.Trim() != "") {
				text.text = string.Format("{0}\n{1}", label, GameManager.Instance.Score);
			} else {
				text.text = string.Format("{0}", GameManager.Instance.Score);
			}
		}

		public void Animate () {
			GetComponent<Animator>().SetTrigger("Bump");
		}
	}
}
