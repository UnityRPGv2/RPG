using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string triggerName;
        [SerializeField] UnityEvent trigger;

        public void Trigger(string name)
        {
            if (name == triggerName)
            {
                trigger.Invoke();
            }
        }

    }
}