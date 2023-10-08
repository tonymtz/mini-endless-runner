using GameManagement;
using UnityEngine;

namespace UI {
	public class UIScore : MonoBehaviour {
		[SerializeField] OutlinedText text;
		[SerializeField] string label;

		void Update () {
			if (label != null && label.Trim() != "") {
				text.SetText(string.Format("{0}\n{1}", label, GameManager.Instance.Score));
			} else {
				text.SetText(string.Format("{0}", GameManager.Instance.Score));
			}
		}
	}
}
