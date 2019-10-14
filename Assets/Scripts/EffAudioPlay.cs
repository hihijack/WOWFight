using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(AudioSource))]
    public class EffAudioPlay : MonoBehaviour
    {
        public AudioClip clip;
        public float vol;

        private AudioSource audioSource;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            audioSource.volume = vol;
            audioSource.PlayOneShot(clip);
        }
    }
}