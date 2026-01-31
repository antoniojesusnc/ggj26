using UnityEngine;

namespace ggj26
{
    public class SheepControllerConfig : ScriptableObject
    {
        [field: Header("Anticipation")]
        [field: SerializeField, Range(0,1)] public float AnticipationRate { get; private set; }
        [field: SerializeField] public float AnticipationTime { get; private set; }
        
        [field: Header("Delay")]
        [field: SerializeField, Range(0,1)] public float DelayRate { get; private set; }
        [field: SerializeField] public float DelayTime { get; private set; }
        
        [field: Header("Wrong")]
        [field: SerializeField, Range(0,1)] public float WrongRate { get; private set; }
    }
}
