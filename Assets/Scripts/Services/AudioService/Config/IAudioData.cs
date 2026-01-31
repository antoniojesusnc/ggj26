using System;
using System.Collections.Generic;
using ggj26;
using UnityEngine;

namespace Urd.Audio
{
    public interface IAudioData
    {
        Ggj26AudioTypes Type { get; }
        AudioClip Clip { get; }
        List<AudioClip> AdditionalClips { get; }
        float Volume { get; }
        AudioMixerType Mixer { get;  }
        float Pitch { get; }
        public bool Loop { get; }
        public float FadeOutDuration { get; }
    }
}
