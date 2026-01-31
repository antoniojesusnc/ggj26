using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace ggj26
{
    public class UIRhythmConfig : ScriptableObject
    {
        [field: SerializeField] public int StepsPerLine;
        [field: SerializeField] public List<UIRhythmInputsKeys> BeatsImages = new List<UIRhythmInputsKeys>();
        [field: SerializeField] public UIRhythmInputs Prefab {get; private set;}
    }

    [Serializable]
    public class UIRhythmInputsKeys
    {
        [field: SerializeField] public InputsTypes InputType { get; private set; }
        [field: SerializeField, PreviewSprite] public Sprite Image { get; private set; }
    }
}