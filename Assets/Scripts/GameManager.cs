using Enemies;
using Player;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager Instance { get; private set; }

	bool _isGameOver;
	bool _isPlaying;
	float _score;
	PlayerMovement _playerMovement;

	public bool IsGameOver => _isGameOver;
	public bool IsPlaying => _isPlaying;
	public int Score => (int)_score;

	[SerializeField] GameObject startScreen;
	[SerializeField] GameObject playScreen;
	[SerializeField] GameObject gameoverScreen;
	[SerializeField] int pointsPerSecond = 3;

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
		_score = 0;
		_isPlaying = true;
		_isGameOver = false;
		startScreen.SetActive(false);
		playScreen.SetActive(true);
		gameoverScreen.SetActive(false);
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

	#region Buffs

	[SerializeField] int bonusPerBuff = 20;

	void BuffPicked (string display, float time) {
		_score += bonusPerBuff;
		UIEffect.Instance.StartEffect(display, time);
		UIScoreWithAnimation.Instance.Animate();
	}

	#region Player Speed

	[Header("Buff: Player Speed")]
	float _originalPlayerSpeed;

	[SerializeField] float moreSpeed = 12f;
	[SerializeField] float moreSpeedTime = 3f;

	public void StartMoreSpeed () {
		BuffPicked("Speed++!", moreSpeedTime);
		_originalPlayerSpeed = _playerMovement.Speed;
		_playerMovement.Speed = moreSpeed;
		StartCoroutine(EndMoreSpeed());
	}

	IEnumerator EndMoreSpeed () {
		yield return new WaitForSeconds(moreSpeedTime);
		UIEffect.Instance.HideEffect();
		_playerMovement.Speed = _originalPlayerSpeed;
	}

	#endregion

	#region Player Jump

	[Header("Buff: Player Jump")]
	float _originalPlayerJump;

	[SerializeField] float moreJump = 24f;
	[SerializeField] float moreJumpTime = 4f;

	public void StartMoreJump () {
		BuffPicked("Jump++!", moreJumpTime);
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

	[Header("Buff: Pumpking Slowed")]
	float _originalPumpkingSpeed;
	PumpkingController _pumpkingController;

	[SerializeField] float pumpkingSlow = 6f;
	[SerializeField] float pumpkingSlowTime = 3f;

	public void StartPumpkingSlow () {
		BuffPicked("Pumpking slowed!", pumpkingSlowTime);
		_pumpkingController = FindObjectsByType<PumpkingController>(FindObjectsSortMode.None)[0];
		_originalPumpkingSpeed = _pumpkingController.Speed;
		_pumpkingController.Speed = pumpkingSlow;
		StartCoroutine(EndPumpkingSlow());
	}

	IEnumerator EndPumpkingSlow () {
		yield return new WaitForSeconds(pumpkingSlowTime);
		UIEffect.Instance.HideEffect();
		_pumpkingController.Speed = _originalPumpkingSpeed;
	}

	#endregion

	#region Fly

	[Header("Buff: Player Fly")]
	[SerializeField] float playerFlyTime = 4f;

	public void StartPlayerFly () {
		BuffPicked("Infinite Jump!", playerFlyTime);
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
