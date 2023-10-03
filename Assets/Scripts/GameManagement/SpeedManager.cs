using System;
using UnityEngine;

namespace GameManagement {
	[Serializable]
	public class Level {
		public int scoreRequired;
		public float speed;
		// add platforms
		// add buffs
	}

	public class SpeedManager : MonoBehaviour {
		#region Singleton

		public static SpeedManager Instance { get; private set; }

		void Awake () {
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this) {
				Destroy(this);
			} else {
				Instance = this;
			}
		}

		#endregion

		[SerializeField] Level[] levels;

		float _currentGameSpeed;
		int _currentLevelIndex;

		public float CurrentSpeed => _currentGameSpeed;

		public void StartGame () {
			_currentLevelIndex = 0;
			_currentGameSpeed = levels[0].speed;
		}

		void Update () {
			if (!GameManager.Instance.IsPlaying) return;

			if (_currentLevelIndex + 1 >= levels.Length) return;

			int currentScore = GameManager.Instance.Score;
			Level nextLevel = levels[_currentLevelIndex + 1];

			if (currentScore >= nextLevel.scoreRequired) {
				_currentGameSpeed = nextLevel.speed;
				_currentLevelIndex += 1;
			}
		}
	}
}
