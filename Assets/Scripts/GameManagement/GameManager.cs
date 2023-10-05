using Enemies;
using Player;
using System.Collections;
using UI;
using UnityEngine;

namespace GameManagement {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }

		bool _isGameOver;
		bool _isPlaying;
		float _score;
		PlayerMovement _playerMovement;

		public bool IsGameOver => _isGameOver;
		public bool IsPlaying => _isPlaying;
		public int Score => (int)_score;

		[Header("Screens")]
		[SerializeField] GameObject startScreen;
		[SerializeField] GameObject playScreen;
		[SerializeField] GameObject gameoverScreen;

		[Header("Scoring")]
		[SerializeField] int pointsPerSecond = 3;

		float _currentGameSpeed;

		void Awake () {
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this) {
				Destroy(this);
			} else {
				Instance = this;
			}
		}

		void Update () {
			if (!_isGameOver) {
				_score += pointsPerSecond*Time.deltaTime;
			}
		}

		void Start () {
			_playerMovement = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None)[0];
			_isPlaying = false;
			_isGameOver = false;

			playScreen.SetActive(false);
			gameoverScreen.SetActive(false);
			startScreen.SetActive(true);
		}

		public void StartGame () {
			// update local vars
			_score = 0;
			_isPlaying = true;
			_isGameOver = false;

			// update screens
			startScreen.SetActive(false);
			playScreen.SetActive(true);
			gameoverScreen.SetActive(false);

			// initialize other objects
			_playerMovement.SpeedMultiplier = 1;
			SpeedManager.Instance.StartGame();
			GetComponent<BuffsGenerator>().StartGeneration();
		}

		public void GameOver () {
			_isPlaying = false;
			_isGameOver = true;
			startScreen.SetActive(false);
			playScreen.SetActive(false);
			gameoverScreen.SetActive(true);
			GetComponent<BuffsGenerator>().StopGeneration();

			// Highest score is updated by UIHighScore component
		}

		public void ScoreBonus (int bonus) {
			_score += bonus;
		}

		#region Buffs

		[Header("Buffs (Power-ups)")]
		[SerializeField] int bonusPerBuff = 20;

		void BuffPicked (string display, float time) {
			_score += bonusPerBuff;
			UIEffect.Instance.StartEffect(display, time);
			UIScoreWithAnimation.Instance.Animate();
		}

		#region Player Speed

		[Header("Buff: Player Speed")]
		[SerializeField] float moreSpeedMultiplier = 1.25f;
		[SerializeField] float moreSpeedTime = 3f;
		[SerializeField] string moreSpeedLabel;

		public void StartMoreSpeed () {
			BuffPicked(moreSpeedLabel, moreSpeedTime);
			_playerMovement.SpeedMultiplier = moreSpeedMultiplier;
			StartCoroutine(EndMoreSpeed());
		}

		IEnumerator EndMoreSpeed () {
			yield return new WaitForSeconds(moreSpeedTime);
			UIEffect.Instance.HideEffect();
			_playerMovement.SpeedMultiplier = 1f;
		}

		#endregion

		#region Player Jump

		float _originalPlayerJump;

		[Header("Buff: Player Jump")]
		[SerializeField] float moreJump = 24f;
		[SerializeField] float moreJumpTime = 4f;
		[SerializeField] string moreJumpLabel;

		public void StartMoreJump () {
			BuffPicked(moreJumpLabel, moreJumpTime);
			_originalPlayerJump = _playerMovement.Jump;
			_playerMovement.Jump = moreJump;
			StartCoroutine(EndMoreJump());
		}

		IEnumerator EndMoreJump () {
			yield return new WaitForSeconds(moreJumpTime);
			UIEffect.Instance.HideEffect();
			_playerMovement.Jump = _originalPlayerJump;
		}

		#endregion

		#region Pumpking Slowed

		float _originalPumpkingSpeedMultiplier;
		PumpkingController _pumpkingController;

		[Header("Buff: Pumpking Slowed")]
		[SerializeField] float pumpkingSlowMultiplier = 0.85f;
		[SerializeField] float pumpkingSlowTime = 3f;
		[SerializeField] string pumpkingSlowLabel;

		public void StartPumpkingSlow () {
			BuffPicked(pumpkingSlowLabel, pumpkingSlowTime);
			_pumpkingController = FindObjectsByType<PumpkingController>(FindObjectsSortMode.None)[0];
			_originalPumpkingSpeedMultiplier = _pumpkingController.SpeedMultiplier;
			_pumpkingController.SpeedMultiplier = pumpkingSlowMultiplier;
			StartCoroutine(EndPumpkingSlow());
		}

		IEnumerator EndPumpkingSlow () {
			yield return new WaitForSeconds(pumpkingSlowTime);
			UIEffect.Instance.HideEffect();
			_pumpkingController.SpeedMultiplier = _originalPumpkingSpeedMultiplier;
		}

		#endregion

		#region Fly

		[Header("Buff: Player Fly")]
		[SerializeField] float playerFlyTime = 4f;
		[SerializeField] string playerFlyLabel;

		public void StartPlayerFly () {
			BuffPicked(playerFlyLabel, playerFlyTime);
			_playerMovement.CanFly = true;
			StartCoroutine(EndPlayerFly());
		}

		IEnumerator EndPlayerFly () {
			yield return new WaitForSeconds(playerFlyTime);
			UIEffect.Instance.HideEffect();
			_playerMovement.CanFly = false;
		}

		#endregion

		#endregion
	}
}
