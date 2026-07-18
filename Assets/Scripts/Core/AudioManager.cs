using Common;
using System;
using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour, ILoadingSceneEntity, IPersistentManager
    {
        [Header("Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio")]
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip bossMusic;
        [SerializeField] private AudioClip popSound;

        private Action<ILoadingSceneEntity> _onReady;

        void ILoadingSceneEntity.OnCreation(Action<ILoadingSceneEntity> onReady)
        {
            _onReady = onReady;
        }

        private void Start()
        {
            _onReady.Invoke(this);
        }


        public void PlayBossMusic()
        {
            AudioClip clip = bossMusic;
            PlayMusic(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayNormalMusic()
        {
            AudioClip clip = backgroundMusic;
            PlayMusic(clip);
        }


        public void PlayPop()
        {
            var clip = popSound;
            if (clip == null)
            {
                return;
            }
            sfxSource.PlayOneShot(clip);
        }
    }
}
