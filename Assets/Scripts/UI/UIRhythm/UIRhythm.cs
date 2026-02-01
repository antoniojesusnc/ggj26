using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ggj26.Event;
using ggj26.Services;
using JetBrains.Annotations;
using Supyrb;
using TMPro;
using UnityEngine;

namespace ggj26
{
    public class UIRhythm : MonoBehaviour
    {
        [SerializeField] private UIRhythmConfig _config;
        [SerializeField] private TextMeshProUGUI _currentBeat;
        [SerializeField] private List<RectTransform> _inputArea;
        [SerializeField] private TextMeshProUGUI _beatsCounter;
        
        
        private List<UIRhythmLineElement> _lines;
        private RhythmManager _rhythmManager;

        private void Start()
        {
            _lines = GetComponentsInChildren<UIRhythmLineElement>().ToList();
            _rhythmManager = RhythmManager.Instance;
            SubscribeToEvents();
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

            SetTime();
            ;
            ClockService.Instance.SubscribeToUpdate(CustomUpdate);
        }

        private void CustomUpdate(float deltaTime)
        {
            SetTime();
        }

        private void UnSubscribeToEvents()
        {
            Signals.Get<OnGameBeginEvent>().RemoveListener(OnGameBegin);
            
            ClockService.Instance.UnSubscribeToUpdate(CustomUpdate);
        }
        
        private void SetTime()
        {
            _beatsCounter.text = TimeSpan.FromSeconds(RhythmManager.Instance.RemainingTime()).ToString(@"mm\:ss");
        }

        private void OnDestroy()
        {
            UnSubscribeToEvents();
        }

        private void OnGameBegin()
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                var input = _lines[i].Input;
                var beats = _rhythmManager.CurrentLevel.InputsBeats.FindAll(beat => beat.Input == input);
                beats.Sort(SortByBeat);
                _lines[i].BeginBeats(beats, input);
            }
        }

        private int SortByBeat(InputsBeat x, InputsBeat y)
        {
            return x.Beat.CompareTo(y.Beat);
        }
    }
}
