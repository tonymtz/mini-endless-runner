using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class OutlinedText : MonoBehaviour {
		Text _outlineLabel;
		string _value;
		bool _valueChanged;

		[SerializeField] Text fillLabel;

		void Start () {
			_outlineLabel = GetComponent<Text>();
			if (fillLabel == null)
				fillLabel = GetComponentInChildren<Text>();
		}

		public void SetText (string newValue) {
			_value = newValue;
			_valueChanged = true;
		}

		void Update () {
			if (_valueChanged) {
				if (fillLabel != null)
					fillLabel.text = _value;
				_outlineLabel.text = _value;
				_valueChanged = false;
			}
		}
	}
}
