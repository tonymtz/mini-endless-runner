using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIHighScore : MonoBehaviour {
		[SerializeField] Text text;
		[SerializeField] string label;

		int highScore;

		void Start () {
			highScore = PlayerPrefs.GetInt("highest-score", 0);
		}

		void Update () {
			if (label != null && label.Trim() != "") {
				text.text = string.Format("{0}\n{1}", label, highScore);
			} else {
				text.text = string.Format("{0}", highScore);
			}
		}
	}
}
