using UnityEngine;
using Spine.Unity;
using Supyrb;
using ggj26.Event;
using Spine;

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

            SetSheepMask(Random.Range(0, 16));
        }

        private void MoveSheep(InputsTypes types)
        {
            switch (types)
            {
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
                case InputsTypes.Size:
                    break;
            }
        }

        private void SetSheepMask (int skinID)
        {
            var skeleton = sheepSkeleton.Skeleton;
            skeleton.SetSkin("mask" +  skinID.ToString());
            skeleton.SetSlotsToSetupPose();
            sheepSkeleton.AnimationState.Apply(skeleton);
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
