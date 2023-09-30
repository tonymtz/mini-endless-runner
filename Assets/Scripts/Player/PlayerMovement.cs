using UnityEngine;

namespace Player {
	public class PlayerMovement : MonoBehaviour {
		Rigidbody2D _rigidbody2D;

		[SerializeField] Transform groundCheck;
		[SerializeField] LayerMask groundLayer;
		[SerializeField] float speed = 8f;
		[SerializeField] float jump = 16f;

		void Start () {
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		void Update () {
			if (Input.GetButtonDown("Jump") && IsGrounded()) {
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jump);
			}

			if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f) {
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y*0.5f);
			}
		}

		void FixedUpdate () {
			_rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
		}

		bool IsGrounded () {
			return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
		}
	}
}
