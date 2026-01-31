using UnityEngine;

namespace ggj26
{
    public static class Vector2LerpRateExtensions 
    {
        public static int Vector2IntLerpRate(this Vector2Int vector2, float rate)
        {
            return Mathf.RoundToInt(Mathf.Lerp(vector2.x, vector2.y, rate));
        }
        
        public static float Vector2LerpAutoRate(this Vector2 vector2) => vector2.Vector2LerpRate(Random.value);
        public static float Vector2LerpRate(this Vector2 vector2, float rate)
        {
            return Mathf.Lerp(vector2.x, vector2.y, rate);
        }
    }
}
