using System;
using ggj26;
using Urd.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDataConfig", menuName = "Urd/Services/Audio Data Config", order = 1)]
public class AudioDataConfig : ScriptableObject
{
    [field: SerializeField]
    public Ggj26AudioConfigData AudioData { get; private set; }
}
