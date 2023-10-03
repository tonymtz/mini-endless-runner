using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIHighScore : MonoBehaviour {
		[SerializeField] Text text;

		int _currentHighest;

		void Start () {
			int score = GameManager.Instance.Score;
			_currentHighest = PlayerPrefs.GetInt("highest-score", 0);

			if (score > _currentHighest) {
				// new highest!
				text.text = "New\nrecord!";
				PlayerPrefs.SetInt("highest-score", score);
			} else {
				// nothing!
				text.text = string.Format("Your record:\n{0}", _currentHighest);
			}
		}
	}
}
