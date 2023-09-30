using UnityEngine;

namespace Levels {
	public class PlatformGenerator : MonoBehaviour {
		[SerializeField] Transform[] platforms;
		[SerializeField] Transform nextPlatformPosition;

		PlatformController _latestPlatform;

		// Start is called before the first frame update
		void Start () {
			AddPlatform();
		}

		void AddPlatform () {
			int randomIndex = Random.Range(0, platforms.Length);
			Transform target = _latestPlatform ? _latestPlatform.NextPlatformPosition : nextPlatformPosition;
			Transform newPlatform = Instantiate(platforms[randomIndex], target.position, Quaternion.identity);
			PlatformController newPlatformController = newPlatform.GetComponent<PlatformController>();

			// Subscribe to player trigger event
			newPlatformController.OnPlayerTrigger += OnPlayerTriggered;

			// Teardown subscription from previous
			if (_latestPlatform != null)
				_latestPlatform.OnPlayerTrigger -= OnPlayerTriggered;

			// Now latest is the just created
			_latestPlatform = newPlatformController;
		}

		void OnPlayerTriggered () {
			AddPlatform();
		}
	}
}
