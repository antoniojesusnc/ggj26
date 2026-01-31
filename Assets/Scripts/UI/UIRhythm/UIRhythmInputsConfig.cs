using DG.Tweening;
using UnityEngine;

namespace ggj26
{
    public class UIRhythmInputsConfig : ScriptableObject
    {
        [field: Header("Animation")] 
        [field: SerializeField] public float AnimationDuration {get; private set;}
        [field: SerializeField] public Ease Ease {get; private set;}
    }
}
