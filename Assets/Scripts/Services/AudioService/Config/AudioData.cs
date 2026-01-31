using System;
using System.Collections.Generic;
using ggj26;
using MyBox;
using UnityEngine;
using Urd.Audio;

namespace Urd.Services
{
    [Serializable]
    public abstract class AudioData<T> : IAudioData where T : Enum
    {
        public Ggj26AudioTypes Type => AudioType;
        
        [field: SerializeField]
        [field: SearchableEnum]
        public Ggj26AudioTypes AudioType { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public List<AudioClip> AdditionalClips { get; private set; }
        [field: SerializeField, Range(0,1)] public float Volume { get; private set; } = 1;
        [field: SerializeField] public AudioMixerType Mixer { get; private set; } = AudioMixerType.Sfx;
        [field: SerializeField, Range(0.1f, 5f)] public float Pitch { get; private set; } = 1;
        [field: SerializeField] public bool Loop { get; private set; }
        [field: Header("Fade Out")]
        [field: SerializeField]
        public float FadeOutDuration { get; private set; }
    }
}