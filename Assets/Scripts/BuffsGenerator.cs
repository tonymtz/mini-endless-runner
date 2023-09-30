using Levels;
using UnityEngine;

public class BuffsGenerator : MonoBehaviour {
	[SerializeField] Transform[] buffs;
	[SerializeField] int addBuffStartSeconds = 3;
	[SerializeField] int addBuffEverySeconds = 8;

	PlatformGenerator _platformGenerator;

	void Start () {
		_platformGenerator = FindObjectsByType<PlatformGenerator>(FindObjectsSortMode.None)[0];
	}

	public void StartGeneration () {
		InvokeRepeating("GenerateRandomBuff", addBuffStartSeconds, addBuffEverySeconds);
	}

	public void StopGeneration () {
		StopAllCoroutines();
	}

	void GenerateRandomBuff () {
		int randomIndex = Random.Range(0, buffs.Length);
		_platformGenerator.nextBuff = buffs[randomIndex];
	}
}
