using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIEffect : MonoBehaviour {
		#region Singleton

		public static UIEffect Instance { get; private set; }

		void Awake () {
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this) {
				Destroy(this);
			} else {
				Instance = this;
			}
		}

		#endregion

		float _maxTime;
		float _currentTime;

		[SerializeField] Text label;
		[SerializeField] Slider slider;

		void Start () {
			HideEffect();
		}

		public void StartEffect (string display, float time) {
			slider.gameObject.SetActive(true);
			_currentTime = time;
			slider.maxValue = time;
			label.text = display;
		}

		void Update () {
			if (_currentTime < 0) return;
			_currentTime -= Time.deltaTime;
			slider.value = _currentTime;
		}

		public void HideEffect () {
			label.text = "";
			slider.gameObject.SetActive(false);
		}
	}
}
