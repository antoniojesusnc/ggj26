using System;
using System.Collections.Generic;
using System.Linq;
using ggj26.Event;
using JetBrains.Annotations;
using Supyrb;
using TMPro;
using UnityEngine;

namespace ggj26
{
    public class UIRhythm : MonoBehaviour
    {
        private const string CURRENT_BEAT_FORMAT = "Beat: {0}";
        
        [SerializeField] private UIRhythmConfig _config;
        [SerializeField] private TextMeshProUGUI _currentBeat;
        [SerializeField] private List<RectTransform> _inputArea;
        
        private List<UIRhythmLineElement> _lines;
        private RhythmManager _rhythmManager;

        private void Start()
        {
            _lines = GetComponentsInChildren<UIRhythmLineElement>().ToList();
            _rhythmManager = RhythmManager.Instance;
            SubscribeToEvents();
            OnBeat();
            SetInputArea();
            _lines.ForEach(line => line.Init(_config));
        }

        private void SetInputArea()
        {
            var rectTransform = GetComponent<RectTransform>();
            var totalSize = rectTransform.sizeDelta.y;
            var stepsSize = totalSize / _config.StepsPerLine;
            _inputArea.ForEach(input => input.sizeDelta = new Vector2(input.sizeDelta.x*0.5f, stepsSize));
        }

        private void SubscribeToEvents()
        {
            Signals.Get<OnGameBeginEvent>().AddListener(OnGameBegin);
            Signals.Get<OnBeatEvent>().AddListener(OnBeat);
        }
        
        private void UnSubscribeToEvents()
        {
            Signals.Get<OnGameBeginEvent>().RemoveListener(OnGameBegin);
            Signals.Get<OnBeatEvent>().RemoveListener(OnBeat);
        }

        private void OnDestroy()
        {
            UnSubscribeToEvents();
        }

        private void OnBeat()
        {
            _currentBeat.text = string.Format(CURRENT_BEAT_FORMAT, _rhythmManager.CurrentBeat);
        }

        private void OnGameBegin()
        {
            OnBeat();
            for (int i = 0; i < _lines.Count; i++)
            {
                var input = _lines[i].Input;
                var beats = _rhythmManager.CurrentLevel.InputsBeats.FindAll(beat => beat.Input == input);
                beats.Sort(SortByBeat);
                _lines[i].BeginBeats(beats);
            }
        }

        private int SortByBeat(InputsBeat x, InputsBeat y)
        {
            return x.Beat.CompareTo(y.Beat);
        }
    }
}
