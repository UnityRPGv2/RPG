using System.Collections;
using RPG.Core;
using UnityEngine;


namespace RPG.Abilities.Helpers
{
    public class CoroutineAction : IAction
    {
        MonoBehaviour owner;
        IEnumerator enumerator;
        Coroutine coroutine;

        public CoroutineAction(MonoBehaviour owner, IEnumerator enumerator)
        {
            this.owner = owner;
            this.enumerator = enumerator;
        }

        public void Activate()
        {
            coroutine = owner.StartCoroutine(enumerator);
        }

        public void Cancel()
        {
            owner.StopCoroutine(coroutine);
        }
    }
}
