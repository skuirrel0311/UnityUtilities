using System;
using UnityEngine;

namespace KanekoUtilities
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : PoolMonoBehaviour
    {
        AudioSource source;
        public AudioSource Source
        {
            get
            {
                if (source != null) return source;

                source = GetComponent<AudioSource>();

                return source;
            }
        }

        Coroutine callBackCoroutine;

        void Play(AudioClip clip, float volume = 1.0f)
        {
            Source.clip = clip;
            Source.volume = volume;
            Source.Play();
        }
        
        public void Play(AudioClip clip, Action callback, float volume = 1.0f)
        {
            Play(clip, volume);

            KKUtilities.WaitUntil(() => !IsPlaying, callback, this);
        }

        //loopするSEを鳴らしたい場合は止める条件を設定する
        public void LoopPlay(AudioClip clip, float limitLife, Action callback, float volume = 1.0f)
        {
            Source.loop = true;
            Play(clip, volume);

            KKUtilities.Delay(limitLife, () =>
            {
                Source.Stop();
                if(callback != null) callback.Invoke();
            }, this);
        }
        public void LoopPlay(AudioClip clip, Func<bool> predicate, Action callback, float volume = 1.0f)
        {
            Source.loop = true;
            Play(clip, volume);

            KKUtilities.WaitUntil(predicate, callback, this);
        }

        public bool IsPlaying { get { return Source.isPlaying; } }
        public int Priority
        {
            get
            {
                return Source.priority;
            }
            set
            {
                Source.priority = value;
            }
        }
    }
}