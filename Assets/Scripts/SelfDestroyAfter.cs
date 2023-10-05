using System.Collections;
using UnityEngine;

public class SelfDestroyAfter : MonoBehaviour {
	[SerializeField] float timeout = 1f;

	void Start () {
		StartCoroutine(SelfDestroy());
	}

	IEnumerator SelfDestroy () {
		yield return new WaitForSeconds(timeout);
		Destroy(gameObject);
	}
}
