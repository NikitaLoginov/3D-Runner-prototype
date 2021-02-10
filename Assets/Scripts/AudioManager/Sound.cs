using UnityEngine;

namespace AudioManager
{
    [System.Serializable] // attribute that allows this to be adjustable in inspector
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0.0f,1f)] // attribute that adds sliders for this values to the inspector
        public float volume;
        [Range(0.1f,3f)]
        public float pitch;

        [HideInInspector] // attribute that hides value from the inspector
        public AudioSource source;

        public bool loop;
    }
}
