using System;
using UnityEngine;
using Spine.Unity;
using Supyrb;
using ggj26.Event;
using ggj26.Services;
using MyBox;
using Random = UnityEngine.Random;

namespace ggj26
{
    public class SheepController : MonoBehaviour
    {
        [SerializeField] private SheepControllerConfig _config;
            
        [SerializeField] private SkeletonAnimation sheepSkeleton;

        private bool _ignoreNextInputBeat;
        private Vector2 _directionMovement;
        private Vector2 _movementArea;
        private Vector3 _centerPoint;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            Subscribe();
            
            Signals.Get<OnGameBeginEvent>().AddListener(OnGameBegin);
            Signals.Get<OnGameOverEvent>().AddListener(OnGameOver);
        }

        private void OnGameOver()
        {
            ClockService.Instance.UnSubscribeToUpdate(CustomUpdate);
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
            ClockService.Instance?.UnSubscribeToUpdate(CustomUpdate);
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

        public void SetSheepMask (int skinID)
        {
            var skeleton = sheepSkeleton.Skeleton;
            skeleton.SetSkin("mask" +  skinID.ToString());
            skeleton.SetSlotsToSetupPose();
            sheepSkeleton.AnimationState.Apply(skeleton);
        }

        public void SetMovement(Vector3 centerPoint, Vector2 movementArea)
        {
            _centerPoint = centerPoint;
            _movementArea = movementArea;
            _directionMovement = ((Random.insideUnitCircle + transform.position.ToVector2()) - transform.position.ToVector2()).normalized;
            ClockService.Instance.SubscribeToUpdate(CustomUpdate);
        }

        private void CustomUpdate(float deltaTime)
        {
            transform.Translate(_config.Speed*_directionMovement*deltaTime);
            CheckDirectionChanged();
            CapPosition();
        }

        private void CapPosition()
        {
            if (transform.position.x > _centerPoint.x + _movementArea.x * 0.5f)
            {
                transform.position = transform.position.SetX(_centerPoint.x + _movementArea.x * 0.5f);
            }
            
            if (transform.position.x < _centerPoint.x - _movementArea.x * 0.5f)
            {
                transform.position = transform.position.SetX(_centerPoint.x - _movementArea.x * 0.5f);
            }
            if (transform.position.y > _centerPoint.y + _movementArea.y * 0.5f)
            {
                transform.position = transform.position.SetY(_centerPoint.y + _movementArea.y * 0.5f);
            }
            if (transform.position.y < _centerPoint.y - _movementArea.y * 0.5f)
            {
                transform.position = transform.position.SetY(_centerPoint.y - _movementArea.y * 0.5f);
            }
        }

        private void CheckDirectionChanged()
        {
            if (transform.position.x > _centerPoint.x + _movementArea.x * 0.5f)
            {
                _directionMovement = _directionMovement.SetX(-_directionMovement.x);
            }
            
            if (transform.position.x < _centerPoint.x - _movementArea.x * 0.5f)
            {
                _directionMovement = _directionMovement.SetX(-_directionMovement.x);
            }
            if (transform.position.y > _centerPoint.y + _movementArea.y * 0.5f)
            {
                _directionMovement = _directionMovement.SetY(-_directionMovement.y);
            }
            if (transform.position.y < _centerPoint.y - _movementArea.y * 0.5f)
            {
                _directionMovement = _directionMovement.SetY(-_directionMovement.y);
            }
        }
    }
}
