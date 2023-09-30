using UnityEngine;

namespace Player {
	public class PlayerMovement : MonoBehaviour {
		Rigidbody2D _rigidbody2D;
		bool _canFly;

		[SerializeField] Transform groundCheck;
		[SerializeField] LayerMask groundLayer;
		[SerializeField] float speed = 8f;
		[SerializeField] float jump = 16f;

		public float Speed {
			get {
				return speed;
			}
			set {
				speed = value;
			}
		}
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

		void Start () {
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		void Update () {
			if (!GameManager.Instance.IsPlaying) {
				return;
			}

			if (Input.GetButtonDown("Jump") && IsGrounded()) {
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Jump);
			}

			if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f) {
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y*0.5f);
			}
		}

		void FixedUpdate () {
			if (GameManager.Instance.IsPlaying) {
				_rigidbody2D.velocity = new Vector2(Speed, _rigidbody2D.velocity.y);
			} else {
				_rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
			}
		}

		bool IsGrounded () {
			return CanFly || Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
		}
	}
}
