using GameManagement;
using UnityEngine;

namespace Enemies {
	public class PumpkingController : MonoBehaviour {
		Rigidbody2D _rigidbody2D;
		Transform _player;

		[SerializeField] float speedMultiplier = 0.85f;

		public float SpeedMultiplier {
			get {
				return speedMultiplier;
			}
			set {
				speedMultiplier = value;
			}
		}

		void Start () {
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		void FixedUpdate () {
			if (!GameManager.Instance.IsPlaying || GameManager.Instance.IsGameOver) {
				_rigidbody2D.velocity = Vector2.zero;
				return;
			}

			float currentSpeed = SpeedManager.Instance.CurrentSpeed*speedMultiplier;
			_rigidbody2D.velocity = new Vector2(currentSpeed, _rigidbody2D.velocity.y);
		}

		[SerializeField] Transform ceilingVines;

		void Update () {
			ceilingVines.position = new Vector3(
				ceilingVines.position.x,
				(PlayerDistance()/4) - 3,
				ceilingVines.position.z
				);
			// Debug.Log("Distance: " + PlayerDistance());
		}

		float PlayerDistance () {
			return Vector3.Distance(transform.position, _player.position);
		}
	}
}
