using System;
using System.Collections.Generic;
using ggj26.Event;
using Supyrb;
using UnityEngine;

namespace ggj26
{
    public class UIRhythmLineElement : MonoBehaviour
    {
        private UIRhythmConfig _config;
        private List<InputsBeat> _beats;
        [field: SerializeField] public InputsTypes Input { get; private set; }
        [field: SerializeField] public RectTransform InitialPosition { get; private set; }
        [field: SerializeField] public RectTransform FinalPosition { get; private set; }
        [field: SerializeField] public RectTransform _parent;

        private List<UIRhythmInputs> _inputsBeats = new List<UIRhythmInputs>();
        
        private Vector3 _jump;

        public void Init(UIRhythmConfig config, List<InputsBeat> beats)
        {
            _config = config;
            _beats = beats;

            SetJump();
            Signals.Get<OnBeatEvent>().AddListener(OnBeat);
        }

        private void SetJump()
        {
            var direction = (FinalPosition.position - InitialPosition.position);
            _jump = direction.normalized * direction.magnitude / (float)_config.StepsPerLine;
        }

        private void OnBeat()
        {
            MoveBeatInputs();
            if (RhythmManager.Instance.CurrentBeat + _config.StepsPerLine == _beats[0].Beat)
            {
                GenerateBeatInput(_beats[0]);
            }
            CheckForRemoveBeats();
        }

        private void CheckForRemoveBeats()
        {
            if (_inputsBeats.Count <= 0)
            {
                return;
            }
            
            if (RhythmManager.Instance.CurrentBeat > _inputsBeats[0].InputBeatData.Beat)
            {
                var inputBeat = _inputsBeats[0]; 
                _inputsBeats.RemoveAt(0);
                inputBeat.MoveTo(inputBeat.transform.position + _jump, destroyAfterMove: true);
            }
        }

        private void MoveBeatInputs()
        {
            for (int i = 0; i < _inputsBeats.Count; i++)
            {
                var inputsBeat = _inputsBeats[i];
                var initialBeat = inputsBeat.InputBeatData.Beat - _config.StepsPerLine;
                var finalBeat = inputsBeat.InputBeatData.Beat;
                float factor = (RhythmManager.Instance.CurrentBeat - initialBeat) / (float)(finalBeat - initialBeat);
                var position = Vector3.Lerp(InitialPosition.position, FinalPosition.position, factor);
                _inputsBeats[i].MoveTo(position);
            }
        }

        private void GenerateBeatInput(InputsBeat beat)
        {
            var newBeatInput = Instantiate(_config.Prefab, InitialPosition.transform.position, Quaternion.identity, _parent);
            newBeatInput.Init(_config, beat);
            _inputsBeats.Add(newBeatInput);
        }
    }
}
