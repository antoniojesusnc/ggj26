using ggj26;
using UnityEngine;

namespace Urd.Services
{
    [CreateAssetMenu(fileName = "AudioClipConfig", menuName = "Urd/Services/Audio/Audio Clip Config", order = 1)]

    public class AudioClipConfig : ScriptableObject
    {
        [field: SerializeField ]
        public Ggj26AudioConfigData Audio { get; private set; }
    }
}