using UnityEngine;
using UnityEngine.Serialization;

namespace ggj26
{
    public class SheepControllerConfig : ScriptableObject
    {
        [field: Header("Anticipation")]
        [field: SerializeField, Range(0,1)] public float AnticipationRate { get; private set; }
        [field: SerializeField, Range(0,1)] public float AnticipationRateFromBeatTime { get; private set; }
        
        [field: Header("Delay")]
        [field: SerializeField, Range(0,1)] public float DelayRate { get; private set; }
        [field: SerializeField, Range(0,1)] public float DelayRateFromBeatTime { get; private set; }
        
        [field: Header("Wrong")]
        [field: SerializeField, Range(0,1)] public float WrongRate { get; private set; }
        
        [field: Header("Animation")]
        [field: SerializeField, Range(0,1)] public float BeatRateToComeBackAnimation { get; private set; }
        
    }
}
