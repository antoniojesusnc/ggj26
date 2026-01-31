using ggj26.Event;
using Supyrb;

namespace ggj26
{
    public class PlayerSheepController : SheepController
    {
        protected override void Subscribe()
        {
            Signals.Get<OnPlayerInputPressEvent>().AddListener(OnPlayerInput);
            Signals.Get<OnPlayerInputReleaseEvent>().AddListener(OnPlayerInputRelease);
        }

        private void OnPlayerInputRelease()
        {
            MoveSheep(InputsTypes.None);
        }
        
        protected override void AfterAnimation(){}

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
