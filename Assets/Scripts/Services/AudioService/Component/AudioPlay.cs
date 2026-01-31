using System;
using UnityEngine;
using Urd.Services;

namespace Urd.Audio
{
    public class AudioPlay<T> : MonoBehaviour where T : Enum
    {
        [SerializeField]
        private T _audio;
        
        private AudioService _audioService;

        void Awake()
        {
            _audioService = AudioService.Instance;
        }
        
        public void Play()
        {
            _audioService.PlaySound(_audio);
        }
    }
}
