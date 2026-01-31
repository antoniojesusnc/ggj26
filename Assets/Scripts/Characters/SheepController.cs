using System;
using UnityEngine;
using Spine.Unity;
using Supyrb;
using ggj26.Event;
using ggj26.Services;
using Random = UnityEngine.Random;

namespace ggj26
{
    public class SheepController : MonoBehaviour
    {
        [SerializeField] private SheepControllerConfig _config;
            
        [SerializeField] private SkeletonAnimation sheepSkeleton;

        private bool _ignoreNextInputBeat;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            Subscribe();
            
            Signals.Get<OnGameBeginEvent>().AddListener(OnGameBegin);
            
            SetSheepMask(Random.Range(0, 16));
        }

        private void OnGameBegin()
        {
            sheepSkeleton.AnimationState.TimeScale = 1/RhythmManager.Instance.BitEachSeconds;
            sheepSkeleton.AnimationState.SetAnimation(0, "beat", true);
        }

        protected virtual void Subscribe()
        {
            Signals.Get<OnBeatInputEvent>().AddListener(OnMoveSheep);
            Signals.Get<OnBeatEvent>().AddListener(OnBeat);
        }

        private void OnBeat()
        {
            _ignoreNextInputBeat = false;
            if (_config.AnticipationRate <= 0 || Random.value > _config.AnticipationRate)
            {
                return;
            }
            
            if(!RhythmManager.Instance.TryGetNextBeat(out var nextBeat))
            {
                return;
            }

            _ignoreNextInputBeat = true;

            var anticipationTime = RhythmManager.Instance.BitEachSeconds * (1-_config.AnticipationRateFromBeatTime);
            ClockService.Instance.AddDelayCall(anticipationTime, () => MoveSheep(nextBeat.Input));
        }

        void OnDestroy()
        {
            Signals.Get<OnBeatInputEvent>().RemoveListener(OnMoveSheep);
        }

        private void OnMoveSheep(InputsTypes input)
        {
            if (_ignoreNextInputBeat)
            {
                return;
            }

            if (_config.DelayRate <= 0 || Random.value > _config.DelayRate)
            {
                CheckIfWrongRate(input);
            }
            else
            {
                var delayTime = RhythmManager.Instance.BitEachSeconds * _config.DelayRateFromBeatTime;
                ClockService.Instance.AddDelayCall(delayTime, () => CheckIfWrongRate(input));
            }
        }

        private void CheckIfWrongRate(InputsTypes input)
        {
            
            var newInput = input;
            if (input != InputsTypes.None && _config.WrongRate > 0 && Random.value < _config.WrongRate)
            {
                newInput = input.GetAnyExceptThis();
            }
            
            MoveSheep(newInput);
        }

        protected void MoveSheep(InputsTypes input)
        {
            switch (input)
            {
                case InputsTypes.None:
                    sheepSkeleton.AnimationState.ClearTracks();
                    sheepSkeleton.AnimationState.SetAnimation(0, "beat", true);
                    break;
                case InputsTypes.Up:
                    sheepSkeleton.AnimationState.SetAnimation(1, "up", false);
                    break;
                case InputsTypes.Down:
                    sheepSkeleton.AnimationState.SetAnimation(1, "down", false);
                    break;
                case InputsTypes.Left:
                    sheepSkeleton.AnimationState.SetAnimation(1, "left", false);
                    break;
                case InputsTypes.Right:
                    sheepSkeleton.AnimationState.SetAnimation(1, "right", false);
                    break;
            }

            if (input != InputsTypes.None)
            {
                AfterAnimation();
            }
        }

        protected virtual void AfterAnimation()
        {
            ClockService.Instance.AddDelayCall(
                RhythmManager.Instance.BitEachSeconds * _config.BeatRateToComeBackAnimation,
                () => MoveSheep(InputsTypes.None));
        }

        private void SetSheepMask (int skinID)
        {
            var skeleton = sheepSkeleton.Skeleton;
            skeleton.SetSkin("mask" +  skinID.ToString());
            skeleton.SetSlotsToSetupPose();
            sheepSkeleton.AnimationState.Apply(skeleton);
        }
    }
}
