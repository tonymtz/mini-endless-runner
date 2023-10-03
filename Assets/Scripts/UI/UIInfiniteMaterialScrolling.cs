using GameManagement;
using UnityEngine;

namespace UI {
	public class UiInfiniteMaterialScrolling : MonoBehaviour {
		[SerializeField] float speedMultiplier = 0.1f;

		float _offset;
		Material _material;

		void Start () {
			_offset = 0f;
			_material = GetComponent<Renderer>().material;
		}

		void Update () {
			_offset += Time.deltaTime*SpeedManager.Instance.CurrentSpeed*speedMultiplier;
			_material.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
		}
	}
}
