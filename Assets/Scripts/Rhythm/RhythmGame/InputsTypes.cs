using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace ggj26
{
    public enum InputsTypes 
    {
        None = -1,
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3 ,
        Size = 4,
    }

    public static class InputsTypesExtensions
    {
        public static InputsTypes GetAnyExceptThis(this InputsTypes input)
        {
            var inputs = new List<InputsTypes>()
            {
                InputsTypes.Up,
                InputsTypes.Down,
                InputsTypes.Left,
                InputsTypes.Right,
            };
            inputs.Remove(input);
            return inputs.GetRandom();
        }
    }
}
