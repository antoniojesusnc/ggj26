using System.Linq;
using ggj26.Event;
using ggj26.Services;
using MyBox;
using Supyrb;
using UnityEngine;
using Urd.Audio;
using Urd.Services;

namespace ggj26
{
    public class RhythmManager : Singleton<RhythmManager>
    {
        [field: SerializeField]
        public RhythmManagerConfig Config { get; private set; }
        public RhythmGameController CurrentLevel { get; private set; }
        
        public int CurrentBeat { get; private set; }
        public float BitEachSeconds { get; private set; }
        
        private float _timestamp;
        private int _maxBeat;

        [field: SerializeField]
        public int WolfID { get; private set; }

        [SerializeField] private bool autoPlay = false;
        
        void Start()
        {
            if (autoPlay) Invoke(nameof(GenerateLevel), 1);
        }
        public void InitGame(RhythmGameConfig levelConfig)
        {
            _timestamp = 0;
            ClockService.Instance?.SubscribeToUpdate(CustomUpdate);
            CurrentLevel = new RhythmGameController();
            CurrentLevel.Init(levelConfig);
            _timestamp += CurrentLevel.LevelConfig.BeatOffset;
            
            BitEachSeconds = 1f/(CurrentLevel.LevelConfig.Bmp / 60f);
            _maxBeat = CurrentLevel.InputsBeats.Max(beat => beat.Beat) + levelConfig.BeatToEnd;
            Signals.Get<OnGameBeginEvent>().Dispatch();

            PlayAudio();
        }

        private void PlayAudio()
        {
            var audioModel = new AudioModel(Ggj26AudioTypes.MainTheme);
            if(AudioService.Instance.Config.TryGetAudioData(audioModel, out var audioConfigData))
            {
                audioModel.SetAudioConfigData(audioConfigData);
                audioModel.SetAudioClip(CurrentLevel.LevelConfig.AudioClip);
                AudioService.Instance.PlaySound(audioModel);
            }
        }

        private void CustomUpdate(float deltaTime)
        {
            _timestamp += deltaTime;

            if (_timestamp >= BitEachSeconds)
            {
                _timestamp -= BitEachSeconds;
                CurrentBeat++;
                MakeBeat();
            }

            if (IsGameOver())
            {
                GameOver();
            }
        }

        private bool IsGameOver()
        {
            return CurrentBeat >= _maxBeat;
        }

        public void GameOver()
        {
            Signals.Get<OnGameOverEvent>().Dispatch();
            ClockService.Instance?.UnSubscribeToUpdate(CustomUpdate);
        }

        private void MakeBeat()
        {
            var beatNow = CurrentLevel.InputsBeats.Find(beat => beat.Beat == CurrentBeat);
            if (beatNow != null)
            {
                Signals.Get<OnBeatInputEvent>().Dispatch(beatNow.Input);
            }
            else
            {
                Signals.Get<OnBeatInputEvent>().Dispatch(InputsTypes.None);
            }
            
            Signals.Get<OnBeatEvent>().Dispatch();
        }

        [field: SerializeField] public RhythmGameConfig _gameConfig;

        [ButtonMethod]
        public void GenerateLevel()
        {
            InitGame(_gameConfig);
        }

        public bool TryGetNextBeat(out InputsBeat inputsBeat)
        {
            var nextBeat = CurrentLevel.InputsBeats.Find(beat => beat.Beat == CurrentBeat + 1);
            inputsBeat = nextBeat;
            return inputsBeat != null;
        }

        public void SetWolfID (int id)
        {
            WolfID = id;
            Debug.Log("Wolf chose mask #" +  WolfID);
        }
    }
}