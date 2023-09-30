using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIScore : MonoBehaviour {
		[SerializeField] Text text;
		[SerializeField] string label;

		Animator _animator;

		void Start () {
			_animator = GetComponent<Animator>();
		}

		void Update () {
			if (label != null && label.Trim() != "") {
				text.text = string.Format("{0}\n{1}", label, GameManager.Instance.Score);
			} else {
				text.text = string.Format("{0}", GameManager.Instance.Score);
			}
		}

		public void Animate () {
			_animator.SetTrigger("Bump");
		}
	}
}
