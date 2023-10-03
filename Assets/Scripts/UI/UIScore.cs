using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIScore : MonoBehaviour {
		[SerializeField] Text text;
		[SerializeField] string label;

		void Update () {
			if (label != null && label.Trim() != "") {
				text.text = string.Format("{0}\n{1}", label, GameManager.Instance.Score);
			} else {
				text.text = string.Format("{0}", GameManager.Instance.Score);
			}
		}
	}
}
