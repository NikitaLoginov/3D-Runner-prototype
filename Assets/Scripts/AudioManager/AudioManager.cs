using System;
using UnityEngine;

namespace AudioManager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Sound[] sounds;
        public static AudioManager Instance;
        private Sound _sound;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Something trying to spawn another AudioManager!");
                Destroy(gameObject);
            }
        
            DontDestroyOnLoad(gameObject);
        
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
    
        // Starts playback
        // Overloading Play to adjust volume when needed
        public void Play(string name)
        {
            if (!GetSound(name)) return;
            _sound.source.Play();
        }
        public void Play(string name, float volume)
        {
            if (volume > 1)
                volume = 1f;
            else if (volume < 0)
                volume = 0f;
        
            if (!GetSound(name)) return;
            _sound.source.volume = volume;
            _sound.source.Play();
        }

        // This method is look for sound in an array by name.
        // If it founds sound it assigns it to _sound if not - shows warning and returns false
        private bool GetSound(string name)
        {
            _sound = Array.Find(sounds, sound => sound.name == name);
            if (_sound == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return false;
            }
            return true;
        }

        //Stops playback
        public void Stop(string name)
        {
            if(!GetSound((name))) return;
            _sound.source.Stop();
        }

        public void Volume(string name, float volume)
        {
            if (volume > 1)
                volume = 1f;
            else if (volume < 0)
                volume = 0f;
        
            if (!GetSound(name)) return;
            _sound.source.volume = volume;
        }
    }
}
