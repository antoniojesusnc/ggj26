using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Audio;

namespace ggj26
{
    [Serializable]
    public class Ggj26AudioConfigData : IAudioData
    {
        [field:SerializeField] public Ggj26AudioTypes Type { get; private set; }
        [field:SerializeField] public AudioClip Clip { get; private set; }
        [field:SerializeField] public List<AudioClip> AdditionalClips { get; private set; }
        [field:SerializeField] public float Volume { get; private set; }
        [field:SerializeField] public AudioMixerType Mixer { get; private set; }
        [field: SerializeField] public float Pitch { get; private set; }
        [field:SerializeField] public bool Loop { get; private set; }
        [field:SerializeField] public float FadeOutDuration { get; private set; }
    }
}
