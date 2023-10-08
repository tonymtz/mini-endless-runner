using GameManagement;
using UnityEngine;

namespace UI {
	public class UIHighScore : MonoBehaviour {
		[SerializeField] OutlinedText text;

		int _currentHighest;

		void Start () {
			int score = GameManager.Instance.Score;
			_currentHighest = PlayerPrefs.GetInt("highest-score", 0);

			if (score > _currentHighest) {
				// new highest!
				text.SetText("New\nrecord!");
				PlayerPrefs.SetInt("highest-score", score);
			} else {
				// nothing!
				text.SetText(string.Format("Your record:\n{0}", _currentHighest));
			}
		}
	}
}
