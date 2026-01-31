using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ggj26
{
    public class UIRhythmInputs : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        public InputsBeat InputBeatData { get; private set;}
        
        UIRhythmInputsConfig _config;

        public void Init(UIRhythmConfig rhythmConfig, InputsBeat inputBeatData)
        {
            InputBeatData = inputBeatData;
            var image = rhythmConfig.BeatsImages.Find(beatImage => beatImage.InputType == inputBeatData.Input);
            _image.sprite = image.Image;
        }
        
        public void MoveTo(Vector3 position, bool destroyAfterMove = false)
        {
            transform.DOMove(position, _config.AnimationDuration).SetEase(_config.Ease).SetLink(gameObject).
                onComplete += () => OnMove(destroyAfterMove);
        }

        private void OnMove(bool destroyAfterMove)
        {
            if (destroyAfterMove)
            {
                Destroy(gameObject);
            }
        }
    }
}
