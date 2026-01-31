using UnityEngine;

namespace ggj26
{
    public class PlayerServiceConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector2 GraceMarginInSeconds { get; private set; }
    }
}
