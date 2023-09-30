using Enemies;
using Player;
using System.Collections;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance { get; private set; }

	bool _isGameOver;
	bool _isPlaying;
	float _score;
	PlayerMovement _playerMovement;
	UIEffect _uiEffect;
	UIScore _uiScore;

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
		_uiEffect = FindObjectsByType<UIEffect>(FindObjectsSortMode.None)[0];
		_uiScore = FindObjectsByType<UIScore>(FindObjectsSortMode.None)[0];
		_playerMovement = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None)[0];
		_isPlaying = false;
		_isGameOver = false;
		startScreen.SetActive(true);
		playScreen.SetActive(false);
		gameoverScreen.SetActive(false);
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
	}

	#region Buffs

	[SerializeField] int bonusPerBuff = 20;

	void BuffPicked () {
		_score += bonusPerBuff;
		_uiScore.Animate();
	}

	#region Player Speed

	[Header("Buff: Player Speed")]
	float _originalPlayerSpeed;

	[SerializeField] float moreSpeed = 12f;
	[SerializeField] float moreSpeedTime = 3f;

	public void StartMoreSpeed () {
		BuffPicked();
		_originalPlayerSpeed = _playerMovement.Speed;
		_playerMovement.Speed = moreSpeed;
		_uiEffect.StartEffect("Speed++!", moreSpeedTime);
		StartCoroutine(EndMoreSpeed());
	}

	IEnumerator EndMoreSpeed () {
		yield return new WaitForSeconds(moreSpeedTime);
		_uiEffect.HideEffect();
		_playerMovement.Speed = _originalPlayerSpeed;
	}

	#endregion

	#region Player Jump

	[Header("Buff: Player Jump")]
	float _originalPlayerJump;

	[SerializeField] float moreJump = 24f;
	[SerializeField] float moreJumpTime = 4f;

	public void StartMoreJump () {
		BuffPicked();
		_originalPlayerJump = _playerMovement.Jump;
		_playerMovement.Jump = moreJump;
		_uiEffect.StartEffect("Jump++!", moreJumpTime);
		StartCoroutine(EndMoreJump());
	}

	IEnumerator EndMoreJump () {
		yield return new WaitForSeconds(moreJumpTime);
		_uiEffect.HideEffect();
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
		BuffPicked();
		_pumpkingController = FindObjectsByType<PumpkingController>(FindObjectsSortMode.None)[0];
		_originalPumpkingSpeed = _pumpkingController.Speed;
		_pumpkingController.Speed = pumpkingSlow;
		_uiEffect.StartEffect("Pumpking slowed!", pumpkingSlowTime);
		StartCoroutine(EndPumpkingSlow());
	}

	IEnumerator EndPumpkingSlow () {
		yield return new WaitForSeconds(pumpkingSlowTime);
		_uiEffect.HideEffect();
		_pumpkingController.Speed = _originalPumpkingSpeed;
	}

	#endregion

	#region Fly

	[Header("Buff: Player Fly")]
	[SerializeField] float playerFlyTime = 4f;

	public void StartPlayerFly () {
		BuffPicked();
		_playerMovement.CanFly = true;
		_uiEffect.StartEffect("Infinite Jump!", playerFlyTime);
		StartCoroutine(EndPlayerFly());
	}

	IEnumerator EndPlayerFly () {
		yield return new WaitForSeconds(playerFlyTime);
		_uiEffect.HideEffect();
		_playerMovement.CanFly = false;
	}

	#endregion

	#endregion
}
