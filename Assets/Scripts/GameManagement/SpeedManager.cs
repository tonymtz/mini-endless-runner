using System;
using System.Collections;
using UnityEngine;

namespace GameManagement {
	public enum LevelDifficulty {
		Easy,
		Medium
	}

	[Serializable]
	public class Level {
		public int scoreRequired;
		public float speed;
		public LevelDifficulty levelDifficulty;
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

		[SerializeField] GameObject speedUpLabel;
		[SerializeField] Level[] levels;

		float _currentGameSpeed;
		int _currentLevelIndex;
		LevelDifficulty _currentLevelDifficulty;

		public float CurrentSpeed => _currentGameSpeed;
		public LevelDifficulty CurrentLevelDifficulty => _currentLevelDifficulty;

		void Start () {
			if (speedUpLabel != null) {
				speedUpLabel.SetActive(false);
			}
		}

		public void StartGame () {
			_currentLevelIndex = 0;
			_currentGameSpeed = levels[0].speed;
			_currentLevelDifficulty = levels[0].levelDifficulty;
		}

		void Update () {
			if (!GameManager.Instance.IsPlaying) return;

			if (_currentLevelIndex + 1 >= levels.Length) return;

			int currentScore = GameManager.Instance.Score;
			Level nextLevel = levels[_currentLevelIndex + 1];

			if (currentScore >= nextLevel.scoreRequired) {
				_currentGameSpeed = nextLevel.speed;
				_currentLevelDifficulty = nextLevel.levelDifficulty;
				_currentLevelIndex += 1;
				if (speedUpLabel != null) {
					speedUpLabel.SetActive(enabled);
					StartCoroutine(HideLabel());
				}
			}
		}

		public IEnumerator HideLabel () {
			yield return new WaitForSeconds(1f);
			speedUpLabel.SetActive(false);
		}
	}
}
