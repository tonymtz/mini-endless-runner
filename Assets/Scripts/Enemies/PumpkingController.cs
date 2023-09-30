using UnityEngine;

namespace Enemies {
	public class PumpkingController : MonoBehaviour {
		Rigidbody2D _rigidbody2D;

		[SerializeField] float speed = 6f;

		void Start () {
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		void FixedUpdate () {
			_rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
		}
	}
}
