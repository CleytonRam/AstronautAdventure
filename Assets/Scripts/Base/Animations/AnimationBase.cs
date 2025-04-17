using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public enum AnimationType 
    {
        NONE,
        IDDLE,
        RUN,
        ATTACK,
        DEATH
    }
    public class AnimationBase : MonoBehaviour
    {
        public Animator animator;
        public List<AnimationSetup> animationSetups;

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = animationSetups.Find(i => i.animationType == animationType);
            if (setup != null)
            {
                animator.SetTrigger(setup.trigger);
            }
            else
            {
                Debug.LogWarning($"Animation type {animationType} not found in setup.");
            }
        }
    }

    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string trigger;
    }
}
