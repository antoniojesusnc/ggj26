using System;
using ggj26.Event;
using Supyrb;

namespace ggj26
{
    public class PlayerSheepAnimationControl : SheepAnimationControl
    {
        private void Start()
        {
            Signals.Get<OnBeatInputEvent>().RemoveListener(MoveSheep);
            Signals.Get<OnPlayerInputPressEvent>().AddListener(OnPlayerInput);
        }

        private void OnDestroy()
        {
            Signals.Get<OnPlayerInputPressEvent>().RemoveListener(OnPlayerInput);
        }

        private void OnPlayerInput(InputsTypes playerInput)
        {
            MoveSheep(playerInput);
        }
    }
}
