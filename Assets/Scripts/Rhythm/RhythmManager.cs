using ggj26.Event;
using ggj26.Services;
using MyBox;
using Supyrb;
using UnityEngine;

namespace ggj26
{
    public class RhythmManager : Singleton<RhythmManager>
    {
        [field: SerializeField]
        public RhythmManagerConfig Config { get; private set; }
        
        public float CurrentWave { get; private set; }
        public int CurrentBeat { get; private set; }
        
        private float _timestamp;
        private RhythmGameConfig _levelConfig;

        private RhythmGameController _currentLevel;
        
        public void InitGame(RhythmGameConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _timestamp = 0;
            ClockService.Instance?.SubscribeToUpdate(CustomUpdate);
            _currentLevel = new RhythmGameController();
            _currentLevel.Init(levelConfig);
            _timestamp += _levelConfig.BeatOffset;
        }

        private void CustomUpdate(float deltaTime)
        {
            _timestamp += deltaTime;

            if (IsTimeToBeat())
            {
                _timestamp -= (Config.Bmp / 60f);
                CurrentBeat++;
                MakeBeat();
            }
        }

        private void MakeBeat()
        {
            Signals.Get<OnBeatEvent>().Dispatch();
        }

        private bool IsTimeToBeat()
        {
            return _timestamp >= (Config.Bmp / 60f);
        }

        [field: SerializeField] public RhythmGameConfig _gameConfig;
        [ButtonMethod]
        public void GenerateLevel()
        {
            InitGame(_gameConfig);
        }
    }
}