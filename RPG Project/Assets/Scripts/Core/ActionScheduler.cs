using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        int currentPriority = 0;
        IAction nextAction;
        int nextPriority = 0;

        public void StartAction(IAction action, int priority, int cancelAllBelow)
        {
            if (currentAction == action) return;
            if (cancelAllBelow > currentPriority)
            {
                if (currentAction != null) currentAction.Cancel();
                currentAction = action;
                currentPriority = priority;
            }
            else
            {
                nextAction = action;
                nextPriority = priority;
            }
        }

        public void FinishAction(IAction action)
        {
            if (!isCurrentAction(action)) return;

            currentAction = nextAction;
            nextAction = null;
            currentPriority = nextPriority;
            nextPriority = 0;
        }

        public bool isCurrentAction(IAction action)
        {
            return currentAction == action;
        }

        public void CancelCurrentAction(int cancelAllBelow)
        {
            StartAction(null, 0, cancelAllBelow);
        }
    }
}