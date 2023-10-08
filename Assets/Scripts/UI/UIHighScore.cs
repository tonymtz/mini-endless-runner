using GameManagement;
using UnityEngine;

namespace UI {
	public class UIHighScore : MonoBehaviour {
		[SerializeField] OutlinedText text;
		[SerializeField] string highestText = "Your best run:\n{0}";
		[SerializeField] string newRecordText = "New\nrecord!";

		int _currentHighest;

		void Start () {
			int score = GameManager.Instance.Score;
			_currentHighest = PlayerPrefs.GetInt("highest-score", 0);

			if (score > _currentHighest) {
				// new highest!
				text.SetText(newRecordText);
				PlayerPrefs.SetInt("highest-score", score);
			} else {
				// nothing!
				text.SetText(string.Format(highestText, _currentHighest));
			}
		}
	}
}
