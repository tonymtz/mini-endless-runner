using System;
using UnityEngine;

namespace Enemies {
	public class PumpkingController : MonoBehaviour {
		Rigidbody2D _rigidbody2D;
		Transform _player;

		[SerializeField] float speed = 6f;

		public float Speed {
			get {
				return speed;
			}
			set {
				speed = value;
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
			_rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
		}

		[SerializeField] float minDistance = 3f;
		[SerializeField] float maxDistance = 10f;
		[SerializeField] Transform ceilingVines;

		void Update () {
			ceilingVines.position = new Vector3(
				ceilingVines.position.x,
				(PlayerDistance()/4) - 3,
				ceilingVines.position.z
				);
			Debug.Log("Distance: " + PlayerDistance());
		}

		float PlayerDistance () {
			return Vector3.Distance(transform.position, _player.position);
		}
	}
}
