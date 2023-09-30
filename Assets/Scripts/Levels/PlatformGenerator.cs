using UnityEngine;

namespace Levels {
	public class PlatformGenerator : MonoBehaviour {
		[SerializeField] Transform[] platforms;
		[SerializeField] Transform nextPlatformPosition;

		PlatformController _latestPlatform;

		public PlatformController LatestPlatform => _latestPlatform;
		public Transform nextBuff;

		// Start is called before the first frame update
		void Start () {
			AddPlatform();
		}

		void AddPlatform () {
			int randomIndex = Random.Range(0, platforms.Length);
			Transform target = LatestPlatform ? LatestPlatform.NextPlatformPosition : nextPlatformPosition;
			Transform newPlatform = Instantiate(platforms[randomIndex], target.position, Quaternion.identity);
			PlatformController newPlatformController = newPlatform.GetComponent<PlatformController>();

			// Subscribe to player trigger event
			newPlatformController.OnPlayerTrigger += OnPlayerTriggered;

			// Teardown subscription from previous
			if (LatestPlatform != null)
				LatestPlatform.OnPlayerTrigger -= OnPlayerTriggered;

			// Now latest is the just created
			_latestPlatform = newPlatformController;

			// Spawn buff if next is present
			if (nextBuff != null) {
				Instantiate(nextBuff, newPlatformController.NextBuffPosition.position, Quaternion.identity);
				nextBuff = null;
			}
		}

		void OnPlayerTriggered () {
			AddPlatform();
		}
	}
}
