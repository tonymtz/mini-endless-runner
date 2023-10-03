using GameManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Player {
	public class PlayerMovement : MonoBehaviour {
		Rigidbody2D _rigidbody2D;
		bool _canFly;
		bool _isJumping;
		float _coyoteTimerCounter;
		float _jumpBufferCounter;
		Animator _animator;
		float _speedMultiplier;

		[SerializeField] Transform groundCheck;
		[SerializeField] LayerMask groundLayer;
		[SerializeField] float jump = 16f;
		[SerializeField] float coyoteTime = 0.2f;
		[SerializeField] float jumpBufferTime = 0.2f;
		[SerializeField] float jumpCooldown = 0.1f;

		public float Jump {
			get {
				return jump;
			}
			set {
				jump = value;
			}
		}
		public bool CanFly {
			get {
				return _canFly;
			}
			set {
				_canFly = value;
			}
		}
		public float SpeedMultiplier {
			get {
				return _speedMultiplier;
			}
			set {
				_speedMultiplier = value;
			}
		}

		void Start () {
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
		}

		void Update () {
			if (!GameManager.Instance.IsPlaying) {
				return;
			}

			if (IsGrounded()) {
				_coyoteTimerCounter = coyoteTime;
			} else {
				_coyoteTimerCounter -= Time.deltaTime;
			}

			if (Input.GetButtonDown("Jump")) {
				_jumpBufferCounter = jumpBufferTime;
			} else {
				_jumpBufferCounter -= Time.deltaTime;
			}

			if (_coyoteTimerCounter > 0f && _jumpBufferCounter > 0f && !_isJumping) {
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Jump);
				_jumpBufferCounter = 0f;
				StartCoroutine(JumpCooldown());
			} else if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f) {
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y*0.5f);
				_coyoteTimerCounter = 0f;
			}
		}

		void FixedUpdate () {
			if (GameManager.Instance.IsPlaying) {
				float currentSpeed = SpeedManager.Instance.CurrentSpeed*_speedMultiplier;
				_rigidbody2D.velocity = new Vector2(currentSpeed, _rigidbody2D.velocity.y);
				_animator.SetBool("IsRunning", true);
			} else {
				_rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
			}

			_animator.SetBool("IsAir", Math.Abs(_rigidbody2D.velocity.y) > 0.1f);
		}

		[SerializeField] float groundCheckerRadius = 0.2f;

		bool IsGrounded () {
			return CanFly || Physics2D.OverlapCircle(groundCheck.position, groundCheckerRadius, groundLayer);
		}

		void OnDrawGizmos () {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckerRadius);
		}

		IEnumerator JumpCooldown () {
			_isJumping = true;
			yield return new WaitForSeconds(jumpCooldown);
			_isJumping = false;
		}
	}
}
