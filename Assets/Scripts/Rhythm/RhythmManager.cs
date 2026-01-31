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
        public RhythmGameController CurrentLevel { get; private set; }
        
        public int CurrentBeat { get; private set; }
        
        private float _timestamp;
        private float _bitEachSeconds;

        void Start()
        {
            Invoke(nameof(GenerateLevel), 1);
        }
        public void InitGame(RhythmGameConfig levelConfig)
        {
            _timestamp = 0;
            ClockService.Instance?.SubscribeToUpdate(CustomUpdate);
            CurrentLevel = new RhythmGameController();
            CurrentLevel.Init(levelConfig);
            _timestamp += CurrentLevel.LevelConfig.BeatOffset;
            
            Signals.Get<OnGameBeginEvent>().Dispatch();
            _bitEachSeconds = 1f/(CurrentLevel.LevelConfig.Bmp / 60f);
        }

        private void CustomUpdate(float deltaTime)
        {
            _timestamp += deltaTime;

            if (_timestamp >= _bitEachSeconds)
            {
                _timestamp -= _bitEachSeconds;
                CurrentBeat++;
                MakeBeat();
            }
        }

        private void MakeBeat()
        {
            Signals.Get<OnBeatEvent>().Dispatch();
        }

        [field: SerializeField] public RhythmGameConfig _gameConfig;

        [ButtonMethod]
        public void GenerateLevel()
        {
            InitGame(_gameConfig);
        }
    }
}