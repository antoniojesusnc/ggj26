using UnityEngine;

namespace ggj26
{
    public static class Vector2LerpRateExtensions 
    {
        public static int Vector2IntLerpRate(this Vector2Int vector2, float rate)
        {
            return Mathf.RoundToInt(Mathf.Lerp(vector2.x, vector2.y, rate));
        }
    }
}
