using UnityEngine;
using UnityEngine.Tilemaps;

namespace Levels {
	public class DestroyableTilemap : MonoBehaviour {
		Tilemap tilemap;

		void Start () {
			tilemap = GetComponent<Tilemap>();
		}

		void OnCollisionEnter2D (Collision2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				foreach (ContactPoint2D point in collision.contacts) {
					Vector3Int cellPosition = tilemap.WorldToCell(point.point);
					tilemap.SetTile(cellPosition, null); // Elimina el tile en la posición de contacto
				}
			}
		}
	}
}
