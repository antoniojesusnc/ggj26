using UnityEngine;

namespace ggj26
{
    public class SpotLightConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector2 StartDelayRange { get; private set; }
        [field: SerializeField]
        public Vector2 FlashEachRange { get; private set; }
        [field: SerializeField]
        public Vector2 RotateMaxAngleRange { get; private set; }
        [field: SerializeField]
        public Vector2 RotateSpeedRange { get; private set; }
        [field: SerializeField]
        public Vector2 IntensityRange { get; private set; }
    }
}
