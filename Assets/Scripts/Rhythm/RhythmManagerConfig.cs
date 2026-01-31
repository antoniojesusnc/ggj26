using UnityEngine;

namespace ggj26
{
    public class RhythmManagerConfig : ScriptableObject
    {
        [field: Header("Dance Floor")]
        [field: SerializeField]
        public Vector2 Size { get; private set; } = Vector2.one;

        [field: SerializeField] public Color[] Colors { get; private set; } = new[] { Color.magenta };
    }
}