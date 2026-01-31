using UnityEngine;
using Spine.Unity;
using Supyrb;
using ggj26.Event;
using System;

namespace ggj26
{
    public class SheepAnimationControl : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation sheepSkeleton;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            Signals.Get<OnBeatInputEvent>().AddListener(MoveSheep);

            sheepSkeleton.AnimationState.SetAnimation(0, "beat", true);
        }

        public void MoveSheep(InputsTypes types)
        {
            switch (types)
            {
                case InputsTypes.None:
                    sheepSkeleton.AnimationState.ClearTracks();
                    sheepSkeleton.AnimationState.SetAnimation(0, "beat", true);
                    break;
                case InputsTypes.Up:
                    sheepSkeleton.AnimationState.AddAnimation(1, "up", false, 0);
                    break;
                case InputsTypes.Down:
                    sheepSkeleton.AnimationState.AddAnimation(1, "down", false,0);
                    break;
                case InputsTypes.Left:
                    sheepSkeleton.AnimationState.AddAnimation(1, "left", false,0);
                    break;
                case InputsTypes.Right:
                    sheepSkeleton.AnimationState.AddAnimation(1, "right", false,0);
                    break;
                case InputsTypes.Size:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                sheepSkeleton.AnimationState.SetAnimation(1, "up", false);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                sheepSkeleton.AnimationState.SetAnimation(1, "down", false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sheepSkeleton.AnimationState.SetAnimation(1, "left", false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                sheepSkeleton.AnimationState.SetAnimation(1, "right", false);
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                sheepSkeleton.AnimationState.ClearTracks();
                sheepSkeleton.AnimationState.SetAnimation(0, "beat", true);

            }
        }

    }
}
