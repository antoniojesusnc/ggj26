using System;
using DG.Tweening;
using ggj26.Event;
using MyBox;
using Supyrb;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace ggj26
{
    public class SpotLightController : MonoBehaviour
    {
        [SerializeField]
        private SpotLightConfig _config;
        
        [SerializeField]
        private Light2D _light2D;

        private Tween _chooseColorTween;

        private void Start()
        {
            _light2D.gameObject.SetActive(false);
            
            Signals.Get<OnGameBeginEvent>().AddListener(OnGameBegin);
        }

        private void OnDestroy()
        {
            _chooseColorTween?.Kill();
            Signals.Get<OnGameBeginEvent>().RemoveListener(OnGameBegin);
        }

        private void OnGameBegin()
        {
            _light2D.gameObject.SetActive(true);

            var finalRotation = Vector3.forward *
                                _config.RotateMaxAngleRange.Vector2LerpAutoRate(); 
            transform.DOLocalRotate(finalRotation, _config.RotateSpeedRange.Vector2LerpAutoRate())
                .SetDelay(_config.StartDelayRange.Vector2LerpAutoRate())
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);

            ChooseColor();
        }

        private void ChooseColor()
        {
            _light2D.color = RhythmManager.Instance.Config.Colors.GetRandom();
            _light2D.intensity = _config.IntensityRange.Vector2LerpAutoRate();
            
            _chooseColorTween = DOVirtual.DelayedCall(_config.FlashEachRange.Vector2LerpAutoRate(), ChooseColor);
        }
    }
}
