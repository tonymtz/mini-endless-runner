using UnityEngine;
using UnityEditor;

public class PlayerPrefsDeleter {
	[MenuItem("Tools/Delete All PlayerPrefs")]
	static void DeleteAllPlayerPrefs () {
		PlayerPrefs.DeleteAll();
		Debug.Log("All PlayerPrefs deleted.");
	}
}
