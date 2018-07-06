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

        public RegisterBoolParameter AudioEnable = new RegisterBoolParameter(SaveKeyName.EnableSound, true);

        public RegisterFloatParameter MasterVolume = new RegisterFloatParameter("MasterVolume", 1.0f);
        public RegisterFloatParameter MasterSEVolume = new RegisterFloatParameter("MasterSEVolume", 1.0f);
        public RegisterFloatParameter MasterBGMVolume = new RegisterFloatParameter("MasterBGMVolume", 1.0f);
        
        Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

        protected override void Awake()
        {
            base.Awake();
            MasterBGMVolume.OnValueChanged.AddListener((volume) =>
            {
                loopAudioSource.volume = volume;
            });
        }

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

            volume = volume * MasterSEVolume.GetValue() * (AudioEnable.GetValue() ? 1.0f : 0.0f);
            audioSource.PlayOneShot(clip, volume);
        }

        public void PlayOneShot(string name, Vector3 position, float volume = 1.0f)
        {
            AudioClip clip = GetAudioClip(name);

            if (clip == null) return;

            volume = volume * MasterSEVolume.GetValue() * (AudioEnable.GetValue() ? 1.0f : 0.0f);
            AudioSource.PlayClipAtPoint(clip, position, volume );
        }

        /// <summary>
        /// 一気に設定したい場合
        /// </summary>
        public void UpdateVolume(float masterVolume = 1.0f, float seVolume = 1.0f, float bgmVolume = 1.0f)
        {
            MasterVolume.SetValue(masterVolume);
            MasterSEVolume.SetValue(seVolume);
            MasterBGMVolume.SetValue(bgmVolume);
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