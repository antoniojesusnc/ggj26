using System.Collections.Generic;
using DG.Tweening;
using ggj26.Event;
using MyBox;
using Supyrb;
using UnityEngine;
using UnityEngine.UI;

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
        [field: SerializeField] public Image _placeHolder;
        [field: SerializeField] public RectTransform _linePrefab;
        [field: SerializeField] private Color _colorHighligth;
        [field: SerializeField] private Image _inputArea;

        private List<UIRhythmInputs> _inputsBeats = new List<UIRhythmInputs>();
        
        private Vector3 _jump;
        private InputsTypes _input;
        private Color _originalColor;
        private Vector3 _originalScale;

        [field: SerializeField]
        public Vector3 RealInitialPosition => InitialPosition.transform.position;
        public Vector3 RealFinalPosition => FinalPosition.transform.position;

        public void Init(UIRhythmConfig config)
        {
            _config = config;

            SetJump();
            CreateLines();
            
            _originalColor =  _inputArea.color;
            _originalScale = _inputArea.transform.lossyScale;
        }

        public void BeginBeats(List<InputsBeat> beats, InputsTypes input)
        {
            _input = input;
            _beats = beats;
            Signals.Get<OnBeatEvent>().AddListener(OnBeat);
            Signals.Get<OnPlayerInputPressEvent>().AddListener(OnPlayerBeat);
            Signals.Get<OnPlayerInputReleaseEvent>().AddListener(OnPlayerRelease);
        }

        private void OnPlayerRelease(InputsTypes input)
        {
            if (input == _input)
            {
                ResetInputArea();
            }
        }

        private void OnPlayerBeat(InputsTypes input)
        {
            if (input == _input && _inputArea.color != _colorHighligth)
            {
                ResetInputArea();
                _inputArea.color = _colorHighligth;
                _inputArea.transform.localScale = Vector3.one*1.1f;
            }
        }

        private void ResetInputArea()
        {
            _inputArea.color = _originalColor;
            _inputArea.transform.localScale = _originalScale;
        }

        private void CreateLines()
        {
            float lines = _config.StepsPerLine;
            //var offset = _jump;
            var offset = _jump*0.1f;
            
            _linePrefab.position = RealInitialPosition + offset;
            for (int i = 1; i < lines; i++)
            {
                var position = Vector3.Lerp(RealInitialPosition, RealFinalPosition, i / lines);
                SetLine(position + offset);
            }
            _linePrefab.gameObject.SetActive(false);
        }

        private void SetLine(Vector3 position)
        {
            Instantiate(_linePrefab, position,  Quaternion.identity, _linePrefab.transform.parent);
        }

        private void OnDestroy()
        {
            Signals.Get<OnBeatEvent>().RemoveListener(OnBeat);
            Signals.Get<OnPlayerInputPressEvent>().RemoveListener(OnPlayerBeat);
        }

        private void SetJump()
        {
            var direction = (FinalPosition.position - InitialPosition.position);
            _jump = direction.normalized * (direction.magnitude / (float)(_config.StepsPerLine-1));
        }

        private void OnBeat()
        {
            MoveBeatInputs();
            
            if(_beats.Count > 0 && RhythmManager.Instance.CurrentBeat + _config.StepsPerLine == _beats[0].Beat)
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
                var position = Vector3.Lerp(RealInitialPosition, RealFinalPosition, factor);
                position += _jump*.5f;
                _inputsBeats[i].MoveTo(position);
            }
        }

        private void GenerateBeatInput(InputsBeat beat)
        {
            var newBeatInput = Instantiate(_config.Prefab, RealInitialPosition, Quaternion.identity, _parent);
            newBeatInput.transform.position += _jump*.5f;
            newBeatInput.Init(_config, beat);
            _placeHolder.sprite = _config.BeatsImages.Find(beatImage => beatImage.InputType == beat.Input).Image; 
            _inputsBeats.Add(newBeatInput);
            _beats.RemoveAt(0);
        }
    }
}
