using System;
using ggj26.Event;
using Supyrb;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

namespace ggj26
{
    public class PlayerService : Singleton<PlayerService>
    {
        [SerializeField]
        private PlayerServiceConfig _config;
        
        private PlayerGameInput _playerInput;

        private void Start()
        {
            Signals.Get<OnGameBeginEvent>().AddListener(OnGameBegin);
            Signals.Get<OnGameOverEvent>().AddListener(OnGameOver);
        }

        private void OnDestroy()
        {
            Signals.Get<OnGameBeginEvent>().RemoveListener(OnGameBegin);
            Signals.Get<OnGameOverEvent>().RemoveListener(OnGameOver);
        }

        private void OnGameBegin()
        {
            _playerInput = new PlayerGameInput();
            _playerInput.Enable();

            SubscribeToInputs();
        }

        private void SubscribeToInputs()
        {
            _playerInput.PlayerInput.Left.performed += (context) => OnPressInput(InputsTypes.Left);
            _playerInput.PlayerInput.Left.canceled += (context) => OnReleaseInput(InputsTypes.Left);
            _playerInput.PlayerInput.Right.performed += (context) => OnPressInput(InputsTypes.Right);
            _playerInput.PlayerInput.Right.canceled += (context) => OnReleaseInput(InputsTypes.Right);
            _playerInput.PlayerInput.Up.performed += (context) => OnPressInput(InputsTypes.Up);
            _playerInput.PlayerInput.Up.canceled += (context) => OnReleaseInput(InputsTypes.Up);
            _playerInput.PlayerInput.Down.performed += (context) => OnPressInput(InputsTypes.Down);
            _playerInput.PlayerInput.Down.canceled += (context) => OnReleaseInput(InputsTypes.Down);
        }

        private void UnsubscribeToInputs()
        {
            _playerInput?.Disable();
            _playerInput?.Dispose();
            _playerInput = null;
        }

        private void OnGameOver()
        {
            UnsubscribeToInputs();
        }
        
        private void OnPressInput(InputsTypes input)
        {
            Signals.Get<OnPlayerInputPressEvent>().Dispatch(input);
        }
        
        private void OnReleaseInput(InputsTypes input)
        {
            Signals.Get<OnPlayerInputReleaseEvent>().Dispatch(input);
        }
    }
}
