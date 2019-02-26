using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
        }
    }
}