using System.Collections.Generic;
using UnityEngine;

namespace ggj26
{
    public class RhythmGameConfig : ScriptableObject
    {
        [field: Header("Audio")]
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public int Bmp { get; private set; }
        [field: SerializeField] public float BeatOffset { get; private set; }
        
        [field: Header("Level Config")]
        [field: SerializeField] public int InitialWait { get; private set; }
        [field: SerializeField] public int Waves { get; private set; }
        
        [field: SerializeField] public Vector2Int AmountInputsTogetherRange { get; private set; }
        [field: SerializeField] public Vector2Int BeatsBetweenInputsRange { get; private set; }
        
        [field: SerializeField] public List<InputsTypes> InputsInLevel { get; private set; }
        
        [field: Header("Rate To Use Other Inputs")]
        [field: SerializeField] public float RateForNewInput { get; private set; }
        [field: SerializeField] public float RateForRepeatInput { get; private set; }
        
    }
}
