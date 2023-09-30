using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIScore : MonoBehaviour {
		[SerializeField] Text text;
		[SerializeField] string label = "{0}";

		Animator _animator;

		void Start () {
			_animator = GetComponent<Animator>();
		}

		void Update () {
			text.text = string.Format(label, GameManager.Instance.Score);
		}

		public void Animate () {
			_animator.SetTrigger("Bump");
		}
	}
}
