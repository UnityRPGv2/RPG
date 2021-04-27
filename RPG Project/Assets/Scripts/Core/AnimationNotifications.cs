using System;
using UnityEngine;

namespace RPG.Core
{
    public class AnimationNotifications : MonoBehaviour
    {
        public event Action<AnimationEvent> animationNotify;

        void Shoot(AnimationEvent evnt)
        {
            Use(evnt);
        }

        void Hit(AnimationEvent evnt)
        {
            Use(evnt);
        }

        void Use(AnimationEvent evnt)
        {
            if (animationNotify != null) animationNotify(evnt);
        }
    }
}

