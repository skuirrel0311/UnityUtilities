using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class AudioManager : SingletonMonobehaviour<AudioManager>
    {
        [SerializeField]
        AudioSource audioSource = null;
        [SerializeField]
        AudioSource loopAudioSource = null;

        Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

        protected override void OnDestroy()
        {
            loopAudioSource.Stop();
            base.OnDestroy();
        }

        public void Play(string name)
        {
            loopAudioSource.clip = GetAudioClip(name);

            if (loopAudioSource.clip == null) return;
            loopAudioSource.Play();
        }

        public void PlayOneShot(string name, float volume = 1.0f)
        {
            AudioClip clip = GetAudioClip(name);

            if (clip == null) return;
            audioSource.PlayOneShot(clip, volume);
        }

        public void PlayOneShot(string name, Vector3 position, float volume = 1.0f)
        {
            AudioClip clip = GetAudioClip(name);

            if (clip == null) return;
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }

        public void SetBGMVolume(float volume)
        {
            loopAudioSource.volume = volume;
        }

        AudioClip GetAudioClip(string name)
        {
            AudioClip clip;

            if (clipDictionary.TryGetValue(name, out clip)) return clip;

            clip = MyAssetStore.Instance.GetAsset<AudioClip>(name, "Audios/");
            
            if (clip == null) return null;
            
            clipDictionary.Add(name, clip);
            return clip;
        }
    }
}