using GameManagement;
using UnityEngine;

namespace Levels {
	public class PlatformGenerator : MonoBehaviour {
		[SerializeField] Transform[] platformsEasy;
		[SerializeField] Transform[] platformsMedium;
		[SerializeField] Transform nextPlatformPosition;

		PlatformController _latestPlatform;

		public PlatformController LatestPlatform => _latestPlatform;
		public Transform nextBuff;

		void Start () {
			AddPlatform();
		}

		void AddPlatform () {
			Transform[] platformPool = SpeedManager.Instance.CurrentLevelDifficulty == LevelDifficulty.Easy ? platformsEasy : platformsMedium;
			int randomIndex = Random.Range(0, platformPool.Length);
			Transform target = LatestPlatform ? LatestPlatform.NextPlatformPosition : nextPlatformPosition;
			Transform newPlatform = Instantiate(platformPool[randomIndex], target.position, Quaternion.identity);
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
