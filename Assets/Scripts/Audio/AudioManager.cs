using System;
using UnityEngine;

namespace Audio {
	public class AudioManager : MonoBehaviour {
		public static AudioManager Instance { get; private set; }

		string currentMusic;

		[SerializeField]
		Sound[] sounds;

		void Awake () {
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this) {
				Destroy(this);
			} else {
				Instance = this;
			}

			foreach (Sound sound in sounds) {
				sound.source = gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.volume = sound.volume;
				sound.source.pitch = sound.pitch;
				sound.source.loop = sound.loop;
			}
		}

		public void Sfx (string soundName) {
			Sound sound = Array.Find(sounds, sound => sound.name == soundName);

			if (sound == null)
				Debug.LogWarning("Sound: " + soundName + "not found!");
			else
				sound.source.Play();
		}

		public void Music (string soundName) {
			if (currentMusic == soundName) return;

			if (currentMusic != null) {
				StopMusic(currentMusic);
			}

			Sound sound = Array.Find(sounds, sound => sound.name == soundName);

			if (sound == null)
				Debug.LogWarning("Sound: " + soundName + "not found!");
			else {
				currentMusic = soundName;
				sound.source.Play();
			}
		}

		public void StopMusic (string soundName) {
			Sound sound = Array.Find(sounds, sound => sound.name == soundName);

			if (sound == null)
				Debug.LogWarning("Sound: " + soundName + "not found!");
			else {
				currentMusic = null;
				sound.source.Stop();
			}
		}
		
		public void StopMusic () {
			Sound sound = Array.Find(sounds, sound => sound.name == currentMusic);

			if (sound == null)
				Debug.LogWarning("Sound: " + currentMusic + "not found!");
			else {
				currentMusic = null;
				sound.source.Stop();
			}
		}

		public void MusicForStage (string stage) {
			if (stage == "play")
				Music("busqueda");
			else if (stage == "gameover")
				Music("marching");
			else
				Music("sunrise");
		}
	}
}